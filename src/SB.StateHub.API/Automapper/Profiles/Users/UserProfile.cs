using AutoMapper;
using SB.StateHub.API.DTOs.Users;
using SB.StateHub.Domain.Entities.Users;

namespace SB.StateHub.API.Automapper.Profiles.Users
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserDto, User>().ReverseMap();
            CreateMap<CreateOrUpdateUserDto, User>().ReverseMap();
        }
    }
}