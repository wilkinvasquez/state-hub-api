using System.Linq.Expressions;
using AutoMapper;
using SB.StateHub.API.DTOs.GovermentEntities;
using SB.StateHub.API.DTOs.Pagination;
using SB.StateHub.API.Services.Bases;
using SB.StateHub.Domain.Entities.GovermentEntities;
using SB.StateHub.Domain.Repositories.Bases;
using SB.StateHub.Domain.Repositories.GovermentEntities;

namespace SB.StateHub.API.Services.GovermentEntities
{
    public class GovermentEntityService : BaseService<GovermentEntity>, IGovermentEntityService
    {
        private readonly IGovermentEntityRepository _govermentEntityRepository;

        public GovermentEntityService(
            IBaseRepository<GovermentEntity> baseRepository,
            IMapper mapper,
            IGovermentEntityRepository govermentEntityRepository
        ) : base(baseRepository, mapper)
        {
            _govermentEntityRepository = govermentEntityRepository;
        }

        public async Task<GovermentEntityDto> GetByIdAsync(int id)
        {
            GovermentEntity? govermentEntity = await _govermentEntityRepository.GetByIdAsync(id);
            GovermentEntityDto govermentEntityDto = _mapper.Map<GovermentEntityDto>(govermentEntity);

            return govermentEntityDto;
        }

        public IEnumerable<GovermentEntityDto> GetAll()
        {
            IEnumerable<GovermentEntity> govermentEntities = _govermentEntityRepository.GetAll();
            IEnumerable<GovermentEntityDto> govermentEntitiesDto = _mapper.Map<IEnumerable<GovermentEntityDto>>(govermentEntities);

            return govermentEntitiesDto;
        }

        public PaginationResponseDto<GovermentEntityDto> GetAllPagedGovermentEntities(PaginationDto parameters)
        {
            string filter = parameters.Filter.ToLower().Trim();

            Expression<Func<GovermentEntity, bool>> filters = p =>
                p.Name!.ToLower().Trim().Contains(filter) ||
                p.Description!.ToLower().Trim().Contains(filter) ||
                p.Acronym!.ToLower().Trim().Contains(filter) ||
                p.Address!.ToLower().Trim().Contains(filter) ||
                p.Phone!.ToLower().Trim().Contains(filter) ||
                parameters.Filter.Trim() == "";

            IEnumerable<GovermentEntity> govermentEntitiesCollection = _govermentEntityRepository
                .GetAllPaged(parameters.PageNumber, parameters.PageSize, filters);

            IEnumerable<GovermentEntityDto> govermentEntities = _mapper.Map<IEnumerable<GovermentEntityDto>>(govermentEntitiesCollection);

            PaginationResponseDto<GovermentEntityDto> paginationResponse = new PaginationResponseDto<GovermentEntityDto>
            {
                Items = govermentEntities,
                Total = _govermentEntityRepository
                    .GetAll(filters)
                    .Count()
            };

            return paginationResponse;
        }
    }
}