using API.DTOs;
using API.Extensions;
using API.Models;
using AutoMapper;

namespace API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AppUser, MemberDTO>()
                .ForMember(destination => destination.Age, option => option.MapFrom(source => source.DateOfBirth.CalculateAge()))
                .ForMember(property => property.PhotoURL, options => options.MapFrom(source => source.Photos.FirstOrDefault(x => x.IsMain)!.URL));
            CreateMap<Photo, PhotoDTO>();
        }
    }
}
