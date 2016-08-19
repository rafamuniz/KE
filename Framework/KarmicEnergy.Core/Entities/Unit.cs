using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KarmicEnergy.Core.Entities
{
    [Table("Units", Schema = "dbo")]
    public class Unit : BaseEntity
    {
        #region Property

        [Key, Column("Id", Order = 1, TypeName = "SMALLINT")]
        public Int16 Id { get; set; }

        [Column("Name", TypeName = "NVARCHAR")]
        [StringLength(128)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} cannot be null or empty")]
        public String Name { get; set; }

        [Column("NamePlural", TypeName = "NVARCHAR")]
        [StringLength(128)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} cannot be null or empty")]
        public String NamePlural { get; set; }

        [Column("Symbol", TypeName = "NVARCHAR")]
        [StringLength(8)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} cannot be null or empty")]
        public String Symbol { get; set; }

        [Column("Status", TypeName = "CHAR")]
        [StringLength(1)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} cannot be null or empty")]
        public String Status { get; set; } = "A";

        #endregion Property        

        #region UnitType

        [Column("UnitTypeId", TypeName = "SMALLINT")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} cannot be null or empty")]
        public Int16 UnitTypeId { get; set; }

        [ForeignKey("UnitTypeId")]
        public virtual UnitType UnitType { get; set; }

        #endregion UnitType

        #region Functions

        public static List<Unit> Load()
        {
            List<Unit> entities = new List<Unit>()
            {
                new Unit() { Id = 1, Name = "Gallon", NamePlural = "Gallons", Symbol = "gal", UnitTypeId = (int)UnitTypeEnum.Volume },
                new Unit() { Id = 2, Name = "Barrel", NamePlural = "Barrels", Symbol = "BBL", UnitTypeId = (int)UnitTypeEnum.Volume, DeletedDate = DateTime.UtcNow },
                new Unit() { Id = 3, Name = "Liter", NamePlural = "Liters", Symbol = "l", UnitTypeId = (int)UnitTypeEnum.Volume, DeletedDate = DateTime.UtcNow },
                new Unit() { Id = 4, Name = "Celsius", NamePlural = "Celsius", Symbol = "°C", UnitTypeId = (int)UnitTypeEnum.Temperature, DeletedDate = DateTime.UtcNow },
                new Unit() { Id = 5, Name = "Kelvin", NamePlural = "Kelvin", Symbol = "K", UnitTypeId = (int)UnitTypeEnum.Temperature, DeletedDate = DateTime.UtcNow },
                new Unit() { Id = 6, Name = "Fahrenheit", NamePlural = "Fahrenheit", Symbol = "°F", UnitTypeId = (int)UnitTypeEnum.Temperature },
                new Unit() { Id = 7, Name = "Inch ", NamePlural = "Inches", Symbol = "in", UnitTypeId = (int)UnitTypeEnum.Length },
                new Unit() { Id = 8, Name = "Centimeter", NamePlural = "Centimeters", Symbol = "cm", UnitTypeId = (int)UnitTypeEnum.Length, DeletedDate = DateTime.UtcNow },
                new Unit() { Id = 9, Name = "Meter", NamePlural = "Meters", Symbol = "m", UnitTypeId = (int)UnitTypeEnum.Length, DeletedDate = DateTime.UtcNow },
                new Unit() { Id = 10, Name = "Millimeter", NamePlural = "Millimeters", Symbol = "mm", UnitTypeId = (int)UnitTypeEnum.Length, DeletedDate = DateTime.UtcNow },
                new Unit() { Id = 11, Name = "Yard", NamePlural = "Yards", Symbol = "yd", UnitTypeId = (int)UnitTypeEnum.Length, DeletedDate = DateTime.UtcNow },
                new Unit() { Id = 12, Name = "PPM", NamePlural = "PPM", Symbol = "ppm", UnitTypeId = (int)UnitTypeEnum.UnitConcentration },
                new Unit() { Id = 13, Name = "mg/m3", NamePlural = "mg/m3", Symbol = "mg/m3", UnitTypeId = (int)UnitTypeEnum.UnitConcentration, DeletedDate = DateTime.UtcNow },
                new Unit() { Id = 14, Name = "PH", NamePlural = "PH", Symbol = "PH", UnitTypeId = (int)UnitTypeEnum.PH },
                new Unit() { Id = 15, Name = "Volt", NamePlural = "Volts", Symbol = "Vm", UnitTypeId = (int)UnitTypeEnum.Energy }
            };

            return entities;
        }
        public void Update(Unit entity)
        {
            this.Name = entity.Name;
            this.NamePlural = entity.NamePlural;
            this.Symbol = entity.Symbol;
            this.Status = entity.Status;
            this.NamePlural = entity.NamePlural;
            this.UnitTypeId = entity.UnitTypeId;

            this.CreatedDate = entity.CreatedDate;
            this.LastModifiedDate = entity.LastModifiedDate;
            this.DeletedDate = entity.DeletedDate;
        }
        #endregion Functions
    }

    public enum UnitEnum : short
    {
        [Description("Gallon")]
        Gallon = 1,

        [Description("Barrel")]
        Barrel = 2,

        [Description("Liter")]
        Liter = 3,

        [Description("Celsius")]
        Celsius = 4,

        [Description("Kelvin")]
        Kelvin = 5,

        [Description("Fahrenheit")]
        Fahrenheit = 6,

        [Description("Inch")]
        Inch = 7,

        [Description("Centimeter")]
        Centimeter = 8,

        [Description("Meter")]
        Meter = 9,

        [Description("Millimeter")]
        Millimeter = 10,

        [Description("Yard")]
        Yard = 11,

        [Description("PPM")]
        PPM = 12,

        [Description("mg/m3")]
        MgM3 = 13,

        [Description("Volt")]
        Volt = 14,

        [Description("PH")]
        PH = 15
    }
}
