using System.Linq.Expressions;
using AutoMapper;
using SB.StateHub.API.DTOs.GovermentEntityTypes;
using SB.StateHub.API.DTOs.Pagination;
using SB.StateHub.API.Services.Bases;
using SB.StateHub.Domain.Entities.GovermentEntityTypes;
using SB.StateHub.Domain.Repositories.Bases;

namespace SB.StateHub.API.Services.GovermentEntityTypes
{
    public class GovermentEntityTypeService : BaseService<GovermentEntityType>, IGovermentEntityTypeService
    {
        public GovermentEntityTypeService(IBaseRepository<GovermentEntityType> baseRepository, IMapper mapper) : base(baseRepository, mapper)
        {

        }

        public PaginationResponseDto<GovermentEntityTypeDto> GetAllPagedGovermentEntityTypes(PaginationDto parameters)
        {
            string filter = parameters.Filter.ToLower().Trim();

            Expression<Func<GovermentEntityType, bool>> filters = p =>
                p.Name!.ToLower().Trim().Contains(filter) ||
                p.Description!.ToLower().Trim().Contains(filter) ||
                parameters.Filter.Trim() == "";

            IEnumerable<GovermentEntityType> govermentEntitiesCollection = _baseRepository
                .GetAllPaged(parameters.PageNumber, parameters.PageSize, filters);

            IEnumerable<GovermentEntityTypeDto> govermentEntities = _mapper.Map<IEnumerable<GovermentEntityTypeDto>>(govermentEntitiesCollection);

            PaginationResponseDto<GovermentEntityTypeDto> paginationResponse = new PaginationResponseDto<GovermentEntityTypeDto>
            {
                Items = govermentEntities,
                Total = _baseRepository.GetAll(filters).Count()
            };

            return paginationResponse;
        }
    }
}