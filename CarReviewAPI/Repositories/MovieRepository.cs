using MovieReviewAPI.Data;
using MovieReviewAPI.Interfaces;
using MovieReviewAPI.Models;

namespace MovieReviewAPI.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly DataContext _context;

        public MovieRepository(DataContext context)
        {
            _context = context;
        }

        public Movie GetMovieById(int movieId)
        {
            return _context.Movies
                .Where(m => m.Id == movieId)
                .FirstOrDefault();
        }

        public Movie GetMovieByName(string movieName)
        {
            return _context.Movies
                .Where(m => m.Name == movieName)
                .FirstOrDefault();
        }

        public ICollection<Movie> GetMovies()
        {
            return _context.Movies
                .OrderBy(m => m.Id)
                .ToList();
        }

        //public decimal GetMovieRating(int movieId)
        //{
        //}

        public bool MovieExists(int id) 
        { 
            return _context.Movies
                .Any(m => m.Id == id);
        }
    }
}
