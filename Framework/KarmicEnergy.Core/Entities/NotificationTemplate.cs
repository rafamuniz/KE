using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;

namespace KarmicEnergy.Core.Entities
{
    [Table("NotificationTemplates", Schema = "dbo")]
    public class NotificationTemplate : BaseEntity
    {
        #region Constructor
        public NotificationTemplate()
        {
            this.Id = Guid.NewGuid();
        }
        #endregion Constructor

        #region Property

        [Key, Column("Id", Order = 1, TypeName = "UNIQUEIDENTIFIER")]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        [Column("Name", TypeName = "NVARCHAR")]
        [StringLength(64)]
        public String Name { get; set; }

        [Column("Subject", TypeName = "NVARCHAR")]
        [StringLength(128)]
        public String Subject { get; set; }

        [Column("Message", TypeName = "NTEXT")]        
        public String Message { get; set; }

        #region NotificationType

        [Column("NotificationTypeId", TypeName = "SMALLINT")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} cannot be null or empty")]
        public Int16 NotificationTypeId { get; set; }

        [ForeignKey("NotificationTypeId")]
        public virtual NotificationType NotificationType { get; set; }

        #endregion NotificationType

        #endregion Property

        #region Functions

        public static List<NotificationTemplate> Load()
        {
            List<NotificationTemplate> entities = new List<NotificationTemplate>()
            {
                new NotificationTemplate() { NotificationTypeId = (Int16)NotificationTypeEnum.Email, Name = "ResetPassword", Subject = "Reset Password" },
                new NotificationTemplate() { NotificationTypeId = (Int16)NotificationTypeEnum.Email, Name = "AlarmEmail", Subject = "Alarm" },
                new NotificationTemplate() { NotificationTypeId = (Int16)NotificationTypeEnum.Email, Name = "AlarmNormalizedEmail", Subject = "Alarm Normalized" },
            };

            return entities;
        }
        public void Update(NotificationTemplate entity)
        {
            this.Name = entity.Name;
            this.Subject = entity.Subject;

            this.Message = entity.Message;
            this.NotificationTypeId = entity.NotificationTypeId;
            
            this.CreatedDate = entity.CreatedDate;
            this.LastModifiedDate = entity.LastModifiedDate;
            this.DeletedDate = entity.DeletedDate;
        }
        #endregion Functions
    }
}
