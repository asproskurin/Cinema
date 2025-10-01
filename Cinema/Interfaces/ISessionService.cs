using Cinema.Models.Request;
using Cinema.Models.Response;

namespace Cinema.Interfaces
{
    public interface ISessionService
    {
        public Task<IEnumerable<FilmsGetResponse>> GetAllSessionsAsync();
        public Task<bool> CreateSessionAsync(SessionUploadRequest session);
        public Task<bool> DeleteSessionAsync(int filmId);
    }
}
