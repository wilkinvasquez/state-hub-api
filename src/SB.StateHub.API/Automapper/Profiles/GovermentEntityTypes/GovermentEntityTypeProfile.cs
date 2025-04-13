using AutoMapper;
using SB.StateHub.API.DTOs.GovermentEntityTypes;
using SB.StateHub.Domain.Entities.GovermentEntityTypes;

namespace SB.StateHub.API.Automapper.Profiles.GovermentEntityTypes
{
    public class GovermentEntityTypeProfile : Profile
    {
        public GovermentEntityTypeProfile()
        {
            CreateMap<GovermentEntityTypeDto, GovermentEntityType>().ReverseMap();
            CreateMap<CreateOrUpdateGovermentEntityTypeDto, GovermentEntityType>().ReverseMap();
        }
    }
}