using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KarmicEnergy.Core.Entities
{
    [Table("NotificationTypes", Schema = "dbo")]
    public class NotificationType : BaseEntity
    {
        #region Constructor
        public NotificationType()
        {

        }
        #endregion Constructor

        #region Property

        [Key, Column("Id", Order = 1, TypeName = "SMALLINT")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int16 Id { get; set; }

        [Column("Name", TypeName = "NVARCHAR")]
        [StringLength(16)]
        public String Name { get; set; }

        #endregion Property

        #region Functions

        public static List<NotificationType> Load()
        {
            List<NotificationType> entities = new List<NotificationType>()
            {
                new NotificationType() { Id = (Int16)NotificationTypeEnum.Email, Name = "Email" },
                new NotificationType() { Id = (Int16)NotificationTypeEnum.SMS, Name = "SMS" },
            };

            return entities;
        }

        public void Update(NotificationType entity)
        {
            this.Name = entity.Name;           
            this.CreatedDate = entity.CreatedDate;
            this.LastModifiedDate = entity.LastModifiedDate;
            this.DeletedDate = entity.DeletedDate;
        }
        #endregion Functions
    }

    public enum NotificationTypeEnum : short
    {
        Email = 1,
        SMS = 2        
    }
}
