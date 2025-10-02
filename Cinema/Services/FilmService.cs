using AutoMapper;
using Cinema.Interfaces;
using Cinema.Models.Dto;
using Cinema.Models.Request;
using Cinema.Models.Response;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Services
{
    public class FilmService(IMapper mapper, ICinemaDbContext dbContext) : IFilmService
    {
        private readonly IMapper _mapper = mapper;
        private readonly ICinemaDbContext _dbContext = dbContext;

        public async Task<bool> CreateFilmAsync(FilmUploadRequest film)
        {
            var filmDto = _mapper.Map<FilmDto>(film);
            await _dbContext.Films.AddAsync(filmDto);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<FilmsGetResponse>> GetAllFilmsAsync()
        {
            var response = await _dbContext.Films.Where(film => film.IsActive).Select(film => _mapper.Map<FilmsGetResponse>(film)).ToListAsync();
            return response;
        }

        public async Task<bool> DeleteFilmAsync(int filmId)
        {
            var film = _dbContext.Films.FirstOrDefault(src => src.Id == filmId);
            ArgumentNullException.ThrowIfNull(film);
            film.IsActive = false;
            var sessions = _dbContext.Sessions
                .Where(src => src.FilmId == filmId)
                .ExecuteUpdateAsync(src => src
                    .SetProperty(s => s.Status, false));
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<FilmSearchResult>> SearchFilmsAsync(FilmSearchRequest request)
        {
            var baseQuery = _dbContext.Films
                .Include(f => f.Sessions)
                    .ThenInclude(s => s.Hall)
                .AsQueryable();

            var filteredQuery = ApplyFilters(baseQuery, request);

            var films = await filteredQuery.ToListAsync();

            var result = films.Select(film => new FilmSearchResult
            {
                Id = film.Id,
                Name = film.Name,
                Year = film.Year,
                Duration = film.Duration,
                AgeLimit = film.AgeLimit,
                GenreString = film.GenreString,
                Genre = film.Genre,
                Poster = film.Poster,
                IsActive = film.IsActive,
                NearestSession = GetNearestSession(film, request.FromTime ?? TimeOnly.MinValue)
            })
            .Where(f => f.NearestSession != null) 
            .OrderBy(f => f.NearestSession.StartTime)
            .ToList();

            return result;
        }

        private IQueryable<FilmDto> ApplyFilters(IQueryable<FilmDto> query, FilmSearchRequest request)
        {
            // Фильтр по названию (подстрока)
            if (!string.IsNullOrWhiteSpace(request.Name))
            {
                query = query.Where(f => f.Name.ToLower().Contains(request.Name.ToLower()));
            }

            // Фильтр по жанрам
            if (request.Genres != null && request.Genres.Any())
            {
                var genreStrings = request.Genres.Select(g => g.ToString()).ToList();
                query = query.Where(f => genreStrings.Contains(f.GenreString));
            }

            // Фильтр по году выпуска
            if (request.MinYear.HasValue)
            {
                query = query.Where(f => f.Year >= request.MinYear.Value);
            }

            if (request.MaxYear.HasValue)
            {
                query = query.Where(f => f.Year <= request.MaxYear.Value);
            }

            // Фильтр по возрастному ограничению
            if (request.MinAgeLimit.HasValue)
            {
                query = query.Where(f => f.AgeLimit >= request.MinAgeLimit.Value);
            }

            if (request.MaxAgeLimit.HasValue)
            {
                query = query.Where(f => f.AgeLimit <= request.MaxAgeLimit.Value);
            }

            // Фильтр по продолжительности
            if (request.MinDuration.HasValue)
            {
                query = query.Where(f => f.Duration >= request.MinDuration.Value);
            }

            if (request.MaxDuration.HasValue)
            {
                query = query.Where(f => f.Duration <= request.MaxDuration.Value);
            }

            // Фильтр по активности
            if (request.IsActive.HasValue)
            {
                query = query.Where(f => f.IsActive == request.IsActive.Value);
            }

            return query;
        }

        private NearestSession GetNearestSession(FilmDto film, TimeOnly fromTime)
        {
            var nearestSession = film.Sessions
                .Where(s => s.StartTime >= fromTime && s.Status)
                .OrderBy(s => s.StartTime)
                .FirstOrDefault();

            if (nearestSession == null)
                return null;

            return new NearestSession
            {
                SessionId = nearestSession.Id,
                StartTime = nearestSession.StartTime,
                HallName = nearestSession.Hall.Name,
                Price = nearestSession.Price,
                MinutesUntilStart = (int)(nearestSession.StartTime.ToTimeSpan() - DateTime.Now.TimeOfDay).TotalMinutes
            };
        }
    }
}
