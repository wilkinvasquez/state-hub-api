using SB.StateHub.API.DTOs.Bases;

namespace SB.StateHub.API.DTOs.GovermentEntities
{
    public class CreateOrUpdateGovermentEntityDto : BaseEntityDto
    {
        public int? EntityTypeId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Acronym { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
    }
}