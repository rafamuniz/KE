using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KarmicEnergy.Core.Entities
{
    [Table("Notifications", Schema = "dbo")]
    public class Notification : BaseEntity
    {
        #region Property

        [Key, Column("Id", Order = 1, TypeName = "UNIQUEIDENTIFIER")]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Column("From", TypeName = "NVARCHAR")]
        [StringLength(256)]
        public String From { get; set; }

        [Column("To", TypeName = "NVARCHAR")]
        [StringLength(4000)]
        public String To { get; set; }

        [Column("Subject", TypeName = "NVARCHAR")]
        [StringLength(128)]
        public String Subject { get; set; }

        [Column("Message", TypeName = "NVARCHAR")]
        [StringLength(4000)]
        public String Message { get; set; }

        [Column("ErrorMessage", TypeName = "NVARCHAR")]
        [StringLength(4000)]
        public String ErrorMessage { get; set; }

        [Column("IsSentSuccess", TypeName = "BIT")]
        public Boolean? IsSentSuccess { get; set; }

        [Column("IsSent", TypeName = "BIT")]
        public Boolean IsSent { get; set; } = false;

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
