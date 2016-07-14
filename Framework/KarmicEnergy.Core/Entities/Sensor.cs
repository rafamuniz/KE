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
            this.Id = Guid.NewGuid();
            SensorItems = new List<SensorItem>();
        }

        #endregion Constructor

        #region Property

        [Key, Column("Id", Order = 1, TypeName = "UNIQUEIDENTIFIER")]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.None)]
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

        #region Site

        [Column("SiteId", TypeName = "UNIQUEIDENTIFIER")]
        public Guid? SiteId { get; set; }

        [ForeignKey("SiteId")]
        public virtual Site Site { get; set; }

        #endregion Site

        #region Pond

        [Column("PondId", TypeName = "UNIQUEIDENTIFIER")]
        public Guid? PondId { get; set; }

        [ForeignKey("PondId")]
        public virtual Pond Pond { get; set; }

        #endregion Pond

        #region Tank

        [Column("TankId", TypeName = "UNIQUEIDENTIFIER")]
        public Guid? TankId { get; set; }

        [ForeignKey("TankId")]
        public virtual Tank Tank { get; set; }

        #endregion Tank

        #region SensorItems        

        public virtual IList<SensorItem> SensorItems { get; set; }

        #endregion SensorItems

        #region Functions 
        public Boolean HasSensorItem()
        {
            if (SensorItems.Count > 0)
                return true;
            return false;
        }

        public Boolean IsSite()
        {
            if (SiteId.HasValue && SiteId != default(Guid) &&
                !TankId.HasValue)
                return true;
            return false;
        }

        public Boolean IsPond()
        {
            if (PondId.HasValue && PondId != default(Guid))
                return true;
            return false;
        }

        public Boolean IsTank()
        {
            if (TankId.HasValue && TankId != default(Guid))
                return true;
            return false;
        }

        public void Update(Sensor entity)
        {
            this.Name = entity.Name;
            this.SpotGPS = entity.SpotGPS;
            this.Reference = entity.Reference;
            this.Status = entity.Status;

            this.SensorTypeId = entity.SensorTypeId;
            this.SiteId = entity.SiteId;
            this.PondId = entity.PondId;
            this.TankId = entity.TankId;

            this.CreatedDate = entity.CreatedDate;
            this.LastModifiedDate = entity.LastModifiedDate;
            this.DeletedDate = entity.DeletedDate;
        }
        #endregion Functions 

    }
}
