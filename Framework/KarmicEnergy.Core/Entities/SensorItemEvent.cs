using Munizoft.Util.Converters;
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

        /// <summary>
        /// AT
        /// GL
        /// PH
        /// R
        /// RF
        /// S
        /// T
        /// V
        /// WT
        /// WV
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>  
        [NotMapped]
        public String ValueFormat
        {
            get
            {
                if (SensorItem != null)
                {
                    switch (SensorItem.ItemId)
                    {
                        case (Int16)ItemEnum.Voltage:
                        case (Int16)ItemEnum.VoltageFlowMeter:
                        case (Int16)ItemEnum.VoltageGasSensor:
                        case (Int16)ItemEnum.VoltagePHMeter:
                        case (Int16)ItemEnum.VoltageSalinity:
                            return Value.Insert(Value.Length - 3, ".");
                        default:
                            return Value;
                    }
                }
                return Value;
            }
            private set { }
        }

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

        [ForeignKey("SensorItemEventId")]
        public virtual SensorItemEvent SensorItemEventChild { get; set; }

        #endregion Event

        #region Alarm

        [Column("CheckedAlarmDate", TypeName = "DATETIME")]
        public DateTime? CheckedAlarmDate { get; set; }

        #endregion Alarm

        #region functions

        public String ConverterItemUnit()
        {
            // Temperature - Default Temperatrure - Farenheit
            if (this.SensorItem.Unit.UnitTypeId == (Int16)UnitTypeEnum.Temperature)
            {
                Double tempVaue;
                if (Double.TryParse(this.Value, out tempVaue))
                {
                    // From Fahrenheit To
                    switch (this.SensorItem.Unit.Name.ToUpper())
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
            else if (this.SensorItem.Unit.UnitTypeId == (Int16)UnitTypeEnum.Volume)
            {

            }

            return this.Value;
        }

        #endregion functions
    }
}
