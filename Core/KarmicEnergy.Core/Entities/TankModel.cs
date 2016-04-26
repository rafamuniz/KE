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
        #region Property

        [Key, Column("Id", Order = 1, TypeName = "INT")]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
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

        [Column("WaterVolumeCapacity", TypeName = "DECIMAL")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} cannot be null or empty")]
        public Decimal WaterVolumeCapacity { get; set; }

        #endregion Property      

        #region Geometry

        [Column("GeometryId", TypeName = "SMALLINT")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} cannot be null or empty")]
        public Int16 GeometryId { get; set; }

        [ForeignKey("GeometryId")]
        public virtual Geometry Geometry { get; set; }

        #endregion Geometry  

        #region Load

        public static List<TankModel> Load()
        {
            List<TankModel> entities = new List<TankModel>()
            {
                new TankModel() { Id = (int)TankModelEnum.StandardTanker, Name = "Standard Tanker", GeometryId = (Int16)GeometryEnum.EllipticalHorizontal, Height = 96, Width = 144, Length = 480, ImageFilename = "1.png", WaterVolumeCapacity = 0 },
                new TankModel() { Id = (int)TankModelEnum.VerticalRoundTank, Name = "Vertical Round Tank", GeometryId = (Int16)GeometryEnum.CylinderVertical, Height = 12, Width = 8, ImageFilename = "2.png", WaterVolumeCapacity = 0 },
                new TankModel() { Id = (int)TankModelEnum.HorizontalRoundTank, Name = "Horizontal Round Tank", GeometryId = (Int16)GeometryEnum.CylinderHorizontal, Height = 12, Width = 8, ImageFilename = "3.png", WaterVolumeCapacity = 0 },
                new TankModel() { Id = (int)TankModelEnum.VerticalStadium, Name = "Vertical Stadium", GeometryId = (Int16)GeometryEnum.StadiumVertical, Height = 10, Width = 10, FaceLength = 10, ImageFilename = "4.png", WaterVolumeCapacity = 0 },
                new TankModel() { Id = (int)TankModelEnum.HorizontalStadium, Name = "Horizontal Stadium", GeometryId = (Int16)GeometryEnum.StadiumHorizontal, Height = 10, Length = 10, BottomWidth = 10, ImageFilename = "5.png", WaterVolumeCapacity = 0 },
                new TankModel() { Id = (int)TankModelEnum.CubeHorizontal, Name = "Cube Horizontal", GeometryId = (Int16)GeometryEnum.CubeHorizontal, Height = 10, Width = 144, Length = 480, ImageFilename = "6.png", WaterVolumeCapacity = 0 },
                new TankModel() { Id = (int)TankModelEnum.FracTank21K, Name = "Frac Tank 21K", GeometryId = (Int16)GeometryEnum.CubeHorizontal, Height = 120, Width = 92, Length = 500, ImageFilename = "7.png", WaterVolumeCapacity = 21000 },
                new TankModel() { Id = (int)TankModelEnum.FracPond48000bbl, Name = "Frac Pond 48000bbl", GeometryId = (Int16)GeometryEnum.CylinderVertical, Height = 144, Width = 1800, ImageFilename = "8.png", WaterVolumeCapacity = 48000 },
                new TankModel() { Id = (int)TankModelEnum.RectangleHorizontal, Name = "Rectangle Horizontal", GeometryId = (Int16)GeometryEnum.Rectangle, Height = 100, Width = 200, Length = 300, ImageFilename = "9.png", WaterVolumeCapacity = 6000000 }
            };

            return entities;
        }
        #endregion Load      

        public Decimal CalculateWaterCapacity()
        {
            if (this.Id != default(Int32))
            {
                switch (this.Id)
                {
                    // set @cubicunits = @length * @width * @height
                    case (Int32)TankModelEnum.CubeHorizontal:
                        return this.Length.Value * this.Width.Value * this.Height.Value;

                    case (Int32)TankModelEnum.HorizontalRoundTank:
                        return Length.Value * Width.Value * Height.Value;

                    case (Int32)TankModelEnum.VerticalRoundTank:
                        return Length.Value * Width.Value * Height.Value;

                    // set @area1 = @height * @dim4
                    // set @area2 = PI() * power((@height / 2), 2)
                    // set @cubicunits = (@area1 + @area2) * @length
                    case (Int32)TankModelEnum.HorizontalStadium:
                        var areaHS1 = Height * DimensionValue1;
                        var areaHS2 = Math.PI * Math.Pow((Double)(Height / 2), 2);
                        var cubicHS = (areaHS1 + (Decimal)areaHS2) * Length;
                        return cubicHS.Value;

                    // set @area1 = @length * @width
                    // set @area2 = PI() * power((@width / 2), 2)
                    // set @cubicunits = (@area1 + @area2) * @height
                    case (Int32)TankModelEnum.VerticalStadium:
                        var areaVS1 = Length * Width;
                        var areaVS2 = Math.PI * Math.Pow((double)(Width / 2), 2);
                        var cubicVS = (areaVS1 + (Decimal)areaVS2) * Height;
                        return cubicVS.Value;

                    // set @area = PI() * (@height / 2) * (@width / 2)
                    // set @cubicunits = @area * @length
                    case (Int32)TankModelEnum.StandardTanker:
                        var areaST = Math.PI * Math.Pow((double)(Height / 2), (Double)(Width / 2));
                        var cubicST = (Decimal)areaST * Length;
                        return cubicST.Value;

                    case (Int32)TankModelEnum.FracPond48000bbl:
                        return 48000;

                    case (Int32)TankModelEnum.FracTank21K:
                        return 21000;

                    default:
                        return Length.Value * Width.Value * Height.Value;
                }
            }

            return Length.Value * Width.Value * Height.Value;
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
