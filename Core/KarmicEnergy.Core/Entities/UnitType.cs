using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KarmicEnergy.Core.Entities
{
    [Table("UnitTypes", Schema = "dbo")]
    public class UnitType : BaseEntity
    {
        #region Property

        [Key, Column("Id", Order = 1, TypeName = "SMALLINT")]
        public Int16 Id { get; set; }

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

        public static List<UnitType> Load()
        {
            List<UnitType> entities = new List<UnitType>()
            {
                new UnitType() { Id = (int)UnitTypeEnum.Volume, Name = "Liquid" },
                new UnitType() { Id = (int)UnitTypeEnum.Temperature, Name = "Temperature" },
                new UnitType() { Id = (int)UnitTypeEnum.Length, Name = "Length" },
                new UnitType() { Id = (int)UnitTypeEnum.Energy, Name = "Energy" },
                new UnitType() { Id = (int)UnitTypeEnum.Mass, Name = "Mass" },
                new UnitType() { Id = (int)UnitTypeEnum.LuminousIntensity, Name = "Luminous Intensity" }
            };

            return entities;
        }
        #endregion Load
    }

    public enum UnitTypeEnum : int
    {
        [Description("Temperature")]
        Temperature = 1,

        [Description("Volume")]
        Volume = 2,

        [Description("Length")]
        Length = 3,

        [Description("Energy")]
        Energy = 4,

        [Description("Mass")]
        Mass = 5,

        [Description("Luminous Intensity")]
        LuminousIntensity = 6
    }
}
