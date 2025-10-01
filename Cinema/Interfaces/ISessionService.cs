using Cinema.Models.Request;
using Cinema.Models.Response;

namespace Cinema.Interfaces
{
    public interface ISessionService
    {
        public Task<IEnumerable<SessionsGetResponce>> GetAllSessionsAsync();
        public Task<bool> CreateSessionAsync(SessionUploadRequest session);
        public Task<bool> DeleteSessionAsync(int sessionId);
    }
}
