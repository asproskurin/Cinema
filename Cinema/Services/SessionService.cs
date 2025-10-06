using AutoMapper;
using Cinema.Interfaces;
using Cinema.Models;
using Cinema.Models.Dto;
using Cinema.Models.Request;
using Cinema.Models.Response;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OpenQA.Selenium;

namespace Cinema.Services
{
    public class SessionService(IMapper mapper, ICinemaDbContext dbContext, IOptions<HallSettings> hallSettings) : ISessionService
    {
        private readonly IMapper _mapper = mapper;
        private readonly ICinemaDbContext _dbContext = dbContext;
        private readonly HallSettings _hallSettings = hallSettings.Value;
        public async Task<bool> CreateSessionAsync(SessionUploadRequest session)
        {
            var film = await _dbContext.Films
                .FirstOrDefaultAsync(f => f.Name == session.FilmName && f.IsActive);
            ArgumentNullException.ThrowIfNull(film);

            var hall = await _dbContext.Halls
                .FirstOrDefaultAsync(h => h.Name == session.HallName);
            ArgumentNullException.ThrowIfNull(hall);

            var sessionEndTime = session.StartTime.AddMinutes(film.Duration + hall.BreakTime);
            var isTimeSlotAvailable = !_dbContext.Sessions
                .Where(src => src.HallId == hall.Id && src.StartDate < session.StartDate)
                .Select(src => new { Start = src.StartTime, End = src.StartTime.AddMinutes(hall.BreakTime + src.Duration)})
                .Any(src => sessionEndTime > src.Start && session.StartTime < src.End 
                && session.StartTime >= _hallSettings.StartTime && sessionEndTime <= _hallSettings.EndTime);
            if (isTimeSlotAvailable)
            {
                var sessionDto = _mapper.Map<SessionDto>(session);
                sessionDto.Film = film;
                sessionDto.Hall = hall;
                sessionDto.Duration = film.Duration;

                await _dbContext.Sessions.AddAsync(sessionDto);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteSessionAsync(int sessionId)
        {
            var session = _dbContext.Sessions.FirstOrDefault(src => src.Id == sessionId);
            ArgumentNullException.ThrowIfNull(session);
            session.Status = false;
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<SessionsGetResponse>> GetAllSessionsAsync()
        {
            var response = await _dbContext.Sessions.Select(session => _mapper.Map<SessionsGetResponse>(session)).ToListAsync();
            return response;
        }

        public async Task<FilmSessionsResponse> GetFilmSessionsAsync(FilmSessionsRequest request)
        {
            var film = await _dbContext.Films
                .Include(f => f.Sessions)
                    .ThenInclude(s => s.Hall)
                .FirstOrDefaultAsync(f => f.Id == request.FilmId && f.IsActive);

            if (film == null)
            {
                throw new NotFoundException($"Фильм с ID {request.FilmId} не найден");
            }

            var response = _mapper.Map<FilmSessionsResponse>(film);
            return response;
        }

        public async Task<ActiveSessionsResponse> GetActiveSessionsAsync(ActiveSessionsRequest request)
        {
            var query = _dbContext.Sessions
                .Include(s => s.Film)
                .Include(s => s.Hall)
                .Where(s => s.Status && s.Film.IsActive);

            query = ApplyFilters(query, request);

            var sessions = await query
                .OrderBy(s => s.StartTime)
                .Select(s => new
                {
                    Session = s,
                    s.Film,
                    s.Hall
                })
                .ToListAsync();

            var filmsGroups = sessions
                .GroupBy(x => x.Film)
                .Select(g => new FilmSessionsGroup
                {
                    FilmId = g.Key.Id,
                    FilmName = g.Key.Name,
                    Year = g.Key.Year,
                    Duration = g.Key.Duration,
                    AgeLimit = g.Key.AgeLimit,
                    GenreString = g.Key.GenreString,
                    Poster = g.Key.Poster,
                    FirstSessionTime = g.Min(x => x.Session.StartTime),
                    MinPrice = g.Min(x => x.Session.Price),
                    MaxPrice = g.Max(x => x.Session.Price),
                    Sessions = g.Select(x => new SessionShortInfo
                    {
                        SessionId = x.Session.Id,
                        StartTime = x.Session.StartTime,
                        EndTime = x.Session.StartTime.AddMinutes(x.Film.Duration),
                        HallName = x.Hall.Name,
                        HallId = x.Hall.Id,
                        Price = x.Session.Price
                    })
                    .OrderBy(s => s.StartTime)
                    .ToList()
                })
                .OrderBy(f => f.FirstSessionTime)
                .ToList();

            return new ActiveSessionsResponse
            {
                TotalFilms = filmsGroups.Count(),
                TotalSessions = sessions.Count(),
                Films = filmsGroups
            };
        }

        private IQueryable<SessionDto> ApplyFilters(IQueryable<SessionDto> query, ActiveSessionsRequest request)
        {
            // Фильтр по жанрам
            if (request.Genres != null && request.Genres.Any())
            {
                var genreStrings = request.Genres.Select(g => g.ToString()).ToList();
                query = query.Where(s => genreStrings.Contains(s.Film.GenreString));
            }

            // Фильтр по году выпуска
            if (request.MinYear.HasValue)
                query = query.Where(s => s.Film.Year >= request.MinYear.Value);

            if (request.MaxYear.HasValue)
                query = query.Where(s => s.Film.Year <= request.MaxYear.Value);

            // Фильтр по возрастному ограничению
            if (request.MinAgeLimit.HasValue)
                query = query.Where(s => s.Film.AgeLimit >= request.MinAgeLimit.Value);

            if (request.MaxAgeLimit.HasValue)
                query = query.Where(s => s.Film.AgeLimit <= request.MaxAgeLimit.Value);

            // Фильтр по продолжительности
            if (request.MinDuration.HasValue)
                query = query.Where(s => s.Film.Duration >= request.MinDuration.Value);

            if (request.MaxDuration.HasValue)
                query = query.Where(s => s.Film.Duration <= request.MaxDuration.Value);

            // Фильтр по цене
            if (request.MinPrice.HasValue)
                query = query.Where(s => s.Price >= request.MinPrice.Value);

            if (request.MaxPrice.HasValue)
                query = query.Where(s => s.Price <= request.MaxPrice.Value);

            // Фильтр по залам
            if (request.HallIds != null && request.HallIds.Any())
                query = query.Where(s => request.HallIds.Contains(s.HallId));

            return query;
        }
    }
}
