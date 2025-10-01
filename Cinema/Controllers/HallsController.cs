using Cinema.Interfaces;
using Cinema.Models.Request;
using Cinema.Models.Response;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.Controllers
{
    [ApiController]
    [Route("Halls")]
    public class HallsController(IHallService hallService) : ControllerBase
    {
        private readonly IHallService _hallsService = hallService;

        [HttpGet("halls")]
        public async Task<ActionResult<IEnumerable<HallsGetResponce>>> GetAllHalls()
        {
            var response = await _hallsService.GetAllHallsAsync();
            return Ok(response);
        }

        [HttpPost("hall")]
        public async Task<IActionResult> CreateHall(HallUploadRequest hall)
        {
            var response = await _hallsService.CreateHallAsync(hall);
            return Ok(response);
        }
    }
}
