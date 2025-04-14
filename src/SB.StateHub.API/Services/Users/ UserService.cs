using System.Linq.Expressions;
using AutoMapper;
using SB.StateHub.API.DTOs.Pagination;
using SB.StateHub.API.DTOs.Users;
using SB.StateHub.API.Services.Bases;
using SB.StateHub.Domain.Entities.Users;
using SB.StateHub.Domain.Repositories.Bases;

namespace SB.StateHub.API.Services.Users
{
    public class UserService : BaseService<User>, IUserService
    {
        public UserService(IBaseRepository<User> baseRepository, IMapper mapper) : base(baseRepository, mapper)
        {
            
        }

        public PaginationResponseDto<UserDto> GetAllPagedUsers (PaginationDto parameters)
        {
            string filter = parameters.Filter.ToLower().Trim();

            Expression<Func<User, bool>> filters = p =>
                p.Name!.ToLower().Trim().Contains(filter) ||
                p.Lastname!.ToLower().Trim().Contains(filter) ||
                p.Username!.ToLower().Trim().Contains(filter) ||
                parameters.Filter.Trim() == "";

            IEnumerable<User> usersCollection = _baseRepository
                .GetAllPaged(parameters.PageNumber, parameters.PageSize, filters);

            IEnumerable<UserDto> users = _mapper.Map<IEnumerable<UserDto>>(usersCollection);

            PaginationResponseDto<UserDto> paginationResponse = new PaginationResponseDto<UserDto>
            {
                Items = users,
                Total = _baseRepository.GetAll(filters).Count()
            };

            return paginationResponse;
        }
    }
}