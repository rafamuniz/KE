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
                new Severity() { Id = 1, Name = "Low" },
                new Severity() { Id = 2, Name = "Medium" },
                new Severity() { Id = 3, Name = "High" }
            };

            return entities;
        }
        #endregion Load
    }
}
