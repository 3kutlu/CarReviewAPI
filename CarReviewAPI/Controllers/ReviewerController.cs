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
    public class ReviewerController : Controller
    {
        private readonly IReviewerRepository _reviewerRepository;
        private readonly IMapper _mapper;

        public ReviewerController(IReviewerRepository reviewerRepository, IMapper mapper)
        {
            _reviewerRepository = reviewerRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Reviewer>))]
        public IActionResult GetReviewers()
        {
            var reviewers = _mapper.Map<List<ReviewerDto>>(_reviewerRepository.GetReviewers());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(reviewers);
        }

        [HttpGet("reviewerId")]
        [ProducesResponseType(200, Type = typeof(Reviewer))]
        [ProducesResponseType(400)]
        public IActionResult GetReviewerById(int reviewerId) 
        {
            if (!_reviewerRepository.ReviewerExists(reviewerId))
                return NotFound();

            var reviewer = _mapper.Map<ReviewerDto>(_reviewerRepository.GetReviewerById(reviewerId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(reviewer);
        }
    }
}
