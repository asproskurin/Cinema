using Cinema.Interfaces;
using Cinema.Models.Dto;
using Cinema.Models.Request;
using Cinema.Models.Response;
using Microsoft.AspNetCore.Mvc;
using OpenQA.Selenium;

namespace Cinema.Controllers
{
    [ApiController]
    [Route("Sessions")]
    public class SessionsController(ISessionService sessionService) : ControllerBase
    {
        private readonly ISessionService _sessionService = sessionService;

        [HttpGet("get")]
        public async Task<ActionResult<IEnumerable<SessionsGetResponse>>> GetAllSessions()
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

        [HttpGet("{filmId}/sessions")]
        public async Task<ActionResult<FilmSessionsResponse>> GetFilmSessions(FilmSessionsRequest request)
        {
            try
            {
                var result = await _sessionService.GetFilmSessionsAsync(request);
                return Ok(result);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Внутренняя ошибка сервера", Details = ex.Message });
            }
        }

        [HttpGet("active")]
        public async Task<ActionResult<ActiveSessionsResponse>> GetActiveSessions(ActiveSessionsRequest request)
        {

            try
            {
                var result = await _sessionService.GetActiveSessionsAsync(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Внутренняя ошибка сервера", Details = ex.Message });
            }
        }
    }
}
