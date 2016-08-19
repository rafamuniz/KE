using KarmicEnergy.Core.Entities.Interface;
using Munizoft.Util.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KarmicEnergy.Core.Entities
{
    [Table("Ponds", Schema = "dbo")]
    public class Pond : BaseEntity, IConvertUnit
    {
        #region Constructor
        public Pond()
        {
            this.Id = Guid.NewGuid();
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

        [Column("Description", TypeName = "NVARCHAR")]
        [MaxLength]
        public String Description { get; set; }

        [Column("Reference", TypeName = "NVARCHAR")]
        [StringLength(8)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} cannot be null or empty")]
        public String Reference { get; set; }

        [Column("WaterVolumeCapacity", TypeName = "DECIMAL")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} cannot be null or empty")]
        public Decimal WaterVolumeCapacity { get; set; }

        #region Unit

        [Column("WaterVolumeCapacityUnitId", TypeName = "SMALLINT")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} cannot be null or empty")]
        public Int16 WaterVolumeCapacityUnitId { get; set; } = (Int16)UnitEnum.Barrel;

        [ForeignKey("WaterVolumeCapacityUnitId")]
        public virtual Unit WaterVolumeCapacityUnit { get; set; }

        #endregion Unit

        [Column("Status", TypeName = "CHAR")]
        [StringLength(1)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} cannot be null or empty")]
        public String Status { get; set; } = "A";

        [Column("Latitude", TypeName = "NVARCHAR")]
        [StringLength(64)]
        public String Latitude { get; set; }

        [Column("Longitude", TypeName = "NVARCHAR")]
        [StringLength(64)]
        public String Longitude { get; set; }

        #endregion Property

        #region Site

        [Column("SiteId", TypeName = "UNIQUEIDENTIFIER")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} cannot be null or empty")]
        public Guid SiteId { get; set; }

        [ForeignKey("SiteId")]
        public virtual Site Site { get; set; }

        #endregion Site

        #region Sensors

        public virtual List<Sensor> Sensors { get; set; }

        #endregion Sensors

        #region Functions 
        public void Update(Pond entity)
        {
            this.Name = entity.Name;
            this.Description = entity.Description;
            this.Reference = entity.Reference;
            this.Status = entity.Status;
            this.Longitude = entity.Longitude;
            this.Latitude = entity.Latitude;

            this.WaterVolumeCapacity = entity.WaterVolumeCapacity;
            //this.WaterVolumeCapacityUnitId = entity.WaterVolumeCapacityUnitId;

            this.SiteId = entity.SiteId;

            this.CreatedDate = entity.CreatedDate;
            this.LastModifiedDate = entity.LastModifiedDate;
            this.DeletedDate = entity.DeletedDate;
        }

        public String Convert()
        {
            throw new Exception("Error convert");
        }

        public String Convert(Int16 unitId)
        {
            Double value;
            if (Double.TryParse(this.WaterVolumeCapacity.ToString(), out value))
            {
                // From Fahrenheit To
                switch (unitId)
                {
                    case (Int16)UnitEnum.Barrel:
                        return ((Int32)VolumeUnit.GallonToBarrel(value)).ToString();
                    //case (Int16)UnitEnum.Liter:
                    //    return ((Int32)VolumeUnit.LiterToGallon(value)).ToString();
                    default:
                        return WaterVolumeCapacity.ToString();
                }
            }
            else
                throw new Exception("Error convert");
        }
        #endregion Functions 
    }
}
