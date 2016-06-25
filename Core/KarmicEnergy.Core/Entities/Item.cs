using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KarmicEnergy.Core.Entities
{
    [Table("Items", Schema = "dbo")]
    public class Item : BaseEntity
    {
        #region Property

        [Key, Column("Id", Order = 1, TypeName = "INT")]
        public Int32 Id { get; set; }

        [Column("Code", TypeName = "NVARCHAR")]
        [StringLength(5)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} cannot be null or empty")]
        public String Code { get; set; }

        [Column("Name", TypeName = "NVARCHAR")]
        [StringLength(128)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} cannot be null or empty")]
        public String Name { get; set; }

        [Column("Status", TypeName = "CHAR")]
        [StringLength(1)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} cannot be null or empty")]
        public String Status { get; set; } = "A";

        #endregion Property

        #region Unit Type

        [Column("UnitTypeId", TypeName = "SMALLINT")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} cannot be null or empty")]
        [DefaultValue(1)]
        public Int16 UnitTypeId { get; set; } = 1;

        [ForeignKey("UnitTypeId")]
        public virtual UnitType UnitType { get; set; }

        #endregion Unit Type

        #region Sensor Type

        [Column("SensorTypeId", TypeName = "SMALLINT")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} cannot be null or empty")]
        [DefaultValue(1)]
        public Int16 SensorTypeId { get; set; } = 1;

        [ForeignKey("SensorTypeId")]
        public virtual SensorType SensorType { get; set; }

        #endregion Sensor Type

        #region Load

        public static List<Item> Load()
        {
            List<Item> entities = new List<Item>()
            {
                // KE Depth
                new Item() { Id = (Int32)ItemEnum.Range, Code = "R", Name = "Range", SensorTypeId = (Int16)SensorTypeEnum.KEDepth, UnitTypeId = (Int16)UnitTypeEnum.Length },
                new Item() { Id = (Int32)ItemEnum.WaterVolume, Code = "WV", Name = "Water Volume", SensorTypeId = (Int16)SensorTypeEnum.KEDepth, UnitTypeId = (Int16)UnitTypeEnum.Volume },
                new Item() { Id = (Int32)ItemEnum.WaterTemperature, Code = "WT", Name = "Water Temperature", SensorTypeId = (Int16)SensorTypeEnum.KEDepth, UnitTypeId = (Int16)UnitTypeEnum.Temperature },
                new Item() { Id = (Int32)ItemEnum.AmbientTemperature, Code= "AT", Name = "Ambient Temperature", SensorTypeId = (Int16)SensorTypeEnum.KEDepth, UnitTypeId = (Int16)UnitTypeEnum.Temperature },
                new Item() { Id = (Int32)ItemEnum.Voltage, Code= "V", Name = "Voltage", SensorTypeId = (Int16)SensorTypeEnum.KEDepth, UnitTypeId = (Int16)UnitTypeEnum.Energy },

                // FlowMeter
                new Item() { Id = (Int32)ItemEnum.RateFlow, Code= "RF", Name = "Rate of Flow", SensorTypeId = (Int16)SensorTypeEnum.FlowMeter, UnitTypeId = (Int16)UnitTypeEnum.Volume},
                new Item() { Id = (Int32)ItemEnum.Totalizer, Code= "T", Name = "Totalizer", SensorTypeId = (Int16)SensorTypeEnum.FlowMeter, UnitTypeId = (Int16)UnitTypeEnum.Volume },
                new Item() { Id = (Int32)ItemEnum.VoltageFlowMeter, Code= "V", Name = "Voltage", SensorTypeId = (Int16)SensorTypeEnum.FlowMeter, UnitTypeId = (Int16)UnitTypeEnum.Energy },

                // PH
                new Item() { Id = (Int32)ItemEnum.PH, Code= "PH", Name = "PH", SensorTypeId = (Int16)SensorTypeEnum.PHMeter, UnitTypeId = (Int16)UnitTypeEnum.PH },
                new Item() { Id = (Int32)ItemEnum.VoltagePHMeter, Code= "V", Name = "Voltage", SensorTypeId = (Int16)SensorTypeEnum.PHMeter, UnitTypeId = (Int16)UnitTypeEnum.Energy },

                // Gas Sensor
                new Item() { Id = (Int32)ItemEnum.GasLevel, Code= "GL", Name = "Gas Level", SensorTypeId = (Int16)SensorTypeEnum.GasSensor, UnitTypeId = (Int16)UnitTypeEnum.UnitConcentration },
                new Item() { Id = (Int32)ItemEnum.VoltageGasSensor, Code= "V", Name = "Voltage", SensorTypeId = (Int16)SensorTypeEnum.GasSensor, UnitTypeId = (Int16)UnitTypeEnum.Energy },
                
                // Salinity Sensor
                new Item() { Id = (Int32)ItemEnum.Salinity, Code= "S", Name = "Salinity ", SensorTypeId = (Int16)SensorTypeEnum.SalinitySensor, UnitTypeId = (Int16)UnitTypeEnum.UnitConcentration },
                new Item() { Id = (Int32)ItemEnum.VoltageSalinity, Code= "V", Name = "Voltage", SensorTypeId = (Int16)SensorTypeEnum.SalinitySensor, UnitTypeId = (Int16)UnitTypeEnum.Energy }
            };

            return entities;
        }
        #endregion Load
    }

    public enum ItemEnum : int
    {
        [Description("Range")]
        Range = 1,

        [Description("Water Volume")]
        WaterVolume = 2,

        [Description("Water Temperature")]
        WaterTemperature = 3,

        [Description("Ambient Temperature")]
        AmbientTemperature = 4,

        [Description("Voltage")]
        Voltage = 5,

        [Description("PH")]
        PH = 6,

        [Description("Rate of Flow")]
        RateFlow = 7,

        [Description("Totalizer")]
        Totalizer = 8,

        [Description("Voltage")]
        VoltageFlowMeter = 9,

        [Description("Voltage")]
        VoltagePHMeter = 10,

        [Description("Gas Level")]
        GasLevel = 11,

        [Description("Voltage")]
        VoltageGasSensor = 12,

        [Description("Salinity")]
        Salinity = 13,

        [Description("Voltage")]
        VoltageSalinity = 14,
    }
}
