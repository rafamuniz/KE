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

        [Column("Dim1", TypeName = "DECIMAL")]
        public Decimal? Dim1 { get; set; }

        [Column("Dim2Value", TypeName = "DECIMAL")]
        public Decimal? Dim2 { get; set; }

        [Column("Dim3Value", TypeName = "DECIMAL")]
        public Decimal? Dim3 { get; set; }

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
                new TankModel() { Id = (int)TankModelEnum.StandardTanker, Name = "Standard Tanker", GeometryId = (Int16)GeometryEnum.EllipticalHorizontal, Height = 100, Width = 100, Length = 100, ImageFilename = "1.png" },
                new TankModel() { Id = (int)TankModelEnum.VerticalRoundTank, Name = "Vertical Round Tank", GeometryId = (Int16)GeometryEnum.CylinderVertical, Height = 100, Width = 100, Length = 100, ImageFilename = "2.png" },
                new TankModel() { Id = (int)TankModelEnum.HorizontalRoundTank, Name = "Horizontal Round Tank", GeometryId = (Int16)GeometryEnum.CylinderHorizontal, Height = 100, Width = 100, Length = 100, ImageFilename = "3.png" },
                new TankModel() { Id = (int)TankModelEnum.VerticalStadium, Name = "Vertical Stadium", GeometryId = (Int16)GeometryEnum.StadiumVertical, Height = 100, Width = 100, Length = 100, ImageFilename = "4.png" },
                new TankModel() { Id = (int)TankModelEnum.HorizontalStadium, Name = "Horizontal Stadium", GeometryId = (Int16)GeometryEnum.StadiumHorizontal, Height = 100, Width = 100, Length = 100, ImageFilename = "5.png" },
                new TankModel() { Id = (int)TankModelEnum.CubeHorizontal, Name = "Cube Horizontal", GeometryId = (Int16)GeometryEnum.CubeHorizontal, Height = 100, Width = 100, Length = 100, ImageFilename = "6.png" },
                new TankModel() { Id = (int)TankModelEnum.FracTank21K, Name = "Frac Tank 21K", GeometryId = (Int16)GeometryEnum.CubeHorizontal, Height = 100, Width = 100, Length = 100, ImageFilename = "7.png" },
                new TankModel() { Id = (int)TankModelEnum.FracPond48000bbl, Name = "Frac Pond 48000bbl", GeometryId = (Int16)GeometryEnum.CylinderVertical, Height = 100, Width = 100, Length = 100, ImageFilename = "8.png" }
            };

            return entities;
        }
        #endregion Load 
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
        FracPond48000bbl = 8
    }
}
