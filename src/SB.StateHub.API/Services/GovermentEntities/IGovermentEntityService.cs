using SB.StateHub.API.DTOs.GovermentEntities;
using SB.StateHub.API.DTOs.Pagination;
using SB.StateHub.API.Services.Bases;
using SB.StateHub.Domain.Entities.GovermentEntities;

namespace SB.StateHub.API.Services.GovermentEntities
{
    public interface IGovermentEntityService : IBaseService<GovermentEntity>
    {
        Task<GovermentEntityDto> GetByIdAsync(int id);
        IEnumerable<GovermentEntityDto> GetAll();
        PaginationResponseDto<GovermentEntityDto> GetAllPagedGovermentEntities(PaginationDto parameters);
    }
}