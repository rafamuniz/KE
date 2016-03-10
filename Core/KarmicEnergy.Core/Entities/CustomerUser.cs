using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KarmicEnergy.Core.Entities
{
    [Table("CustomerUsers", Schema = "dbo")]
    public class CustomerUser : BaseEntity
    {
        #region Property

        [Key, Column("Id", Order = 1, TypeName = "UNIQUEIDENTIFIER")]
        public Guid Id { get; set; }

        [Column("Name", TypeName = "NVARCHAR")]
        [StringLength(128)]
        [Required]
        public String Name { get; set; }

        [Column("Email", TypeName = "NVARCHAR")]
        [StringLength(256)]
        [Required]
        public String Email { get; set; }

        [Column("CustomerId", TypeName = "UNIQUEIDENTIFIER")]
        [Required]
        public Guid CustomerId { get; set; }

        #endregion Property
    }
}
