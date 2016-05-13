using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KarmicEnergy.Core.Entities
{
    [Table("Sensors", Schema = "dbo")]
    public class Sensor : BaseEntity
    {
        #region Constructor
        public Sensor()
        {
            SensorItems = new List<SensorItem>();
        }

        #endregion Constructor

        #region Property

        [Key, Column("Id", Order = 1, TypeName = "UNIQUEIDENTIFIER")]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Column("Name", TypeName = "NVARCHAR")]
        [StringLength(128)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} cannot be null or empty")]
        public String Name { get; set; }

        [Column("Reference", TypeName = "NVARCHAR")]
        [StringLength(8)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} cannot be null or empty")]
        public String Reference { get; set; }

        [Column("Status", TypeName = "CHAR")]
        [StringLength(1)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} cannot be null or empty")]
        public String Status { get; set; } = "A";

        [Column("SpotGPS", TypeName = "NVARCHAR")]
        [StringLength(128)]
        public String SpotGPS { get; set; }

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
        public Guid? TankId { get; set; }

        [ForeignKey("TankId")]
        public virtual Tank Tank { get; set; }

        #endregion Tank

        #region Site

        [Column("SiteId", TypeName = "UNIQUEIDENTIFIER")]
        public Guid? SiteId { get; set; }

        [ForeignKey("SiteId")]
        public virtual Site Site { get; set; }

        #endregion Site

        #region SensorItems        

        public virtual IList<SensorItem> SensorItems { get; set; }

        #endregion SensorItems

        public Boolean HasSensorItem()
        {
            if (SensorItems.Count > 0)
                return true;
            return false;
        }
    }
}
