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

        public ReviewController(IReviewRepository reviewRepository, IMapper mapper)
        {
            _reviewRepository = reviewRepository;
            _mapper = mapper;
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

    }
}
