using SB.StateHub.API.DTOs.GovermentEntityTypes;
using SB.StateHub.API.DTOs.Pagination;
using SB.StateHub.API.Services.Bases;
using SB.StateHub.Domain.Entities.GovermentEntityTypes;

namespace SB.StateHub.API.Services.GovermentEntityTypes
{
    public interface IGovermentEntityTypeService : IBaseService<GovermentEntityType>
    {
        PaginationResponseDto<GovermentEntityTypeDto> GetAllPagedGovermentEntityTypes(PaginationDto parameters);
    }
}