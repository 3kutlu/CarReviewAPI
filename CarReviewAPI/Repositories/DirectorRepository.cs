using MovieReviewAPI.Data;
using MovieReviewAPI.Interfaces;
using MovieReviewAPI.Models;
using System.IO;

namespace MovieReviewAPI.Repositories
{
    public class DirectorRepository : IDirectorRepository
    {
        private readonly DataContext _context;

        public DirectorRepository(DataContext context)
        {
            _context = context;
        }
        public bool DirectorExists(int directorId)
        {
            return _context.Directors.Any(d => d.Id == directorId);
        }

        public Director GetDirectorById(int directorId)
        {
            return _context.Directors
                .Where(d => d.Id == directorId)
                .FirstOrDefault();
        }

        public ICollection<Director> GetDirectorByMovieId(int movieId)
        {
            return _context.MovieDirectors
                .Where(m => m.MovieId == movieId)
                .Select(d => d.Director).ToList();
        }

        public Director GetDirectorByName(string directorName)
        {
            return _context.Directors
                .Where(d => d.FirstName == directorName)
                .FirstOrDefault();
        }

        public ICollection<Director> GetDirectors()
        {
            return _context.Directors
                .OrderBy(d => d.Id)
                .ToList();
        }

        public ICollection<Movie> GetMoviesByDirectorId(int directorId)
        {
            return _context.MovieDirectors
                .Where(d => d.DirectorId == directorId)
                .Select(m => m.Movie).ToList();
        }
    }
}
