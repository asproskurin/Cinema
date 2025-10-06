using Cinema.Models.Request;
using Cinema.Models.Response;

namespace Cinema.Interfaces
{
    public interface IHallService
    {
        public Task<IEnumerable<HallsGetResponse>> GetAllHallsAsync();
        public Task<bool> CreateHallAsync(HallUploadRequest hall);
    }
}
