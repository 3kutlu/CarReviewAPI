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

        [HttpGet("name={directorName}")]
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

        [HttpPut("{directorId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateDirector(int directorId, [FromBody] DirectorDto updatedDirector)
        {
            if (updatedDirector == null)
                return BadRequest(ModelState);

            if (directorId != updatedDirector.Id)
                return BadRequest(ModelState);

            if (!_directorRepository.DirectorExists(directorId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var directorMap = _mapper.Map<Director>(updatedDirector);

            if (!_directorRepository.UpdateDirector(directorMap))
            {
                ModelState.AddModelError("", "Something went wrong while updating director");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{directorId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteDirector(int directorId)
        {
            if (!_directorRepository.DirectorExists(directorId))
                return NotFound();

            var directorToDelete = _directorRepository.GetDirectorById(directorId);

            //It is necessary to check categories that are part of another models
            //but this is a simple database in order to learn stuff
            // so I am not gonna add so many validations to avoid conflicts

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_directorRepository.DeleteDirector(directorToDelete))
            {
                ModelState.AddModelError("", "Something went wrong while deleting director");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}
