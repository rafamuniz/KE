using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KarmicEnergy.Core.Entities
{
    [Table("Sensors", Schema = "dbo")]
    public class Sensor : BaseEntity
    {
        #region Property

        [Key, Column("Id", Order = 1, TypeName = "BIGINT")]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public Int64 Id { get; set; }

        [Column("Name", TypeName = "NVARCHAR")]
        [StringLength(128)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} cannot be null or empty")]
        public String Name { get; set; }

        [Column("Status", TypeName = "CHAR")]
        [StringLength(1)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} cannot be null or empty")]
        public String Status { get; set; } = "A";

        #endregion Property

        #region SensorType

        [Column("SensorTypeId", TypeName = "SMALLINT")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} cannot be null or empty")]
        public Int16 SensorTypeId { get; set; }

        [ForeignKey("SensorTypeId")]
        public virtual SensorType SensorType { get; set; }

        #endregion SensorType

        #region Tank

        [Column("TankId", TypeName = "UNIQUEIDENTIFIER")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} cannot be null or empty")]
        public Guid TankId { get; set; }

        [ForeignKey("TankId")]
        public virtual Tank Tank { get; set; }

        #endregion Tank
        
        //#region SensorItems        

        //public virtual IList<SensorItem> SensorItems { get; set; }

        //#endregion SensorItems
    }
}
