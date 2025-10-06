using Cinema.Models.Request;
using Cinema.Models.Response;

namespace Cinema.Interfaces
{
    public interface ISessionService
    {
        public Task<IEnumerable<SessionsGetResponse>> GetAllSessionsAsync();
        public Task<bool> CreateSessionAsync(SessionUploadRequest session);
        public Task<bool> DeleteSessionAsync(int sessionId);
        Task<FilmSessionsResponse> GetFilmSessionsAsync(FilmSessionsRequest request);
        Task<ActiveSessionsResponse> GetActiveSessionsAsync(ActiveSessionsRequest request);
    }
}
