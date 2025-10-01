using AutoMapper;
using Cinema.Interfaces;
using Cinema.Models.Dto;
using Cinema.Models.Request;
using Cinema.Models.Response;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Services
{
    public class SessionService(IMapper mapper, ICinemaDbContext dbContext) : ISessionService
    {
        private readonly IMapper _mapper = mapper;
        private readonly ICinemaDbContext _dbContext = dbContext;

        public async Task<bool> CreateSessionAsync(SessionUploadRequest session)
        {
            var film = await _dbContext.Films
                .FirstOrDefaultAsync(f => f.Name == session.FilmName && f.IsActive);
            ArgumentNullException.ThrowIfNull(film);

            var hall = await _dbContext.Halls
                .FirstOrDefaultAsync(h => h.Name == session.HallName);
            ArgumentNullException.ThrowIfNull(hall);

            var sessionDto = _mapper.Map<SessionDto>(session);
            sessionDto.Film = film;
            sessionDto.Hall = hall;
            await _dbContext.Sessions.AddAsync(sessionDto);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteSessionAsync(int sessionId)
        {
            var session = _dbContext.Sessions.FirstOrDefault(src => src.Id == sessionId);
            ArgumentNullException.ThrowIfNull(session);
            session.Status = false;
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<SessionsGetResponce>> GetAllSessionsAsync()
        {
            var response = await _dbContext.Sessions.Select(session => _mapper.Map<SessionsGetResponce>(session)).ToListAsync();
            return response;
        }
    }
}
