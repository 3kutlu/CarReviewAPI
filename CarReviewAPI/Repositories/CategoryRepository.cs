using MovieReviewAPI.Data;
using MovieReviewAPI.Interfaces;
using MovieReviewAPI.Models;

namespace MovieReviewAPI.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DataContext _context;

        public CategoryRepository(DataContext context)
        {
            _context = context;
        }

        public bool CategoryExists(int categoryId)
        {
            return _context.Categories.Any(c => c.Id == categoryId);
        }

        public ICollection<Category> GetCategories()
        {
            return _context.Categories
                .OrderBy(c => c.Id)
                .ToList();
        }

        public Category GetCategoryById(int categoryId)
        {
            return _context.Categories
                .Where(c => c.Id == categoryId)
                .FirstOrDefault();
        }

        public Category GetCategoryByName(string categoryName)
        {
            return _context.Categories
                .Where(c => c.Name == categoryName)
                .FirstOrDefault();
        }

        public ICollection<Movie> GetMoviesByCategory(int categoryId)
        {
            return _context.MovieCategories
                .Where(c => c.CategoryId == categoryId)
                .Select(m => m.Movie).ToList();
        }
    }
}
