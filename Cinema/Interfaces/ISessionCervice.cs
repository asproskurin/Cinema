using Cinema.Models.Request;
using Cinema.Models.Response;

namespace Cinema.Interfaces
{
    public interface ISessionCervice
    {
        public Task<IEnumerable<FilmsGetResponse>> GetAllSessionsAsync();
        public Task<bool> CreateSessionAsync(FilmUploadRequest film);
        public Task<bool> DeleteSessionAsync(int filmId);
    }
}
