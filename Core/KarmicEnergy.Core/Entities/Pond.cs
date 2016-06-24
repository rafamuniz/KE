using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KarmicEnergy.Core.Entities
{
    [Table("Ponds", Schema = "dbo")]
    public class Pond : BaseEntity
    {
        #region Property

        [Key, Column("Id", Order = 1, TypeName = "UNIQUEIDENTIFIER")]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Column("Name", TypeName = "NVARCHAR")]
        [StringLength(128)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} cannot be null or empty")]
        public String Name { get; set; }

        [Column("Description", TypeName = "NVARCHAR")]
        [MaxLength]
        public String Description { get; set; }

        [Column("WaterVolumeCapacity", TypeName = "DECIMAL")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} cannot be null or empty")]
        public Decimal WaterVolumeCapacity { get; set; }

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

        [Column("Height", TypeName = "DECIMAL")]
        public Decimal? Height { get; set; }

        [Column("Width", TypeName = "DECIMAL")]
        public Decimal? Width { get; set; }

        [Column("Length", TypeName = "DECIMAL")]
        public Decimal? Length { get; set; }

        [Column("FaceLength", TypeName = "DECIMAL")]
        public Decimal? FaceLength { get; set; }

        [Column("BottomWidth", TypeName = "DECIMAL")]
        public Decimal? BottomWidth { get; set; }

        [Column("Radius", TypeName = "DECIMAL")]
        public Decimal? Radius { get; set; }

        [Column("Diagonal", TypeName = "DECIMAL")]
        public Decimal? Diagonal { get; set; }

        [Column("Dimension1", TypeName = "DECIMAL")]
        public Decimal? Dimension1 { get; set; }

        [Column("Dimension2", TypeName = "DECIMAL")]
        public Decimal? Dimension2 { get; set; }

        [Column("Dimension3", TypeName = "DECIMAL")]
        public Decimal? Dimension3 { get; set; }

        [Column("MinimumDistance", TypeName = "INT")]
        public Int32? MinimumDistance { get; set; }

        [Column("MaximumDistance", TypeName = "INT")]
        public Int32? MaximumDistance { get; set; }

        #endregion Property

        #region Site

        [Column("SiteId", TypeName = "UNIQUEIDENTIFIER")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} cannot be null or empty")]
        public Guid SiteId { get; set; }

        [ForeignKey("SiteId")]
        public virtual Site Site { get; set; }

        #endregion Site

        #region TankModel

        [Column("TankModelId", TypeName = "INT")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} cannot be null or empty")]
        public Int32 TankModelId { get; set; }

        [ForeignKey("TankModelId")]
        public virtual TankModel TankModel { get; set; }

        #endregion TankModel
        
        #region Sensors

        public virtual List<Sensor> Sensors { get; set; }

        #endregion Sensors
    }
}
