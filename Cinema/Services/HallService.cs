using AutoMapper;
using Cinema.Interfaces;
using Cinema.Models.Dto;
using Cinema.Models.Request;
using Cinema.Models.Response;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Services
{
    public class HallService(IMapper mapper, ICinemaDbContext dbContext) : IHallService
    {
        private readonly IMapper _mapper = mapper;
        private readonly ICinemaDbContext _dbContext = dbContext;

        public async Task<bool> CreateHallAsync(HallUploadRequest film)
        {
            var Hall = _mapper.Map<HallDto>(film);
            await _dbContext.Halls.AddAsync(Hall);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<HallsGetResponce>> GetAllHallsAsync()
        {
            var response = await _dbContext.Halls.Select(film => _mapper.Map<HallsGetResponce>(film)).ToListAsync();
            return response;
        }
    }
}
