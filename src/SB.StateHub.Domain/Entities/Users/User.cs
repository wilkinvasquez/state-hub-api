using SB.StateHub.Domain.Entities.Bases;

namespace SB.StateHub.Domain.Entities.Users
{
    public class User : BaseEntity
    {
        public string? Name { get; set; }
        public string? Lastname { get; set; }
        public string? Username { get; set; }
        public string? Mail { get; set; }
        public string? Phone { get; set; }
        public string? Password { get; set; }
    }
}