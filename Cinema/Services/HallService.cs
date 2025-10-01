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

        public async Task<bool> CreateHallAsync(HallUploadRequest hall)
        {
            var hallDto = _mapper.Map<HallDto>(hall);
            await _dbContext.Halls.AddAsync(hallDto);
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
