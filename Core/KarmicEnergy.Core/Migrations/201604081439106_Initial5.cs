namespace KarmicEnergy.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Geometries", "DimensionTitle1", c => c.String(maxLength: 32));
            AddColumn("dbo.Geometries", "DimensionTitle2", c => c.String(maxLength: 32));
            AddColumn("dbo.Geometries", "DimensionTitle3", c => c.String(maxLength: 32));
            DropColumn("dbo.Geometries", "Dim1Description");
            DropColumn("dbo.Geometries", "Dim2Description");
            DropColumn("dbo.Geometries", "Dim3Description");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Geometries", "Dim3Description", c => c.String(maxLength: 32));
            AddColumn("dbo.Geometries", "Dim2Description", c => c.String(maxLength: 32));
            AddColumn("dbo.Geometries", "Dim1Description", c => c.String(maxLength: 32));
            DropColumn("dbo.Geometries", "DimensionTitle3");
            DropColumn("dbo.Geometries", "DimensionTitle2");
            DropColumn("dbo.Geometries", "DimensionTitle1");
        }
    }
}
