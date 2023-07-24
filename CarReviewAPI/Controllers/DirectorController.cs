using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MovieReviewAPI.Dto;
using MovieReviewAPI.Interfaces;
using MovieReviewAPI.Models;
using MovieReviewAPI.Repositories;

namespace MovieReviewAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DirectorController : Controller
    {
        private readonly IDirectorRepository _directorRepository;
        private readonly IMapper _mapper;

        public DirectorController(IDirectorRepository directorRepository, IMapper mapper)
        {
            _directorRepository = directorRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Director>))]
        public IActionResult GetDirectors()
        {
            var directors = _mapper.Map<List<DirectorDto>>(_directorRepository.GetDirectors());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(directors);
        }

        [HttpGet("id={directorId}")]
        [ProducesResponseType(200, Type = typeof(Director))]
        [ProducesResponseType(400)]
        public IActionResult GetDirectorById(int directorId)
        {
            if (!_directorRepository.DirectorExists(directorId))
                return NotFound();

            var director = _mapper.Map<DirectorDto>(_directorRepository.GetDirectorById(directorId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(director);
        }

        [HttpGet("name={directorId}")]
        [ProducesResponseType(200, Type = typeof(Director))]
        [ProducesResponseType(400)]
        public IActionResult GetDirectorByName(string directorName)
        {
            var director = _mapper.Map<DirectorDto>(_directorRepository.GetDirectorByName(directorName));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(director);
        }

        [HttpGet("{directorId}/movies")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Movie>))]
        [ProducesResponseType(400)]
        public IActionResult GetMoviesByDirectorId(int directorId)
        {
            if (!_directorRepository.DirectorExists(directorId))
                return NotFound();

            var movies = _mapper.Map<List<MovieDto>>(_directorRepository.GetMoviesByDirectorId(directorId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(movies);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateDirector([FromBody] DirectorDto directorCreate)
        {
            if (directorCreate == null)
                return BadRequest(ModelState);

            var director = _directorRepository.GetDirectors()
                .Where(d => d.LastName.Trim().ToUpper() == directorCreate.LastName.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (director != null)
            {
                ModelState.AddModelError("", "Director already exists!");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var directorMap = _mapper.Map<Director>(directorCreate);

            if (!_directorRepository.CreateDirector(directorMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving!");
                return StatusCode(500, ModelState);
            }

            return Ok("Director successfully created");
        }




    }
}
