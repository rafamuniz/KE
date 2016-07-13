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
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} cannot be null or empty")]
        public String Name { get; set; }

        [Column("Reference", TypeName = "NVARCHAR")]
        [StringLength(8)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} cannot be null or empty")]
        public String Reference { get; set; }

        [Column("IPAddress", TypeName = "NVARCHAR")]
        [StringLength(64)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} cannot be null or empty")]
        public String IPAddress { get; set; }

        [Column("Status", TypeName = "CHAR")]
        [StringLength(1)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} cannot be null or empty")]
        public String Status { get; set; } = "A";

        [Column("Latitude", TypeName = "NVARCHAR")]
        [StringLength(64)]
        public String Latitude { get; set; }

        [Column("Longitude", TypeName = "NVARCHAR")]
        [StringLength(64)]
        public String Longitude { get; set; }

        #endregion Property

        #region Customer

        [Column("CustomerId", TypeName = "UNIQUEIDENTIFIER")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} cannot be null or empty")]
        public Guid CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }

        #endregion Customer

        #region Address

        [Column("AddressId", TypeName = "UNIQUEIDENTIFIER")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} cannot be null or empty")]
        public Guid AddressId { get; set; }

        [ForeignKey("AddressId")]
        public virtual Address Address { get; set; }

        #endregion Address

        #region Functions 
        public void Update(Site entity)
        {
            this.Name = entity.Name;
            this.IPAddress = entity.IPAddress;
            this.Reference = entity.Reference;
            this.Status = entity.Status;
            this.Longitude = entity.Longitude;
            this.Latitude = entity.Latitude;

            this.CustomerId = entity.CustomerId;
            this.AddressId = entity.AddressId;

            this.CreatedDate = entity.CreatedDate;
            this.LastModifiedDate = entity.LastModifiedDate;
            this.DeletedDate = entity.DeletedDate;
        }
        #endregion Functions 
    }
}
