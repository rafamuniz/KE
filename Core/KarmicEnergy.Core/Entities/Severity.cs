using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KarmicEnergy.Core.Entities
{
    [Table("Severities", Schema = "dbo")]
    public class Severity : BaseEntity
    {
        #region Property

        [Key, Column("Id", Order = 1, TypeName = "SMALLINT")]
        public Int16 Id { get; set; }

        [Column("Name", TypeName = "NVARCHAR")]
        [StringLength(128)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} cannot be null or empty")]
        public String Name { get; set; }

        #endregion Property        

        #region Load

        public static List<Severity> Load()
        {
            List<Severity> entities = new List<Severity>()
            {
                new Severity() { Id = (Int16)SeverityEnum.Info, Name = "Info" },
                new Severity() { Id = (Int16)SeverityEnum.Low, Name = "Low" },
                new Severity() { Id = (Int16)SeverityEnum.Medium, Name = "Medium" },
                new Severity() { Id = (Int16)SeverityEnum.High, Name = "High" },
                new Severity() { Id = (Int16)SeverityEnum.Critical, Name = "Critical" },
            };

            return entities;
        }
        #endregion Load
    }

    public enum SeverityEnum : short
    {
        Info = 1,
        Low = 2,
        Medium = 3,
        High = 4,
        Critical = 5,
    }
}
