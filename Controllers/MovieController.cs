using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using MovieServiceAPI.Services;

namespace MovieServiceAPI.Controllers
{
    [Route("api/movies")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly TmdbService _tmdbService;

        public MovieController(TmdbService tmdbService)
        {
            _tmdbService = tmdbService;
        }

        [HttpGet("{title}")]
        public async Task<IActionResult> GetMovie(string title)
        {
            var movie = await _tmdbService.GetMovieByTitleAsync(title);
            if (movie == null)
                return NotFound("Película no encontrada.");

            return Ok(movie);
        }
    }
}