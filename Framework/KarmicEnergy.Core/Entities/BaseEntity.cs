using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KarmicEnergy.Core.Entities
{
    public abstract class BaseEntity
    {
        [Timestamp]
        public Byte[] RowVersion { get; set; }

        [NotMapped]
        public DateTime RowVersionDate
        {
            get
            {
                long longVar = BitConverter.ToInt64(RowVersion, 0);
                DateTime dateTimeVar = new DateTime(1980, 1, 1).AddMilliseconds(longVar);
                return dateTimeVar;
            }
        }

        [Column("CreatedDate", TypeName = "DATETIME")]
        [Required]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        [Column("DeletedDate", TypeName = "DATETIME")]        
        public DateTime? DeletedDate { get; set; }
    }
}
