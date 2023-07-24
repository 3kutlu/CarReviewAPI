using MovieReviewAPI.Models;

namespace MovieReviewAPI.Interfaces
{
    public interface IMovieRepository
    {
        ICollection<Movie> GetMovies();
        Movie GetMovieById(int id);
        Movie GetMovieByName(string name);
        bool MovieExists(int id);
        decimal GetMovieRating(int movieId);
        bool CreateMovie(int directorId, int categoryId, Movie movie);
        bool UpdateMovie(int directorId, int categoryId, Movie movie);
        bool DeleteMovie(Movie movie);
        bool Save();
    }
}
