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

        #region Load

        public static List<NotificationType> Load()
        {
            List<NotificationType> entities = new List<NotificationType>()
            {
                new NotificationType() { Id = (Int16)NotificationTypeEnum.Email, Name = "Email" },
                new NotificationType() { Id = (Int16)NotificationTypeEnum.SMS, Name = "SMS" },
            };

            return entities;
        }
        #endregion Load
    }

    public enum NotificationTypeEnum : short
    {
        Email = 1,
        SMS = 2        
    }
}
