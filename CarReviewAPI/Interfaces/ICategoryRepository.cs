using MovieReviewAPI.Models;

namespace MovieReviewAPI.Interfaces
{
    public interface ICategoryRepository
    {
        ICollection<Category> GetCategories();
        Category GetCategoryById(int categoryId);
        Category GetCategoryByName(string categoryName);
        ICollection<Movie> GetMoviesByCategory(int categoryId);
        bool CategoryExists(int categoryId);
        bool CreateCategory(Category category);
        bool UpdateCategory(Category category);
        bool DeleteCategory(Category category);
        bool Save();

    }
}
