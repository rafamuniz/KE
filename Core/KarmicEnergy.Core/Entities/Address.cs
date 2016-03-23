using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KarmicEnergy.Core.Entities
{
    [Table("Addresses", Schema = "dbo")]
    public class Address : BaseEntity
    {
        #region Property

        [Key, Column("Id", Order = 1, TypeName = "UNIQUEIDENTIFIER")]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Column("AddressLine1", TypeName = "NVARCHAR")]
        [StringLength(128)]
        public String AddressLine1 { get; set; }

        [Column("AddressLine2", TypeName = "NVARCHAR")]
        [StringLength(128)]
        public String AddressLine2 { get; set; }

        [Column("City", TypeName = "NVARCHAR")]
        [StringLength(128)]
        public String City { get; set; }

        [Column("State", TypeName = "NVARCHAR")]
        [StringLength(128)]
        public String State { get; set; }

        [Column("Country", TypeName = "NVARCHAR")]
        [StringLength(128)]
        public String Country { get; set; }

        [Column("ZipCode", TypeName = "NVARCHAR")]
        [StringLength(16)]
        public String ZipCode { get; set; }
        
        #endregion Property
    }
}
