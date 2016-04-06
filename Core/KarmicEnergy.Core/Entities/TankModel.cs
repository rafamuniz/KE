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

        [Column("Height", TypeName = "BIT")]
        public Boolean Height { get; set; }

        [Column("Width", TypeName = "BIT")]
        public Boolean Width { get; set; }

        [Column("Length", TypeName = "BIT")]
        public Boolean Length { get; set; }

        [Column("FaceLength", TypeName = "BIT")]
        public Boolean FaceLength { get; set; }

        [Column("BottomWidth", TypeName = "BIT")]
        public Boolean BottomWidth { get; set; }

        #endregion Property       

        #region Load

        public static List<TankModel> Load()
        {
            List<TankModel> entities = new List<TankModel>()
            {
                new TankModel() { Id = (int)TankModelEnum.StandardTanker, Name = "Standard Tanker", Height = false, Width = false, BottomWidth = false, FaceLength = false, Length = false, ImageFilename = "1.png" },
                new TankModel() { Id = (int)TankModelEnum.VerticalRoundTank, Name = "Vertical Round Tank", Height = false, Width = false, BottomWidth = false, FaceLength = false, Length = false, ImageFilename = "2.png" },
                new TankModel() { Id = (int)TankModelEnum.HorizontalRoundTank,  Name = "Horizontal Round Tank", Height = false, Width = false, BottomWidth = false, FaceLength = false, Length = false, ImageFilename = "3.png" },
                new TankModel() { Id = (int)TankModelEnum.VerticalStadium,  Name = "Vertical Stadium", Height = false, Width = false, BottomWidth = false, FaceLength = false, Length = false, ImageFilename = "4.png" },
                new TankModel() { Id = (int)TankModelEnum.HorizontalStaduim,  Name = "Horizontal Staduim", Height = false, Width = false, BottomWidth = false, FaceLength = false, Length = false, ImageFilename = "5.png" },
                new TankModel() { Id = (int)TankModelEnum.CubeHorizontal, Name = "Cube Horizontal", Height = false, Width = false, BottomWidth = false, FaceLength = false, Length = false, ImageFilename = "6.png" },
                new TankModel() { Id = (int)TankModelEnum.FracTank21K, Name = "Frac Tank 21K", Height = false, Width = false, BottomWidth = false, FaceLength = false, Length = false, ImageFilename = "7.png" },
                new TankModel() { Id = (int)TankModelEnum.FracPond48000bbl, Name = "Frac Pond 48000bbl", Height = false, Width = false, BottomWidth = false, FaceLength = false, Length = false, ImageFilename = "8.png" }
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
        HorizontalStaduim = 5,

        [Description("Cube Horizontal")]
        CubeHorizontal = 6,

        [Description("Frac Tank 21K")]
        FracTank21K = 7,

        [Description("Frac Pond 48000bbl")]
        FracPond48000bbl = 8
    }
}
