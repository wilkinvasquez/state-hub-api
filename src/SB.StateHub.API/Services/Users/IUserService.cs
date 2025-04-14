using SB.StateHub.API.DTOs.Pagination;
using SB.StateHub.API.DTOs.Users;
using SB.StateHub.API.Services.Bases;
using SB.StateHub.Domain.Entities.Users;

namespace SB.StateHub.API.Services.Users
{
    public interface IUserService : IBaseService<User>
    {
        PaginationResponseDto<UserDto> GetAllPagedUsers(PaginationDto parameters);
    }
}