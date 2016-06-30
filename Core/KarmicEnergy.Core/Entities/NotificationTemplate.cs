using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KarmicEnergy.Core.Entities
{
    [Table("NotificationTemplates", Schema = "dbo")]
    public class NotificationTemplate : BaseEntity
    {
        #region Property

        [Key, Column("Id", Order = 1, TypeName = "UNIQUEIDENTIFIER")]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Column("Name", TypeName = "NVARCHAR")]
        [StringLength(64)]        
        public String Name { get; set; }
        
        [Column("Subject", TypeName = "NVARCHAR")]
        [StringLength(128)]
        public String Subject { get; set; }

        [Column("Message", TypeName = "NVARCHAR")]
        [StringLength(4000)]
        public String Message { get; set; }
             
        #endregion Property

        #region Country

        [Column("NotificationTypeId", TypeName = "SMALLINT")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} cannot be null or empty")]
        public Int16 NotificationTypeId { get; set; }

        [ForeignKey("NotificationTypeId")]
        public virtual NotificationType NotificationType { get; set; }

        #endregion Country
    }
}
