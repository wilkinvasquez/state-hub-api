using SB.StateHub.Domain.Entities.Bases;
using SB.StateHub.Domain.Entities.GovermentEntityTypes;

namespace SB.StateHub.Domain.Entities.GovermentEntities
{
    public class GovermentEntity : BaseEntity
    {
        public int? EntityTypeId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Acronym { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }

        public GovermentEntityType? EntityType { get; set; }
    }
}