using SB.StateHub.API.DTOs.Bases;
using SB.StateHub.API.DTOs.GovermentEntityTypes;

namespace SB.StateHub.API.DTOs.GovermentEntities
{
    public class GovermentEntityDto : BaseEntityDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Acronym { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }

        public GovermentEntityTypeDto? EntityType { get; set; }
    }
}