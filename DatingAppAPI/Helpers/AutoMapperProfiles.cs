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
                .ForMember(destinationMember => destinationMember.Age, option => option.MapFrom(source => source.DateOfBirth.CalculateAge()))
                .ForMember(destinationMember => destinationMember.PhotoURL, options => options.MapFrom(source => source.Photos.FirstOrDefault(x => x.IsMain)!.URL));
            CreateMap<Photo, PhotoDTO>();
            CreateMap<MemberUpdateDTO, AppUser>();
        }
    }
}
