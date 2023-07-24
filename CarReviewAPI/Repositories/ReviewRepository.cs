using AutoMapper;
using MovieReviewAPI.Data;
using MovieReviewAPI.Interfaces;
using MovieReviewAPI.Models;

namespace MovieReviewAPI.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly DataContext _context;
        public ReviewRepository(DataContext context)
        {
            _context = context;
        }
        public bool CreateReview(Review review)
        {
            _context.Add(review);
            return Save();
        }

        public bool DeleteReview(Review review)
        {
            _context.Remove(review);
            return Save();
        }

        public ICollection<Review> GetAllReviewByReviewerId(int reviewerId)
        {
            return _context.Reviews.Where(r => r.Reviewer.Id == reviewerId).ToList();
        }

        public ICollection<Review> GetAllReviewsByMovieId(int movieId)
        {
            return _context.Reviews.Where(r => r.Movie.Id == movieId).ToList(); 
        }

        public Review GetReviewById(int reviewId)
        {
            return _context.Reviews.Where(r => r.Id == reviewId).FirstOrDefault();
        }

        public ICollection<Review> GetReviews()
        {
            return _context.Reviews.OrderBy(r => r.Id).ToList();
        }

        public bool ReviewExists(int reviewId)
        {
            return _context.Reviews.Any(r => r.Id == reviewId);
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateReview(Review review)
        {
            _context.Update(review);
            return Save();
        }
    }
}
