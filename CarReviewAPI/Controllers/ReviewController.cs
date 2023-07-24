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
    public class ReviewController : Controller
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;
        private readonly IMovieRepository _movieRepository;
        private readonly IReviewerRepository _reviewerRepository;

        public ReviewController(IReviewRepository reviewRepository, 
            IMapper mapper, 
            IMovieRepository movieRepository,
            IReviewerRepository reviewerRepository)
        {
            _reviewRepository = reviewRepository;
            _mapper = mapper;
            _movieRepository = movieRepository;
            _reviewerRepository = reviewerRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Review>))]
        public IActionResult GetReviews()
        {
            var reviews = _mapper.Map<List<ReviewDto>>(_reviewRepository.GetReviews());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(reviews);
        }

        [HttpGet("{reviewId}")]
        [ProducesResponseType(200, Type = typeof(Review))]
        [ProducesResponseType(400)]
        public IActionResult GetReviewById(int reviewId)
        {
            if (!_reviewRepository.ReviewExists(reviewId))
                return NotFound();

            var review = _mapper.Map<ReviewDto>(_reviewRepository.GetReviewById(reviewId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(review);
        }

        [HttpGet("movie/{movieId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Review>))]
        [ProducesResponseType(400)]
        public IActionResult GetAllReviewsByMovieId(int movieId)
        {
            var reviews = _mapper.Map<List<ReviewDto>>(_reviewRepository.GetAllReviewsByMovieId(movieId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(reviews);
        }

        [HttpGet("reviewer/{reviewerId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Review>))]
        [ProducesResponseType(400)]
        public IActionResult GetAllReviewsByReviewerId(int reviewerId)
        {
            var reviews = _mapper.Map<List<ReviewDto>>(_reviewRepository.GetAllReviewByReviewerId(reviewerId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(reviews);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateReview([FromQuery] int reviewerId, [FromQuery] int movieId, [FromBody] ReviewDto reviewCreate)
        {
            if (reviewCreate == null)
                return BadRequest(ModelState);

            var reviews = _reviewRepository.GetReviews()
                .Where(r => r.Text.Trim().ToUpper() == reviewCreate.Text.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (reviews != null)
            {
                ModelState.AddModelError("", "Review already exists!");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var reviewMap = _mapper.Map<Review>(reviewCreate);

            reviewMap.Movie = _movieRepository.GetMovieById(movieId);
            reviewMap.Reviewer = _reviewerRepository.GetReviewerById(reviewerId);


            if (!_reviewRepository.CreateReview(reviewMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving!");
                return StatusCode(500, ModelState);
            }

            return Ok("Review successfully created");
        }

        [HttpPut("{reviewId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateReview(int reviewId, [FromBody] ReviewDto updatedReview)
        {
            if (updatedReview == null)
                return BadRequest(ModelState);

            if (reviewId != updatedReview.Id)
                return BadRequest(ModelState);

            if (!_reviewRepository.ReviewExists(reviewId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var reviewMap = _mapper.Map<Review>(updatedReview);

            if (!_reviewRepository.UpdateReview(reviewMap))
            {
                ModelState.AddModelError("", "Something went wrong while updating review");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{reviewId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteReview(int reviewId)
        {
            if (!_reviewRepository.ReviewExists(reviewId))
                return NotFound();

            var reviewToDelete = _reviewRepository.GetReviewById(reviewId);

            //It is necessary to check categories that are part of another models
            //but this is a simple database in order to learn stuff
            // so I am not gonna add so many validations to avoid conflicts

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_reviewRepository.DeleteReview(reviewToDelete))
            {
                ModelState.AddModelError("", "Something went wrong while deleting review");
                return StatusCode(500, ModelState);
            }
            return NoContent();

        }


    }
}
