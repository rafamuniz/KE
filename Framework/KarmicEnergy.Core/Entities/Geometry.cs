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

        [Column("HasRadius", TypeName = "BIT")]
        public Boolean HasRadius { get; set; } = false;

        [Column("HasDiagonal", TypeName = "BIT")]
        public Boolean HasDiagonal { get; set; } = false;

        [Column("HasDimension1", TypeName = "BIT")]
        public Boolean HasDimension1 { get; set; } = false;

        [Column("DimensionTitle1", TypeName = "NVARCHAR")]
        [StringLength(32)]
        public String DimensionTitle1 { get; set; }

        [Column("HasDimension2", TypeName = "BIT")]
        public Boolean HasDimension2 { get; set; } = false;

        [Column("DimensionTitle2", TypeName = "NVARCHAR")]
        [StringLength(32)]
        public String DimensionTitle2 { get; set; }

        [Column("HasDimension3", TypeName = "BIT")]
        public Boolean HasDimension3 { get; set; } = false;

        [Column("DimensionTitle3", TypeName = "NVARCHAR")]
        [StringLength(32)]
        public String DimensionTitle3 { get; set; }

        [Column("FormulaVolume", TypeName = "NVARCHAR")]
        [StringLength(128)]
        public String FormulaVolume { get; set; } = String.Empty;

        #endregion Property

        #region Functions

        public static List<Geometry> Load()
        {
            List<Geometry> entities = new List<Geometry>()
            {
                new Geometry() { Id = (Int16)GeometryEnum.Cube, Name = "Cube", HasWidth = true },
                new Geometry() { Id = (Int16)GeometryEnum.StadiumVertical, Name = "Stadium Vertical", HasHeight = true, HasWidth = true, HasFaceLength = true },
                new Geometry() { Id = (Int16)GeometryEnum.StadiumHorizontal, Name = "Stadium Horizontal", HasHeight = true, HasLength = true, HasBottomWidth = true },
                new Geometry() { Id = (Int16)GeometryEnum.EllipticalHorizontal, Name = "Elliptical Horizontal", HasHeight = true, HasWidth = true, HasLength = true },
                new Geometry() { Id = (Int16)GeometryEnum.CylinderVertical, Name = "Cylinder Vertical", HasHeight = true, HasWidth = true },
                new Geometry() { Id = (Int16)GeometryEnum.CylinderHorizontal, Name = "Cylinder Horizontal", HasHeight = true, HasLength = true },
                new Geometry() { Id = (Int16)GeometryEnum.Rectangle, Name = "Rectangle", HasHeight = true, HasWidth = true, HasLength = true }
            };

            return entities;
        }
        public void Update(Geometry entity)
        {
            this.Name = entity.Name;

            this.FormulaVolume = entity.FormulaVolume;

            this.HasBottomWidth = entity.HasBottomWidth;
            this.HasDiagonal = entity.HasDiagonal;
            this.HasDimension1 = entity.HasDimension1;
            this.HasDimension2 = entity.HasDimension2;
            this.HasDimension3 = entity.HasDimension3;
            this.HasFaceLength = entity.HasFaceLength;
            this.HasHeight = entity.HasHeight;
            this.HasLength = entity.HasLength;
            this.HasRadius = entity.HasRadius;
            this.HasWidth = entity.HasWidth;
            
            this.DimensionTitle1 = entity.DimensionTitle1;
            this.DimensionTitle2 = entity.DimensionTitle2;
            this.DimensionTitle3 = entity.DimensionTitle3;

            this.CreatedDate = entity.CreatedDate;
            this.LastModifiedDate = entity.LastModifiedDate;
            this.DeletedDate = entity.DeletedDate;
        }
        #endregion Functions
    }

    public enum GeometryEnum : short
    {
        [Description("Cube")]
        Cube = 1,

        [Description("Stadium Vertical")]
        StadiumVertical = 2,

        [Description("Stadium Horizontal")]
        StadiumHorizontal = 3,

        [Description("Elliptical Horizontal")]
        EllipticalHorizontal = 4,

        [Description("Cylinder Vertical")]
        CylinderVertical = 5,

        [Description("Cylinder Horizontal")]
        CylinderHorizontal = 6,

        [Description("Rectangle")]
        Rectangle = 7
    }
}
