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
            CreateMap<Category, CategoryDto>();
            CreateMap<Director, DirectorDto>();
            CreateMap<Review, ReviewDto>();
        }
    }
}
