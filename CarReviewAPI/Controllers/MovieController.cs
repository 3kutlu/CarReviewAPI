using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MovieReviewAPI.Dto;
using MovieReviewAPI.Interfaces;
using MovieReviewAPI.Models;
using MovieReviewAPI.Repositories;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace MovieReviewAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : Controller
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IMapper _mapper;

        public MovieController(IMovieRepository movieRepository, IMapper mapper) 
        {
            _movieRepository = movieRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type =typeof(IEnumerable<Movie>))]
        public IActionResult GetMovies()
        {
            var movies = _mapper.Map<List<MovieDto>>(_movieRepository.GetMovies());

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(movies);
        }

        [HttpGet("id={movieId}")]
        [ProducesResponseType(200, Type = typeof(Movie))]
        [ProducesResponseType(400)]
        public IActionResult GetMovieById(int movieId)
        {
            if (!_movieRepository.MovieExists(movieId)) 
                return NotFound();

            var movie = _mapper.Map<MovieDto>(_movieRepository.GetMovieById(movieId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(movie);
        }

        [HttpGet("name={movieName}")]
        [ProducesResponseType(200, Type = typeof(Movie))]
        [ProducesResponseType(400)]
        public IActionResult GetMovieByName(string movieName)
        {
            var movie = _mapper.Map<MovieDto>(_movieRepository.GetMovieByName(movieName));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(movie);
        }

        [HttpGet("{movieId}/rating")]
        [ProducesResponseType(200, Type = typeof(decimal))]
        [ProducesResponseType(400)]
        public IActionResult GetMovieRating(int movieId)
        {
            if (!_movieRepository.MovieExists(movieId))
                return NotFound();

            var rating = _movieRepository.GetMovieRating(movieId);

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(rating);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateMovie([FromQuery] int directorId,[FromQuery] int categoryId, [FromBody] MovieDto movieCreate)
        {
            if (movieCreate == null)
                return BadRequest(ModelState);

            var movies = _movieRepository.GetMovies()
                .Where(m => m.Name.Trim().ToUpper() == movieCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (movies!= null)
            {
                ModelState.AddModelError("", "Movie already exists!");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var movieMap= _mapper.Map<Movie>(movieCreate);

            if (!_movieRepository.CreateMovie(directorId, categoryId, movieMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving!");
                return StatusCode(500, ModelState);
            }

            return Ok("Movie successfully created");
        }
    }
}
