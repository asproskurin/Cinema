using Cinema.Models.Dto;
using Cinema.Models.Request;
using Cinema.Models.Response;

namespace Cinema.Interfaces
{
    public interface IFilmService
    {
        public Task<IEnumerable<FilmsGetResponse>> GetAllFilmsAsync();
        public Task<bool> CreateFilmAsync(FilmUploadRequest film);
        public Task<bool> DeleteFilmAsync(int filmId);
    }
}
