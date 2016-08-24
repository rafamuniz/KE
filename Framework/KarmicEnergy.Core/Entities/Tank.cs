using KarmicEnergy.Core.Entities.Interface;
using Munizoft.Util.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KarmicEnergy.Core.Entities
{
    [Table("Tanks", Schema = "dbo")]
    public class Tank : BaseEntity, IConvertUnit
    {
        #region Constructor
        public Tank()
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

        [Column("WaterVolumeCapacity", TypeName = "DECIMAL")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} cannot be null or empty")]
        public Decimal WaterVolumeCapacity { get; set; }

        #region Unit

        [Column("WaterVolumeCapacityUnitId", TypeName = "SMALLINT")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} cannot be null or empty")]
        [DefaultValue((Int16)UnitEnum.Barrel)]
        public Int16 WaterVolumeCapacityUnitId { get; set; } = (Int16)UnitEnum.Barrel;

        [ForeignKey("WaterVolumeCapacityUnitId")]
        public virtual Unit WaterVolumeCapacityUnit { get; set; }

        #endregion Unit

        [Column("Reference", TypeName = "NVARCHAR")]
        [StringLength(8)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} cannot be null or empty")]
        public String Reference { get; set; }

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

        #region StickConversion

        [Column("StickConversionId", TypeName = "UNIQUEIDENTIFIER")]
        public Guid? StickConversionId { get; set; }

        [ForeignKey("StickConversionId")]
        public virtual StickConversion StickConversion { get; set; }

        #endregion StickConversion

        #region Sensors

        public virtual List<Sensor> Sensors { get; set; }

        #endregion Sensors

        #region Functions   
        public Decimal CalculateWaterCapacity()
        {
            if (this.TankModelId != default(Int32))
            {
                switch (this.TankModelId)
                {
                    // Id: 1
                    // set @area = PI() * (@height / 2) * (@width / 2)
                    // set @cubicunits = @area * @length
                    case (Int32)TankModelEnum.StandardTanker:
                        if (this.Length.HasValue && this.Width.HasValue && this.Height.HasValue)
                        {
                            var areaST = Math.PI * Math.Pow((double)(Height / 2), (Double)(Width / 2));
                            var cubicST = (Decimal)areaST * Length;
                            return cubicST.Value;
                        }
                        else
                            return 0;

                    // Id: 2
                    case (Int32)TankModelEnum.VerticalRoundTank:
                        if (this.Length.HasValue && this.Width.HasValue && this.Height.HasValue)
                            return Length.Value * Width.Value * Height.Value;
                        else
                            return 0;

                    // Id: 3              
                    case (Int32)TankModelEnum.HorizontalRoundTank:
                        if (this.Length.HasValue && this.Width.HasValue && this.Height.HasValue)
                            return Length.Value * Width.Value * Height.Value;
                        else
                            return 0;

                    // Id: 4                    
                    // set @area1 = @length * @width
                    // set @area2 = PI() * power((@width / 2), 2)
                    // set @cubicunits = (@area1 + @area2) * @height
                    case (Int32)TankModelEnum.VerticalStadium:
                        if (this.Length.HasValue && this.Width.HasValue && this.Height.HasValue)
                        {
                            var areaVS1 = Length * Width;
                            var areaVS2 = Math.PI * Math.Pow((double)(Width / 2), 2);
                            var cubicVS = (areaVS1 + (Decimal)areaVS2) * Height;
                            return cubicVS.Value;
                        }
                        else
                            return 0;

                    // Id: 5                    
                    // set @area1 = @height * @dim4
                    // set @area2 = PI() * power((@height / 2), 2)
                    // set @cubicunits = (@area1 + @area2) * @length
                    case (Int32)TankModelEnum.HorizontalStadium:
                        if (this.Length.HasValue && this.Dimension1.HasValue && this.Height.HasValue)
                        {
                            var areaHS1 = Height * Dimension1;
                            var areaHS2 = Math.PI * Math.Pow((Double)(Height / 2), 2);
                            var cubicHS = (areaHS1 + (Decimal)areaHS2) * Length;
                            return cubicHS.Value;
                        }
                        else
                            return 0;

                    // Id: 6 - set @cubicunits = @length * @width * @height
                    case (Int32)TankModelEnum.CubeHorizontal:
                        if (this.Length.HasValue && this.Width.HasValue && this.Height.HasValue)
                            return this.Length.Value * this.Width.Value * this.Height.Value;
                        else
                            return 0;

                    // Id: 7
                    case (Int32)TankModelEnum.FracTank21K:
                        return 21000;

                    // Id: 8
                    case (Int32)TankModelEnum.FracPond48000bbl:
                        return 48000;

                    // Id: 9
                    case (Int32)TankModelEnum.RectangleHorizontal:
                        if (this.Length.HasValue && this.Width.HasValue && this.Height.HasValue)
                            return Length.Value * Width.Value * Height.Value;
                        else
                            return 0;

                    default:
                        if (this.Length.HasValue && this.Width.HasValue && this.Height.HasValue)
                            return Length.Value * Width.Value * Height.Value;
                        else
                            return 0;
                }
            }

            return 0;
        }

        public Decimal CalculateWaterRemaining(Decimal range)
        {
            if (this.TankModelId != default(Int32))
            {
                switch (this.TankModelId)
                {
                    // set @cubicunits = @length * @width * @height
                    case (Int32)TankModelEnum.CubeHorizontal:
                        return this.Length.Value * this.Width.Value * range;

                    case (Int32)TankModelEnum.HorizontalRoundTank:
                        return Length.Value * Width.Value * range;

                    case (Int32)TankModelEnum.VerticalRoundTank:
                        return Length.Value * Width.Value * range;

                    // set @area1 = @height * @dim4
                    // set @area2 = PI() * power((@height / 2), 2)
                    // set @cubicunits = (@area1 + @area2) * @length
                    case (Int32)TankModelEnum.HorizontalStadium:
                        var areaHS1 = range * Dimension1;
                        var areaHS2 = Math.PI * Math.Pow((Double)(range / 2), 2);
                        var cubicHS = (areaHS1 + (Decimal)areaHS2) * Length;
                        return cubicHS.Value;

                    // set @area1 = @length * @width
                    // set @area2 = PI() * power((@width / 2), 2)
                    // set @cubicunits = (@area1 + @area2) * @height
                    case (Int32)TankModelEnum.VerticalStadium:
                        var areaVS1 = Length * Width;
                        var areaVS2 = Math.PI * Math.Pow((double)(Width / 2), 2);
                        var cubicVS = (areaVS1 + (Decimal)areaVS2) * range;
                        return cubicVS.Value;

                    // set @area = PI() * (@height / 2) * (@width / 2)
                    // set @cubicunits = @area * @length
                    case (Int32)TankModelEnum.StandardTanker:
                        var areaST = Math.PI * Math.Pow((double)(range / 2), (Double)(Width / 2));
                        var cubicST = (Decimal)areaST * Length;
                        return cubicST.Value;

                    case (Int32)TankModelEnum.FracPond48000bbl:
                        return 48000;

                    case (Int32)TankModelEnum.FracTank21K:
                        return 21000;

                    default:
                        return Length.Value * Width.Value * range;
                }
            }

            return Length.Value * Width.Value * range;
        }

        public String Convert()
        {
            throw new Exception("Error convert");
        }

        /// <summary>
        /// Tank Capacity Default Unit is GALLON
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
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

        public void Update(Tank entity)
        {
            this.Name = entity.Name;
            this.Description = entity.Description;
            this.Reference = entity.Reference;
            this.Status = entity.Status;
            this.Longitude = entity.Longitude;
            this.Latitude = entity.Latitude;

            this.Height = entity.Height;
            this.Length = entity.Length;
            this.Width = entity.Width;

            this.BottomWidth = entity.BottomWidth;
            this.FaceLength = entity.FaceLength;
            this.Radius = entity.Radius;
            this.Diagonal = entity.Diagonal;
            this.Dimension1 = entity.Dimension1;
            this.Dimension2 = entity.Dimension2;
            this.Dimension3 = entity.Dimension3;

            this.MinimumDistance = entity.MinimumDistance;
            this.MaximumDistance = entity.MaximumDistance;
            this.WaterVolumeCapacity = entity.WaterVolumeCapacity;
            this.WaterVolumeCapacityUnitId = entity.WaterVolumeCapacityUnitId;

            this.SiteId = entity.SiteId;
            this.TankModelId = entity.TankModelId;
            this.StickConversionId = entity.StickConversionId;

            this.CreatedDate = entity.CreatedDate;
            this.LastModifiedDate = entity.LastModifiedDate;
            this.DeletedDate = entity.DeletedDate;
        }
        #endregion Functions   
    }
}
