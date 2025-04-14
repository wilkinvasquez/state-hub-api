using System.Linq.Expressions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SB.StateHub.API.DTOs.Authentications;
using SB.StateHub.API.DTOs.Pagination;
using SB.StateHub.API.DTOs.Users;
using SB.StateHub.API.Services.Bases;
using SB.StateHub.API.Services.Cryptography;
using SB.StateHub.Domain.Entities.Users;
using SB.StateHub.Domain.Repositories.Bases;

namespace SB.StateHub.API.Services.Users
{
    public class UserService : BaseService<User>, IUserService
    {
        private readonly ICryptoService _cryptoService;

        public UserService(
            IBaseRepository<User> baseRepository,
            IMapper mapper,
            ICryptoService cryptoService
        ) : base(baseRepository, mapper)
        {
            _cryptoService = cryptoService;
        }

        public PaginationResponseDto<UserDto> GetAllPagedUsers(PaginationDto parameters)
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

        public async Task<UserDto> CreateOrUpdateUserAsync(CreateOrUpdateUserDto userDto)
        {
            User user = _mapper.Map<User>(userDto);

            if (user.Id == null)
            {
                user.Password = _cryptoService.GenerateSHA256(user.Password);
            }

            User createdOrUpdatedUser = await _baseRepository.CreateOrUpdateAsync(user);
            UserDto createdUserDto = _mapper.Map<UserDto>(createdOrUpdatedUser);

            return createdUserDto;
        }

        public async Task<AuthenticationResultDto> AuthenticateAsync(AuthenticationDto authentication)
        {
            string password = _cryptoService.GenerateSHA256(authentication.Password!);

            User? user = await _baseRepository
                .GetAll()
                .FirstOrDefaultAsync(usr => (usr.Username == authentication.Username || usr.Mail == authentication.Username) && usr.Password == password);

            AuthenticationResultDto result = new AuthenticationResultDto();

            if (user != null)
            {
                result.Username = user!.Username;
               
                result.Token = _cryptoService.GenerateAuthenticationToken(user.Id);
            }

            return result;
        }

    }
}