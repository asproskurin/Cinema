using Cinema.Interfaces;
using Cinema.Models.Request;
using Cinema.Models.Response;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.Controllers
{
    [ApiController]
    [Route("Sessions")]
    public class SessionsController(ISessionService sessionService) : ControllerBase
    {
        private readonly ISessionService _sessionService = sessionService;

        [HttpGet("sessions")]
        public async Task<ActionResult<IEnumerable<HallsGetResponce>>> GetAllHalls()
        {
            var response = await _sessionService.GetAllSessionsAsync();
            return Ok(response);
        }

        [HttpPost("session")]
        public async Task<IActionResult> CreateHall(SessionUploadRequest session)
        {
            var response = await _sessionService.CreateSessionAsync(session);
            return Ok(response);
        }
    }
}
