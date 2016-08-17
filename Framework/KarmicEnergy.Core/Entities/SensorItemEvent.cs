using Munizoft.Util.Converters;
using System;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KarmicEnergy.Core.Entities
{
    [Table("SensorItemEvents", Schema = "dbo")]
    public class SensorItemEvent : BaseEntity
    {
        #region Constructor
        public SensorItemEvent()
        {
            this.Id = Guid.NewGuid();
        }
        #endregion Constructor

        #region Property

        [Key, Column("Id", Order = 1, TypeName = "UNIQUEIDENTIFIER")]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.None)]
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
                Double tempValue;
                if (Double.TryParse(this.Value, out tempValue))
                {
                    // From Fahrenheit To
                    switch (this.SensorItem.Unit.Name.ToUpper())
                    {
                        case "KELVIN":
                            return ((Int32)TemperatureUnit.FahrenheitToKelvin(tempValue)).ToString();
                        case "CELSIUS":
                            return ((Int32)TemperatureUnit.FahrenheitToCelsius(tempValue)).ToString();
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
                if (this.SensorItem.Sensor.PondId.HasValue) // POND
                {
                    return PondVolumeCapactiy();
                }
                else if (this.SensorItem.Sensor.TankId.HasValue) // TANK
                {
                    return TankVolumeCapactiy();
                }
                else // SITE
                {
                    return this.Value;
                }
            }

            return this.Value;
        }

        private String PondVolumeCapactiy()
        {
            return this.Value;  
        }

        private String TankVolumeCapactiy()
        {
            var tank = this.SensorItem.Sensor.Tank;
            if (tank.StickConversionId.HasValue) // STICK CONVERSION
            {
                var values = this.SensorItem.Sensor.Tank.StickConversion.StickConversionValues;

                var stv = values.Where(x => x.FromValue == this.Value);

                if (stv.Any())
                    return stv.SingleOrDefault().ToValue;
                return this.Value;
            }
            else
            {
                Int32 range;
                if (Int32.TryParse(this.Value, out range))
                {
                    return tank.CalculateWaterRemaining(range).ToString();
                }
            }

            return this.Value;
        }

        public void Update(SensorItemEvent entity)
        {
            this.EventDate = entity.EventDate;
            this.Value = entity.Value;
            this.SensorItemId = entity.SensorItemId;
            this.SensorItemEventId = entity.SensorItemEventId;
            this.CheckedAlarmDate = entity.CheckedAlarmDate;

            this.CreatedDate = entity.CreatedDate;
            this.LastModifiedDate = entity.LastModifiedDate;
            this.DeletedDate = entity.DeletedDate;
        }
        #endregion functions
    }
}
