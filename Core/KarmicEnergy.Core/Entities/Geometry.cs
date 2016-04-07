using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KarmicEnergy.Core.Entities
{
    [Table("Geometries", Schema = "dbo")]
    public class Geometry : BaseEntity
    {
        #region Property

        [Key, Column("Id", Order = 1, TypeName = "SMALLINT")]
        public Int16 Id { get; set; }

        [Column("Name", TypeName = "NVARCHAR")]
        [StringLength(128)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} cannot be null or empty")]
        public String Name { get; set; }

        [Column("HasHeight", TypeName = "BIT")]
        public Boolean HasHeight { get; set; } = false;

        [Column("HeightValue", TypeName = "DECIMAL")]
        public Decimal HeightValue { get; set; } = default(Decimal);

        [Column("HasWidth", TypeName = "BIT")]
        public Boolean HasWidth { get; set; } = false;

        [Column("WidthValue", TypeName = "DECIMAL")]
        public Decimal WidthValue { get; set; } = default(Decimal);

        [Column("HasLength", TypeName = "BIT")]
        public Boolean HasLength { get; set; } = false;

        [Column("LengthValue", TypeName = "DECIMAL")]
        public Decimal LengthValue { get; set; } = default(Decimal);

        [Column("HasFaceLength", TypeName = "BIT")]
        public Boolean HasFaceLength { get; set; } = false;

        [Column("FaceLengthValue", TypeName = "DECIMAL")]
        public Decimal FaceLengthValue { get; set; } = default(Decimal);

        [Column("HasBottomWidth", TypeName = "BIT")]
        public Boolean HasBottomWidth { get; set; } = false;

        [Column("BottomWidthValue", TypeName = "DECIMAL")]
        public Decimal BottomWidthValue { get; set; } = default(Decimal);

        [Column("HasDim1", TypeName = "BIT")]
        public Boolean HasDim1 { get; set; } = false;

        [Column("Dim1Description", TypeName = "NVARCHAR")]
        [StringLength(32)]
        public String Dim1Description { get; set; }

        [Column("Dim1Value", TypeName = "DECIMAL")]
        public Decimal Dim1Value { get; set; } = default(Decimal);

        [Column("HasDim2", TypeName = "BIT")]
        public Boolean HasDim2 { get; set; } = false;

        [Column("Dim2Description", TypeName = "NVARCHAR")]
        [StringLength(32)]
        public String Dim2Description { get; set; }

        [Column("Dim2Value", TypeName = "DECIMAL")]
        public Decimal Dim2Value { get; set; } = default(Decimal);

        [Column("HasDim3", TypeName = "BIT")]
        public Boolean HasDim3 { get; set; } = false;

        [Column("Dim3Description", TypeName = "NVARCHAR")]
        [StringLength(32)]
        public String Dim3Description { get; set; }

        [Column("Dim3Value", TypeName = "DECIMAL")]
        public Decimal Dim3Value { get; set; } = default(Decimal);

        #endregion Property

        #region Load

        public static List<Geometry> Load()
        {
            List<Geometry> entities = new List<Geometry>()
            {
                new Geometry() { Id = (Int16)GeometryEnum.CubeHorizontal, Name = "Cube Horizontal", HasHeight = true, HeightValue = 0, HasWidth = true, WidthValue = 0, HasLength = true, LengthValue = 0 },
                new Geometry() { Id = (Int16)GeometryEnum.StadiumVertical, Name = "Stadium Vertical", HasHeight = true, HeightValue = 0, HasWidth = true, WidthValue = 0, HasLength = true, LengthValue = 0 },
                new Geometry() { Id = (Int16)GeometryEnum.StadiumHorizontal, Name = "Stadium Horizontal", HasHeight = true, HeightValue = 0, HasWidth = true, WidthValue = 0, HasLength = true, LengthValue = 0, HasBottomWidth = true, BottomWidthValue = 0 },
                new Geometry() { Id = (Int16)GeometryEnum.EllipticalHorizontal, Name = "Elliptical Horizontal", HasHeight = true, HeightValue = 0, HasWidth = true, WidthValue = 0 , HasLength = true, LengthValue = 0 },
                new Geometry() { Id = (Int16)GeometryEnum.CylinderVertical, Name = "Cylinder Vertical", HasHeight = true, HeightValue = 0, HasWidth = true, WidthValue = 0 },
                new Geometry() { Id = (Int16)GeometryEnum.CylinderHorizontal, Name = "Cylinder Horizontal", HasHeight = true, HeightValue = 0, HasLength = true, LengthValue = 0 }
            };

            return entities;
        }
        #endregion Load 
    }

    public enum GeometryEnum : short
    {
        [Description("Cube Horizontal")]
        CubeHorizontal = 1,

        [Description("Stadium Vertical")]
        StadiumVertical = 2,

        [Description("Stadium Horizontal")]
        StadiumHorizontal = 3,

        [Description("Elliptical Horizontal")]
        EllipticalHorizontal = 4,

        [Description("Cylinder Vertical")]
        CylinderVertical = 5,

        [Description("Cylinder Horizontal")]
        CylinderHorizontal = 6
    }
}
