using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KarmicEnergy.Core.Entities
{
    [Table("CustomerUserSites", Schema = "dbo")]
    public class CustomerUserSite : BaseEntity
    {
        #region Property

        [Key, Column("Id", Order = 1, TypeName = "UNIQUEIDENTIFIER")]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        #endregion Property

        #region User

        [Column("CustomerUserId", TypeName = "UNIQUEIDENTIFIER")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} cannot be null or empty")]
        public Guid CustomerUserId { get; set; }

        #endregion User

        #region Site

        [Column("SiteId", TypeName = "UNIQUEIDENTIFIER")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} cannot be null or empty")]
        public Guid SiteId { get; set; }

        [ForeignKey("SiteId")]
        public virtual Site Site { get; set; }

        #endregion Site

        #region Functions
        public void Update(CustomerUserSite entity)
        {
            this.CustomerUserId = entity.CustomerUserId;
            this.SiteId = entity.SiteId;
            
            this.CreatedDate = entity.CreatedDate;
            this.LastModifiedDate = entity.LastModifiedDate;
            this.DeletedDate = entity.DeletedDate;
        }
        #endregion Functions
    }
}
