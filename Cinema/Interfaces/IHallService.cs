using Cinema.Models.Request;
using Cinema.Models.Response;

namespace Cinema.Interfaces
{
    public interface IHallService
    {
        public Task<IEnumerable<HallsGetResponce>> GetAllHallsAsync();
        public Task<bool> CreateHallAsync(HallUploadRequest film);
    }
}
