using SB.StateHub.API.DTOs.Bases;

namespace SB.StateHub.API.DTOs.GovermentEntityTypes
{
    public class CreateOrUpdateGovermentEntityTypeDto : BaseEntityDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}