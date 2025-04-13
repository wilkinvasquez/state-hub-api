using SB.StateHub.API.DTOs.Bases;

namespace SB.StateHub.API.DTOs.GovermentEntityTypes
{
    public class GovermentEntityTypeDto : BaseEntityDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}