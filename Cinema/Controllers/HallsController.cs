using Cinema.Interfaces;
using Cinema.Models.Request;
using Cinema.Models.Response;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.Controllers
{
    [ApiController]
    [Route("Halls")]
    public class HallsController(IHallService HallService) : ControllerBase
    {
        private readonly IHallService _HallsService = HallService;

        [HttpGet("Halls")]
        public async Task<ActionResult<IEnumerable<HallsGetResponce>>> GetAllFilms()
        {
            var response = await _HallsService.GetAllHallsAsync();
            return Ok(response);
        }

        [HttpPost("Halls")]
        public async Task<IActionResult> CreateFilm(HallUploadRequest film)
        {
            var response = await _HallsService.CreateHallAsync(film);
            return Ok(response);
        }
    }
}
