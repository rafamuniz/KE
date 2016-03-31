using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KarmicEnergy.Core.Entities
{
    [Table("Item", Schema = "dbo")]
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

        #region Load

        public static List<Item> Load()
        {
            List<Item> entities = new List<Item>()
            {
                new Item() { Id = (int)ItemEnum.WaterVolume, Code = "WV", Name = "Water Volume" },
                new Item() { Id = (int)ItemEnum.WaterTemperature, Code = "WT", Name = "Water Temperature" },
                new Item() { Id = (int)ItemEnum.WeatherTemperature, Code= "WV", Name = "Weather Temperature" },
                new Item() { Id = (int)ItemEnum.Volts, Code= "V", Name = "Volts" }
            };

            return entities;
        }
        #endregion Load
    }

    public enum ItemEnum : int
    {
        [Description("Water Volume")]
        WaterVolume = 1,

        [Description("Water Temperature")]
        WaterTemperature = 2,

        [Description("Weather Temperature")]
        WeatherTemperature = 3,

        [Description("Volts")]
        Volts = 4
    }
}
