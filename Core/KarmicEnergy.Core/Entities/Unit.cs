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

        [Column("Status", TypeName = "CHAR")]
        [StringLength(1)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} cannot be null or empty")]
        public String Status { get; set; } = "A";

        #endregion Property        

        #region Load

        public static List<Unit> Load()
        {
            List<Unit> entities = new List<Unit>()
            {
                new Unit() { Id = 1, Name = "Gallon", NamePlural = "Gallons" },
                new Unit() { Id = 2, Name = "Barrel", NamePlural = "Barrels" }
            };

            return entities;
        }
        #endregion Load
    }
}
