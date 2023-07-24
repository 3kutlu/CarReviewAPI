using MovieReviewAPI.Models;

namespace MovieReviewAPI.Interfaces
{
    public interface IReviewerRepository
    {
        ICollection<Reviewer> GetReviewers();
        Reviewer GetReviewerById(int reviewerId);
        bool ReviewerExists(int reviewerId);
        bool CreateReviewer(Reviewer reviewer);
        bool UpdateReviewer(Reviewer reviewer);
        bool DeleteReviewer(Reviewer reviewer);
        bool Save();
    }
}
