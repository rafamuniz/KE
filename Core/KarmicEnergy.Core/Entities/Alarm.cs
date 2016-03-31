using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KarmicEnergy.Core.Entities
{
    [Table("Alarms", Schema = "dbo")]
    public class Alarm : BaseEntity
    {
        #region Property

        [Key, Column("Id", Order = 1, TypeName = "UNIQUEIDENTIFIER")]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        #endregion Property

        #region SensorAlarm

        [Column("SensorItemAlarmId", TypeName = "UNIQUEIDENTIFIER")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} cannot be null or empty")]
        public Guid SensorItemAlarmId { get; set; }

        [ForeignKey("SensorItemAlarmId")]
        public virtual SensorItemAlarm SensorItemAlarm { get; set; }

        #endregion SensorAlarm 
    }
}
