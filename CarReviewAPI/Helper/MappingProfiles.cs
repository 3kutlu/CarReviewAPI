using AutoMapper;
using MovieReviewAPI.Dto;
using MovieReviewAPI.Models;

namespace MovieReviewAPI.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Movie, MovieDto>(); 
            CreateMap<MovieDto, Movie>();

            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, Category>();

            CreateMap<Director, DirectorDto>();
            CreateMap<DirectorDto, Director>();

            CreateMap<Review, ReviewDto>();
            CreateMap<ReviewDto, Review>();

            CreateMap<Reviewer, ReviewerDto>();
            CreateMap<ReviewerDto, Reviewer>();
        }
    }
}
