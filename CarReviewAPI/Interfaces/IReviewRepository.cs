using MovieReviewAPI.Models;

namespace MovieReviewAPI.Interfaces
{
    public interface IReviewRepository
    {
        ICollection<Review> GetReviews();
        Review GetReviewById(int reviewId);
        ICollection<Review> GetAllReviewsByMovieId(int movieId);
        ICollection<Review> GetAllReviewByReviewerId(int reviewerId);
        bool ReviewExists(int reviewId);
        bool CreateReview(Review review);
        bool UpdateReview(Review review);
        bool DeleteReview(Review review);
        bool Save();
    }
}
