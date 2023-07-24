using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MovieReviewAPI.Dto;
using MovieReviewAPI.Interfaces;
using MovieReviewAPI.Models;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace MovieReviewAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Category>))]
        public IActionResult GetCategories()
        {
            var categories = _mapper.Map<List<CategoryDto>>(_categoryRepository.GetCategories());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(categories);
        }

        [HttpGet("id={categoryId}")]
        [ProducesResponseType(200, Type = typeof(Category))]
        [ProducesResponseType(400)]
        public IActionResult GetCategoryById(int categoryId)
        {
            if (!_categoryRepository.CategoryExists(categoryId))
                return NotFound();

            var category = _mapper.Map<CategoryDto>(_categoryRepository.GetCategoryById(categoryId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(category);
        }

        [HttpGet("name={categoryName}")]
        [ProducesResponseType(200, Type = typeof(Category))]
        [ProducesResponseType(400)]
        public IActionResult GetCategoryByName(string categoryName)
        {
            var category = _mapper.Map<CategoryDto>(_categoryRepository.GetCategoryByName(categoryName));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(category);
        }


        [HttpGet("movie/{categoryId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Movie>))]
        [ProducesResponseType(400)]
        public IActionResult GetMovieByCategoryId(int categoryId)
        {
            if(!_categoryRepository.CategoryExists(categoryId))
                return NotFound();

            var movies = _mapper.Map<List<MovieDto>>(_categoryRepository.GetMoviesByCategory(categoryId));

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(movies);
        }


    }
}
