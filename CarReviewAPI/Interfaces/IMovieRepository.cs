using MovieReviewAPI.Models;

namespace MovieReviewAPI.Interfaces
{
    public interface IMovieRepository
    {
        ICollection<Movie> GetMovies();
        Movie GetMovieById(int id);
        Movie GetMovieByName(string name);
        //decimal GetMovierating(int movieId);
        bool MovieExists(int id);
    }
}
