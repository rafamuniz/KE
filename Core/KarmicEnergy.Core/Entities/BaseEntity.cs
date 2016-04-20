using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KarmicEnergy.Core.Entities
{
    public abstract class BaseEntity
    {
        [Timestamp]
        public Byte[] RowVersion { get; set; }

        [Column("CreatedDate", TypeName = "DATETIME")]
        [Required]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        [Column("DeletedDate", TypeName = "DATETIME")]        
        public DateTime? DeletedDate { get; set; }
    }
}
