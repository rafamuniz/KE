using System;
using System.Collections.Generic;
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
        [StringLength(4)]
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

        #region Load

        public static List<Unit> Load()
        {
            List<Unit> entities = new List<Unit>()
            {
                new Unit() { Id = 1, Name = "Gallon", NamePlural = "Gallons", Symbol = "gal", UnitTypeId = (int)UnitTypeEnum.Volume },
                new Unit() { Id = 2, Name = "Barrel", NamePlural = "Barrels", Symbol = "B", UnitTypeId = (int)UnitTypeEnum.Volume },
                new Unit() { Id = 3, Name = "Litre", NamePlural = "Litre", Symbol = "l", UnitTypeId = (int)UnitTypeEnum.Volume },
                new Unit() { Id = 4, Name = "Celsius", NamePlural = "Celsius", Symbol = "C", UnitTypeId = (int)UnitTypeEnum.Temperature },
                new Unit() { Id = 5, Name = "Kevin", NamePlural = "Kevin", Symbol = "K", UnitTypeId = (int)UnitTypeEnum.Temperature },
                new Unit() { Id = 6, Name = "Fahrenheit", NamePlural = "Fahrenheit", Symbol = "F", UnitTypeId = (int)UnitTypeEnum.Temperature }
            };

            return entities;
        }
        #endregion Load
    }
}
