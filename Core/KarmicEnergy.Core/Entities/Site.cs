using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KarmicEnergy.Core.Entities
{
    [Table("Sites", Schema = "dbo")]
    public class Site : BaseEntity
    {
        #region Property

        [Key, Column("Id", Order = 1, TypeName = "UNIQUEIDENTIFIER")]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Column("Name", TypeName = "NVARCHAR")]
        [StringLength(128)]
        [Required]
        public String Name { get; set; }

        [Column("IPAddress", TypeName = "NVARCHAR")]
        [StringLength(64)]
        [Required]
        public String IPAddress { get; set; }

        [Column("CustomerId", TypeName = "UNIQUEIDENTIFIER")]
        [Required]
        public Guid CustomerId { get; set; }

        #endregion Property
    }
}
