using SB.StateHub.Domain.Entities.Bases;

namespace SB.StateHub.Domain.Entities.EntityTypes
{
    public class EntityType : BaseEntity
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Acronym { get; set; }
    }
}