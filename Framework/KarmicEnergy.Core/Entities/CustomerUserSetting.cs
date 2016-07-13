using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KarmicEnergy.Core.Entities
{
    [Table("CustomerUserSettings", Schema = "dbo")]
    public class CustomerUserSetting : BaseEntity
    {
        #region Property

        [Key, Column("Id", Order = 1, TypeName = "UNIQUEIDENTIFIER")]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Column("Key", TypeName = "NVARCHAR")]
        [StringLength(128)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{2} cannot be null or empty")]
        public String Key { get; set; }

        [Column("Value", TypeName = "NVARCHAR")]        
        [Required(AllowEmptyStrings = false, ErrorMessage = "{2} cannot be null or empty")]
        public String Value { get; set; }

        #endregion Property

        #region Customer

        [Column("CustomerUserId", TypeName = "UNIQUEIDENTIFIER")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} cannot be null or empty")]
        public Guid CustomerUserId { get; set; }

        [ForeignKey("CustomerUserId")]
        public virtual CustomerUser CustomerUser { get; set; }

        #endregion Customer

        #region functions
        public void Update(CustomerUserSetting entity)
        {
            this.CustomerUserId = entity.CustomerUserId;
            
            this.Key = entity.Key;           
            this.Value = entity.Value;
           
            this.CreatedDate = entity.CreatedDate;
            this.LastModifiedDate = entity.LastModifiedDate;
            this.DeletedDate = entity.DeletedDate;
        }

        #endregion functions
    }
}
