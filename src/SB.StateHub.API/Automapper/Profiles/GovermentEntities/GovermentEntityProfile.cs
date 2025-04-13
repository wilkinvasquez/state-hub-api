using AutoMapper;
using SB.StateHub.API.DTOs.GovermentEntities;
using SB.StateHub.Domain.Entities.GovermentEntities;

namespace SB.StateHub.API.Automapper.Profiles.GovermentEntities
{
    public class GovermentEntityProfile : Profile
    {
         public GovermentEntityProfile()
        {
            CreateMap<GovermentEntityDto, GovermentEntity>().ReverseMap();
            CreateMap<CreateOrUpdateGovermentEntityDto, GovermentEntity>().ReverseMap();
        }
    }
}