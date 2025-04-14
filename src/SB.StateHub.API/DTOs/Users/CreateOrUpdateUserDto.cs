using SB.StateHub.API.DTOs.Bases;

namespace SB.StateHub.API.DTOs.Users
{
    public class CreateOrUpdateUserDto : BaseEntityDto
    {
        public string? Name { get; set; }
        public string? Lastname { get; set; }
        public string? Username { get; set; }
        public string? Mail { get; set; }
        public string? Phone { get; set; }
        public string? Password { get; set; }
    }
}