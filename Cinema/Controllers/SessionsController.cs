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

        [HttpGet("get")]
        public async Task<ActionResult<IEnumerable<SessionsGetResponce>>> GetAllSessions()
        {
            var response = await _sessionService.GetAllSessionsAsync();
            return Ok(response);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateSession(SessionUploadRequest session)
        {
            var response = await _sessionService.CreateSessionAsync(session);
            return Ok(response);
        }

        [HttpPost("remove")]
        public async Task<IActionResult> RemoveSesion([FromQuery]int sessionId)
        {
            var response = await _sessionService.DeleteSessionAsync(sessionId);
            return Ok(response);
        }
    }
}
