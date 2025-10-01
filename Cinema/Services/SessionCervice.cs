using Cinema.Interfaces;
using Cinema.Models.Request;
using Cinema.Models.Response;

namespace Cinema.Services
{
    public class SessionCervice : ISessionService
    {
        public Task<bool> CreateSessionAsync(SessionUploadRequest session)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteSessionAsync(int filmId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<FilmsGetResponse>> GetAllSessionsAsync()
        {
            throw new NotImplementedException();
        }
    }
}
