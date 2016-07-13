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

        [Column("Message", TypeName = "NTEXT")]
        public String Message { get; set; }

        [Column("ErrorMessage", TypeName = "NTEXT")]
        public String ErrorMessage { get; set; }

        [Column("IsSentSuccess", TypeName = "DATETIME")]
        public DateTime? SentSuccessDate { get; set; }

        #endregion Property

        #region NotificationType

        [Column("NotificationTypeId", TypeName = "SMALLINT")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} cannot be null or empty")]
        public Int16 NotificationTypeId { get; set; }

        [ForeignKey("NotificationTypeId")]
        public virtual NotificationType NotificationType { get; set; }

        #endregion NotificationType

        #region Functions
        public void Update(Notification entity)
        {
            this.ErrorMessage = entity.ErrorMessage;
            this.From = entity.From;
            this.Message = entity.Message;
            this.NotificationTypeId = entity.NotificationTypeId;
            this.SentSuccessDate = entity.SentSuccessDate;
            this.Subject = entity.Subject;
            this.To = entity.To;
            
            this.CreatedDate = entity.CreatedDate;
            this.LastModifiedDate = entity.LastModifiedDate;
            this.DeletedDate = entity.DeletedDate;
        }
        #endregion Functions
    }
}
