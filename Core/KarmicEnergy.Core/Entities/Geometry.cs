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

        [Column("HasWidth", TypeName = "BIT")]
        public Boolean HasWidth { get; set; } = false;

        [Column("HasLength", TypeName = "BIT")]
        public Boolean HasLength { get; set; } = false;

        [Column("HasFaceLength", TypeName = "BIT")]
        public Boolean HasFaceLength { get; set; } = false;

        [Column("HasBottomWidth", TypeName = "BIT")]
        public Boolean HasBottomWidth { get; set; } = false;

        [Column("HasDim1", TypeName = "BIT")]
        public Boolean HasDimension1 { get; set; } = false;

        [Column("DimensionTitle1", TypeName = "NVARCHAR")]
        [StringLength(32)]
        public String DimensionTitle1 { get; set; }

        [Column("HasDim2", TypeName = "BIT")]
        public Boolean HasDimension2 { get; set; } = false;

        [Column("DimensionTitle2", TypeName = "NVARCHAR")]
        [StringLength(32)]
        public String DimensionTitle2 { get; set; }

        [Column("HasDim3", TypeName = "BIT")]
        public Boolean HasDimension3 { get; set; } = false;

        [Column("DimensionTitle3", TypeName = "NVARCHAR")]
        [StringLength(32)]
        public String DimensionTitle3 { get; set; }

        #endregion Property

        #region Load

        public static List<Geometry> Load()
        {
            List<Geometry> entities = new List<Geometry>()
            {
                new Geometry() { Id = (Int16)GeometryEnum.CubeHorizontal, Name = "Cube Horizontal", HasHeight = true, HasWidth = true, HasLength = true },
                new Geometry() { Id = (Int16)GeometryEnum.StadiumVertical, Name = "Stadium Vertical", HasHeight = true, HasWidth = true, HasLength = true },
                new Geometry() { Id = (Int16)GeometryEnum.StadiumHorizontal, Name = "Stadium Horizontal", HasHeight = true, HasWidth = true, HasLength = true, HasBottomWidth = true },
                new Geometry() { Id = (Int16)GeometryEnum.EllipticalHorizontal, Name = "Elliptical Horizontal", HasHeight = true, HasWidth = true, HasLength = true },
                new Geometry() { Id = (Int16)GeometryEnum.CylinderVertical, Name = "Cylinder Vertical", HasHeight = true, HasWidth = true },
                new Geometry() { Id = (Int16)GeometryEnum.CylinderHorizontal, Name = "Cylinder Horizontal", HasHeight = true, HasLength = true }
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
