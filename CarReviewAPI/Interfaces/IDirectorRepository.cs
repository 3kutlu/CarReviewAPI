using MovieReviewAPI.Models;

namespace MovieReviewAPI.Interfaces
{
    public interface IDirectorRepository
    {
        ICollection<Director> GetDirectors();
        Director GetDirectorById(int directorId);
        Director GetDirectorByName(string directorName);
        ICollection<Director> GetDirectorByMovieId(int movieId);
        ICollection<Movie> GetMoviesByDirectorId(int directorId);
        bool DirectorExists(int directorId);
    }
}
