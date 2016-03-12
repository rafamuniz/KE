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

        //[Column("Name", TypeName = "NVARCHAR")]
        //[StringLength(128)]
        //[Required(AllowEmptyStrings = false, ErrorMessage = "{2} cannot be null or empty")]
        //public String Name { get; set; }

        //[Column("Email", TypeName = "NVARCHAR")]
        //[StringLength(256)]
        //[Required(AllowEmptyStrings = false, ErrorMessage = "{2} cannot be null or empty")]
        //public String Email { get; set; }

        #endregion Property

        #region Customer

        [Column("CustomerId", TypeName = "UNIQUEIDENTIFIER")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{2} cannot be null or empty")]
        public Guid CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }

        #endregion Customer
    }
}
