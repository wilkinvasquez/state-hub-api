using System.Linq.Expressions;
using AutoMapper;
using SB.StateHub.API.DTOs.GovermentEntities;
using SB.StateHub.API.DTOs.Pagination;
using SB.StateHub.API.Services.Bases;
using SB.StateHub.Domain.Entities.GovermentEntities;
using SB.StateHub.Domain.Repositories.Bases;

namespace SB.StateHub.API.Services.GovermentEntities
{
    public class GovermentEntityService : BaseService<GovermentEntity>, IGovermentEntityService
    {
        public GovermentEntityService(IBaseRepository<GovermentEntity> baseRepository, IMapper mapper) : base(baseRepository, mapper)
        {

        }

        public PaginationResponseDto<GovermentEntityDto> GetAllPagedGovermentEntities(PaginationDto parameters)
        {
            string filter = parameters.Filter.ToLower().Trim();

            Expression<Func<GovermentEntity, bool>> filters = p =>
                p.Name!.ToLower().Trim().Contains(filter) ||
                p.Description!.ToLower().Trim().Contains(filter) ||
                parameters.Filter.Trim() == "";

            IEnumerable<GovermentEntity> govermentEntitiesCollection = _baseRepository
                .GetAllPaged(parameters.PageNumber, parameters.PageSize, filters);

            IEnumerable<GovermentEntityDto> govermentEntities = _mapper.Map<IEnumerable<GovermentEntityDto>>(govermentEntitiesCollection);

            PaginationResponseDto<GovermentEntityDto> paginationResponse = new PaginationResponseDto<GovermentEntityDto>
            {
                Items = govermentEntities,
                Total = _baseRepository.GetAll(filters).Count()
            };

            return paginationResponse;
        }
    }
}