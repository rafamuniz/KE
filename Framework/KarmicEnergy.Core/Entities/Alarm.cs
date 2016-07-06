using Munizoft.Util.Converters;
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

        [Column("Value", TypeName = "NVARCHAR")]
        [StringLength(256)]
        public String Value { get; set; } = String.Empty;

        [Column("LastAckUserId", TypeName = "UNIQUEIDENTIFIER")]
        public Guid? LastAckUserId { get; set; }

        [Column("LastAckUserName", TypeName = "NVARCHAR")]
        [StringLength(256)]
        public String LastAckUserName { get; set; }

        [Column("LastAckDate", TypeName = "DATETIME")]
        public DateTime? LastAckDate { get; set; }

        [Column("StartDate", TypeName = "DATETIME")]
        [Required]
        public DateTime StartDate { get; set; } = DateTime.UtcNow;

        [Column("EndDate", TypeName = "DATETIME")]
        public DateTime? EndDate { get; set; }

        #endregion Property

        #region Sensor Event

        [Column("SensorItemEventId", TypeName = "UNIQUEIDENTIFIER")]
        public Guid SensorItemEventId { get; set; }

        #endregion Sensor Event

        #region Trigger

        [Column("TriggerId", TypeName = "UNIQUEIDENTIFIER")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} cannot be null or empty")]
        public Guid TriggerId { get; set; }

        [ForeignKey("TriggerId")]
        public virtual Trigger Trigger { get; set; }

        #endregion Trigger 

        #region functions

        public String ConverterItemUnit()
        {
            // Temperature - Default Temperatrure - Farenheit
            if (this.Trigger.SensorItem.Unit.UnitTypeId == (Int16)UnitTypeEnum.Temperature)
            {
                Double tempVaue;
                if (Double.TryParse(this.Value, out tempVaue))
                {
                    // From Fahrenheit To
                    switch (this.Trigger.SensorItem.Unit.Name.ToUpper())
                    {
                        case "KELVIN":
                            return ((Int32)TemperatureUnit.FahrenheitToKelvin(tempVaue)).ToString();
                        case "CELSIUS":
                            return ((Int32)TemperatureUnit.FahrenheitToCelsius(tempVaue)).ToString();
                        default:
                            return ((Int32)Double.Parse(this.Value)).ToString();
                    }
                }
                else
                    throw new Exception("Error convert temperature");
            }
            // Volume
            else if (this.Trigger.SensorItem.Unit.UnitTypeId == (Int16)UnitTypeEnum.Volume)
            {

            }

            return this.Value;
        }

        #endregion functions
    }
}
