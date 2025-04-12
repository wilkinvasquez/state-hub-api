using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SB.StateHub.Domain.Entities.Bases
{
    public class BaseEntity
    {
        public int? Id { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public DateTime? DeletionTime { get; set; }
        public bool IsDeleted { get; set; }
    }
}