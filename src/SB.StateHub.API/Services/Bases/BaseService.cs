using AutoMapper;
using SB.StateHub.Domain.Entities.Bases;
using SB.StateHub.Domain.Repositories.Bases;

namespace SB.StateHub.API.Services.Bases
{
    public class BaseService<T> : IBaseService<T> where T : BaseEntity
    {
        protected readonly IBaseRepository<T> _baseRepository;
        protected readonly IMapper _mapper;

        public BaseService(IBaseRepository<T> baseRepository, IMapper mapper)
        {
            _baseRepository = baseRepository;
            _mapper = mapper;
        }

        public async Task<D> GetByIdAsync<D>(int id)
        {
            T? entity = await _baseRepository.GetByIdAsync(id);
            D entityDto = _mapper.Map<D>(entity);

            return entityDto;
        }

        public IEnumerable<D> GetAll<D>()
        {
            IEnumerable<T> entities = _baseRepository.GetAll();
            IEnumerable<D> entitiesDto = _mapper.Map<IEnumerable<D>>(entities);

            return entitiesDto;
        }

        public async Task<D> CreateOrUpdateAsync<D>(D dto)
        {
            T entity = _mapper.Map<T>(dto);

            entity = await _baseRepository.CreateOrUpdateAsync(entity);

            return _mapper.Map<D>(entity);
        }

        public async Task DeleteAsync(int id)
        {
            await _baseRepository.DeleteAsync(id);
        }
    }
}