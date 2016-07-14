using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KarmicEnergy.Core.Entities
{
    [Table("TankModels", Schema = "dbo")]
    public class TankModel : BaseEntity
    {
        #region Constructor
        public TankModel()
        {
        
        }
        #endregion Constructor

        #region Property

        [Key, Column("Id", Order = 1, TypeName = "INT")]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.None)]
        public Int32 Id { get; set; }

        [Column("Name", TypeName = "NVARCHAR")]
        [StringLength(128)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} cannot be null or empty")]
        public String Name { get; set; }

        [Column("ImageFileName", TypeName = "NVARCHAR")]
        [StringLength(256)]
        public String ImageFilename { get; set; }

        [Column("Image", TypeName = "VARBINARY")]
        [MaxLength]
        public Byte[] Image { get; set; }

        [Column("Status", TypeName = "CHAR")]
        [StringLength(1)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} cannot be null or empty")]
        public String Status { get; set; } = "A";

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

        [Column("DimensionValue1", TypeName = "DECIMAL")]
        public Decimal? DimensionValue1 { get; set; }

        [Column("DimensionValue2", TypeName = "DECIMAL")]
        public Decimal? DimensionValue2 { get; set; }

        [Column("DimensionValue3", TypeName = "DECIMAL")]
        public Decimal? DimensionValue3 { get; set; }

        [Column("WaterVolumeCapacity", TypeName = "FLOAT")]
        public Double? WaterVolumeCapacity { get; set; }

        #endregion Property      

        #region Geometry

        [Column("GeometryId", TypeName = "SMALLINT")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} cannot be null or empty")]
        public Int16 GeometryId { get; set; }

        [ForeignKey("GeometryId")]
        public virtual Geometry Geometry { get; set; }

        #endregion Geometry  

        #region Functions

        public static List<TankModel> Load()
        {
            List<TankModel> entities = new List<TankModel>()
            {
                new TankModel() { Id = (int)TankModelEnum.StandardTanker, Name = "Standard Tanker", GeometryId = (Int16)GeometryEnum.EllipticalHorizontal, Height = 96, Width = 144, Length = 480, ImageFilename = "EllipseHoriz-{color}-{info}.png", WaterVolumeCapacity = 0 },
                new TankModel() { Id = (int)TankModelEnum.VerticalRoundTank, Name = "Vertical Round Tank", GeometryId = (Int16)GeometryEnum.CylinderVertical, Height = 12, Width = 8, ImageFilename = "CylVert-{color}-{info}.png", WaterVolumeCapacity = 0 },
                new TankModel() { Id = (int)TankModelEnum.HorizontalRoundTank, Name = "Horizontal Round Tank", GeometryId = (Int16)GeometryEnum.CylinderHorizontal, Height = 12, Length = 8, ImageFilename = "CylHoriz-{color}-{info}.png", WaterVolumeCapacity = 0 },
                new TankModel() { Id = (int)TankModelEnum.VerticalStadium, Name = "Vertical Stadium", GeometryId = (Int16)GeometryEnum.StadiumVertical, Height = 10, Width = 10, FaceLength = 10, ImageFilename = "StadiumVert-{color}-{info}.png", WaterVolumeCapacity = 0 },
                new TankModel() { Id = (int)TankModelEnum.HorizontalStadium, Name = "Horizontal Stadium", GeometryId = (Int16)GeometryEnum.StadiumHorizontal, Height = 10, Length = 10, BottomWidth = 10, ImageFilename = "StadiumHoriz-{color}-{info}.png", WaterVolumeCapacity = 0 },
                new TankModel() { Id = (int)TankModelEnum.CubeHorizontal, Name = "Cube Horizontal", GeometryId = (Int16)GeometryEnum.Rectangle, Height = 10, Width = 144, Length = 480, ImageFilename = "CubeHoriz-{color}-{info}.png", WaterVolumeCapacity = 0 },
                new TankModel() { Id = (int)TankModelEnum.FracTank21K, Name = "Frac Tank 21K", GeometryId = (Int16)GeometryEnum.Rectangle, Height = 120, Width = 92, Length = 500, ImageFilename = "FracFA1-{color}-{info}.png", WaterVolumeCapacity = 21000 },
                new TankModel() { Id = (int)TankModelEnum.FracPond48000bbl, Name = "Frac Pond 48000bbl", GeometryId = (Int16)GeometryEnum.CylinderVertical, Height = 144, Width = 1800, ImageFilename = "FracPO1-{color}-{info}.png", WaterVolumeCapacity = 48000 },
            };

            return entities;
        }
        public void Update(TankModel entity)
        {
            this.Name = entity.Name;
            this.BottomWidth = entity.BottomWidth;
            this.Diagonal = entity.Diagonal;
            this.DimensionValue1 = entity.DimensionValue1;
            this.DimensionValue2 = entity.DimensionValue2;
            this.DimensionValue3 = entity.DimensionValue3;
            this.FaceLength = entity.FaceLength;
            this.GeometryId = entity.GeometryId;
            this.Height = entity.Height;
            this.ImageFilename = entity.ImageFilename;
            this.Length = entity.Length;
            this.Radius = entity.Radius;
            this.Status = entity.Status;
            this.Width = entity.Width;
            this.WaterVolumeCapacity = entity.WaterVolumeCapacity;
            
            this.CreatedDate = entity.CreatedDate;
            this.LastModifiedDate = entity.LastModifiedDate;
            this.DeletedDate = entity.DeletedDate;
        }
        #endregion Functions    

        //public Decimal CalculateWaterCapacity()
        //{
        //    if (this.Id != default(Int32))
        //    {
        //        switch (this.Id)
        //        {
        //            // set @cubicunits = @length * @width * @height
        //            case (Int32)TankModelEnum.CubeHorizontal:
        //                return this.Length.Value * this.Height.Value * this.Width.Value;

        //            case (Int32)TankModelEnum.HorizontalRoundTank:
        //                return Height.Value * Length.Value * BottomWidth.Value;

        //            case (Int32)TankModelEnum.VerticalRoundTank:
        //                return Width.Value * Height.Value * FaceLength.Value;

        //            // set @area1 = @height * @dim4
        //            // set @area2 = PI() * power((@height / 2), 2)
        //            // set @cubicunits = (@area1 + @area2) * @length
        //            case (Int32)TankModelEnum.HorizontalStadium:
        //                var areaHS1 = Height * DimensionValue1;
        //                var areaHS2 = Math.PI * Math.Pow((Double)(Height / 2), 2);
        //                var cubicHS = (areaHS1 + (Decimal)areaHS2) * Length;
        //                return cubicHS.Value;

        //            // set @area1 = @length * @width
        //            // set @area2 = PI() * power((@width / 2), 2)
        //            // set @cubicunits = (@area1 + @area2) * @height
        //            case (Int32)TankModelEnum.VerticalStadium:
        //                var areaVS1 = Length * Width;
        //                var areaVS2 = Math.PI * Math.Pow((double)(Width / 2), 2);
        //                var cubicVS = (areaVS1 + (Decimal)areaVS2) * Height;
        //                return cubicVS.Value;

        //            // set @area = PI() * (@height / 2) * (@width / 2)
        //            // set @cubicunits = @area * @length
        //            case (Int32)TankModelEnum.StandardTanker:
        //                var areaST = Math.PI * Math.Pow((double)(Height / 2), (Double)(Width / 2));
        //                var cubicST = (Decimal)areaST * Length;
        //                return cubicST.Value;

        //            case (Int32)TankModelEnum.FracPond48000bbl:
        //                return 48000;

        //            case (Int32)TankModelEnum.FracTank21K:
        //                return 21000;

        //            default:
        //                return Length.Value * Width.Value * Height.Value;
        //        }
        //    }

        //    return Length.Value * Width.Value * Height.Value;
        //}

        public Double? CalculateWaterCapacity()
        {
            if (this.GeometryId != default(Int16))
            {
                switch (this.GeometryId)
                {
                    // Formula: @width 3
                    case (Int16)GeometryEnum.Cube:
                        if (this.Width.HasValue)
                        {
                            var result = Math.Pow((Double)this.Width.Value, 3);
                            return result;
                        }
                        else
                            break;

                    // Formula: @length * @width * @height
                    case (Int16)GeometryEnum.Rectangle:
                        if (this.Length.HasValue && this.Height.HasValue && this.Width.HasValue)
                        {
                            return (Double)(this.Length.Value * this.Height.Value * this.Width.Value);
                        }
                        else
                            break;

                    case (Int16)GeometryEnum.CylinderHorizontal:
                        if (this.Height.HasValue && this.Length.HasValue && this.BottomWidth.HasValue)
                        {
                            return (Double)(Height.Value * Length.Value * BottomWidth.Value);
                        }
                        else
                            break;

                    case (Int16)GeometryEnum.CylinderVertical:
                        if (Width.HasValue && this.Height.HasValue)
                        {
                            return (Double)(Width.Value * Height.Value);
                        }
                        else
                            break;

                    // set @area1 = @height * @dim4
                    // set @area2 = PI() * power((@height / 2), 2)
                    // set @cubicunits = (@area1 + @area2) * @length
                    case (Int16)GeometryEnum.StadiumHorizontal:
                        if (this.Height.HasValue && this.DimensionValue1.HasValue && this.Height.HasValue)
                        {
                            var areaHS1 = (Double)Height * (Double)DimensionValue1;
                            var areaHS2 = Math.PI * Math.Pow(((Double)Height / 2), 2);
                            var cubicHS = (areaHS1 + areaHS2) * (Double)Length;
                            return cubicHS;
                        }
                        else
                            break;

                    // set @area1 = @length * @width
                    // set @area2 = PI() * power((@width / 2), 2)
                    // set @cubicunits = (@area1 + @area2) * @height
                    case (Int16)GeometryEnum.StadiumVertical:
                        if (this.Length.HasValue && this.Width.HasValue && this.Height.HasValue)
                        {
                            var areaVS1 = (Double)Length * (Double)Width;
                            var areaVS2 = Math.PI * Math.Pow(((Double)Width / 2), 2);
                            var cubicVS = (areaVS1 + areaVS2) * (Double)Height;
                            return cubicVS;
                        }
                        else
                            break;

                    // set @area = PI() * (@height / 2) * (@width / 2)
                    // set @cubicunits = @area * @length
                    case (Int16)GeometryEnum.EllipticalHorizontal:
                        if (this.Height.HasValue && this.Length.HasValue)
                        {
                            var areaST = Math.PI * Math.Pow((Double)(Height.Value / 2), (Double)(Width.Value / 2));
                            var cubicST = areaST * (Double)Length.Value;
                            return cubicST;
                        }
                        else
                            break;
                    default:
                        return null;
                }
            }

            return null;
        }

        /// <summary>
        /// Get the integral and floating point portions of a Double
        /// as separate integer values, where the floating point value is 
        /// raised to the specified power of ten, given by 'places'.
        /// </summary>
        public static void Split(Double value, Int32 places, out Int32 left, out Int32 right)
        {
            left = (Int32)Math.Truncate(value);
            right = (Int32)((value - left) * Math.Pow(10, places));
        }

        public static void Split(Double value, out Int32 left, out Int32 right)
        {
            Split(value, 1, out left, out right);
        }

        public Decimal? CalculateWaterRemaining(Int32 minimalDistance, Int32 maxDistance)
        {
            if (this.GeometryId != default(Int16))
            {
                var len = maxDistance - minimalDistance;

                switch (this.GeometryId)
                {
                    // Formula: @width 3
                    case (Int16)GeometryEnum.Cube:
                        return this.Width.Value;

                    // Formula: @length * @width * @height
                    case (Int16)GeometryEnum.Rectangle:
                        return this.Length.Value * this.Height.Value * this.Width.Value;

                    case (Int16)GeometryEnum.CylinderHorizontal:
                        return Height.Value * Length.Value * BottomWidth.Value;

                    case (Int16)GeometryEnum.CylinderVertical:
                        return Width.Value * Height.Value * FaceLength.Value;

                    // set @area1 = @height * @dim4
                    // set @area2 = PI() * power((@height / 2), 2)
                    // set @cubicunits = (@area1 + @area2) * @length
                    case (Int16)GeometryEnum.StadiumHorizontal:
                        var areaHS1 = Height * DimensionValue1;
                        var areaHS2 = Math.PI * Math.Pow((Double)(Height / 2), 2);
                        var cubicHS = (areaHS1 + (Decimal)areaHS2) * Length;
                        return cubicHS.Value;

                    // set @area1 = @length * @width
                    // set @area2 = PI() * power((@width / 2), 2)
                    // set @cubicunits = (@area1 + @area2) * @height
                    case (Int16)GeometryEnum.StadiumVertical:
                        var areaVS1 = Length * Width;
                        var areaVS2 = Math.PI * Math.Pow((double)(Width / 2), 2);
                        var cubicVS = (areaVS1 + (Decimal)areaVS2) * Height;
                        return cubicVS.Value;

                    // set @area = PI() * (@height / 2) * (@width / 2)
                    // set @cubicunits = @area * @length
                    case (Int16)GeometryEnum.EllipticalHorizontal:
                        var areaST = Math.PI * Math.Pow((double)(Height / 2), (Double)(Width / 2));
                        var cubicST = (Decimal)areaST * Length;
                        return cubicST.Value;

                    default:
                        return null;
                }
            }

            return null;
        }
    }

    public enum TankModelEnum : int
    {
        [Description("Standard Tanker")]
        StandardTanker = 1,

        [Description("Vertical Round Tank")]
        VerticalRoundTank = 2,

        [Description("Horizontal Round Tank")]
        HorizontalRoundTank = 3,

        [Description("Vertical Stadium")]
        VerticalStadium = 4,

        [Description("Horizontal Staduim")]
        HorizontalStadium = 5,

        [Description("Cube Horizontal")]
        CubeHorizontal = 6,

        [Description("Frac Tank 21K")]
        FracTank21K = 7,

        [Description("Frac Pond 48000bbl")]
        FracPond48000bbl = 8,

        [Description("Rectangle Horizontal")]
        RectangleHorizontal = 9,
    }
}