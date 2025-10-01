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
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
