using Cinema.Interfaces;
using Cinema.Models.Request;
using Cinema.Models.Response;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.Controllers
{
    [ApiController]
    [Route("Films")]
    public class FilmsController(IFilmService filmService) : ControllerBase
    {
        private readonly IFilmService _filmService = filmService;

        [HttpGet("get")]
        public async Task<ActionResult<IEnumerable<FilmsGetResponse>>> GetAllFilms()
        {
            var response = await _filmService.GetAllFilmsAsync();
            return Ok(response);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateFilm(FilmUploadRequest film)
        {
            var response = await _filmService.CreateFilmAsync(film);
            return Ok(response);
        }

        [HttpPost("remove")]
        public async Task<IActionResult> DeleteFilm([FromQuery]int filmId)
        {
            var response = await _filmService.DeleteFilmAsync(filmId);
            return Ok(response);
        }

        [HttpPost("search")]
        public async Task<ActionResult<List<FilmSearchResult>>> SearchFilms(FilmSearchRequest request)
        {
            try
            {
                var result = await _filmService.SearchFilmsAsync(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ошибка при поиске фильмов: {ex.Message}");
            }
        }
    }
}
