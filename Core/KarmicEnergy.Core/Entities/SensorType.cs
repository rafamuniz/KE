using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KarmicEnergy.Core.Entities
{
    [Table("SensorTypes", Schema = "dbo")]
    public class SensorType : BaseEntity
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

        public static List<SensorType> Load()
        {
            List<SensorType> entities = new List<SensorType>()
            {
                new SensorType() { Id = 1, Name = "KE Depth Sensor" },
                new SensorType() { Id = 2, Name = "Flow Meter" }
            };

            return entities;
        }
        #endregion Load
    }
}
