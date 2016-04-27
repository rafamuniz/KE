using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KarmicEnergy.Core.Entities
{
    [Table("Cities", Schema = "dbo")]
    public class City : BaseEntity
    {
        #region Property

        [Key, Column("Id", Order = 1, TypeName = "UNIQUEIDENTIFIER")]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Column("Name", TypeName = "NVARCHAR")]
        [StringLength(128)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{2} cannot be null or empty")]
        public String Name { get; set; }

        #endregion Property

        #region Country

        [Column("CountryId", TypeName = "SMALLINT")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} cannot be null or empty")]
        public Int16 CountryId { get; set; }

        [ForeignKey("CountryId")]
        public virtual Country Country { get; set; }

        #endregion Country
    }
}
