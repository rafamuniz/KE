﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KarmicEnergy.Core.Entities
{
    [Table("Tanks", Schema = "dbo")]
    public class Tank : BaseEntity
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

        private Decimal _WaterVolumeCapacity = default(Decimal);

        [Column("WaterVolumeCapacity", TypeName = "DECIMAL")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} cannot be null or empty")]
        public Decimal WaterVolumeCapacity
        {
            get
            {
                switch (TankModelId)
                {
                    // set @cubicunits = @length * @width * @height
                    case (Int32)TankModelEnum.CubeHorizontal:
                        return Length * Width * Height;

                    case (Int32)TankModelEnum.HorizontalRoundTank:
                        return Length * Width * Height;

                    case (Int32)TankModelEnum.VerticalRoundTank:
                        return Length * Width * Height;

                    // set @area1 = @height * @dim4
                    // set @area2 = PI() * power((@height / 2), 2)
                    // set @cubicunits = (@area1 + @area2) * @length
                    case (Int32)TankModelEnum.HorizontalStadium:
                        var areaHS1 = Height * Dim1;
                        var areaHS2 = Math.PI * Math.Pow((Double)(Height / 2), 2);
                        var cubicHS = (areaHS1 + (Decimal)areaHS2) * Length;
                        return cubicHS;

                    // set @area1 = @length * @width
                    // set @area2 = PI() * power((@width / 2), 2)
                    // set @cubicunits = (@area1 + @area2) * @height
                    case (Int32)TankModelEnum.VerticalStadium:
                        var areaVS1 = Length * Width;
                        var areaVS2 = Math.PI * Math.Pow((double)(Width / 2), 2);
                        var cubicVS = (areaVS1 + (Decimal)areaVS2) * Height;
                        return cubicVS;

                    // set @area = PI() * (@height / 2) * (@width / 2)
                    // set @cubicunits = @area * @length
                    case (Int32)TankModelEnum.StandardTanker:
                        var areaST = Math.PI * Math.Pow((double)(Height / 2), (Double)(Width / 2));
                        var cubicST = (Decimal)areaST * Length;
                        return cubicST;

                    case (Int32)TankModelEnum.FracPond48000bbl:
                        return 48000;

                    case (Int32)TankModelEnum.FracTank21K:
                        return 21000;

                    default:
                        return default(Decimal);

                }
            }

            set { _WaterVolumeCapacity = value; }
        }

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

        [Column("SpotGPS", TypeName = "NVARCHAR")]
        [StringLength(128)]
        public String SpotGPS { get; set; }

        [Column("Height", TypeName = "DECIMAL")]
        public Decimal Height { get; set; }

        [Column("Width", TypeName = "DECIMAL")]
        public Decimal Width { get; set; }

        [Column("Length", TypeName = "DECIMAL")]
        public Decimal Length { get; set; }

        [Column("Dim1", TypeName = "DECIMAL")]
        public Decimal Dim1 { get; set; }

        [Column("Dim2", TypeName = "DECIMAL")]
        public Decimal Dim2 { get; set; }

        [Column("Dim3", TypeName = "DECIMAL")]
        public Decimal Dim3 { get; set; }

        [Column("MinimumDistance", TypeName = "INT")]
        public Int32 MinimumDistance { get; set; }

        [Column("MaximumDistance", TypeName = "INT")]
        public Int32 MaximumDistance { get; set; }

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
