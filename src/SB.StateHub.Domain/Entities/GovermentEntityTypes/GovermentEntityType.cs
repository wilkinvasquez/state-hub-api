using SB.StateHub.Domain.Entities.Bases;

namespace SB.StateHub.Domain.Entities.GovermentEntityTypes
{
    public class GovermentEntityType : BaseEntity
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}