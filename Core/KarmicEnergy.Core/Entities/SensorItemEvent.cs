using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KarmicEnergy.Core.Entities
{
    [Table("SensorItemEvents", Schema = "dbo")]
    public class SensorItemEvent : BaseEntity
    {
        #region Property

        [Key, Column("Id", Order = 1, TypeName = "UNIQUEIDENTIFIER")]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Column("Value", TypeName = "NVARCHAR")]
        [StringLength(256)]
        public String Value { get; set; } = String.Empty;

        [Column("CalculatedValue", TypeName = "NVARCHAR")]
        [StringLength(256)]
        public String CalculatedValue { get; set; }

        [Column("EventDate", TypeName = "DATETIME")]
        [Required]
        public DateTime EventDate { get; set; } = DateTime.UtcNow;

        #endregion Property

        #region Sensor Item

        [Column("SensorItemId", TypeName = "UNIQUEIDENTIFIER")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} cannot be null or empty")]
        public Guid SensorItemId { get; set; }

        [ForeignKey("SensorItemId")]
        public virtual SensorItem SensorItem { get; set; }

        #endregion Sensor Item

        #region Event

        [Column("SensorItemEventId", TypeName = "UNIQUEIDENTIFIER")]
        public Guid? SensorItemEventId { get; set; }

        #endregion Event

        #region Alarm

        [Column("CheckedAlarm", TypeName = "BIT")]
        public Boolean CheckedAlarm { get; set; } = false;

        #endregion Alarm
    }
}
