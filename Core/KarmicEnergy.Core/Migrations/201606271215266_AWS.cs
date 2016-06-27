namespace KarmicEnergy.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AWS : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Ponds", "TankModelId", "dbo.TankModels");
            DropIndex("dbo.Ponds", new[] { "TankModelId" });
            DropColumn("dbo.Ponds", "Height");
            DropColumn("dbo.Ponds", "Width");
            DropColumn("dbo.Ponds", "Length");
            DropColumn("dbo.Ponds", "FaceLength");
            DropColumn("dbo.Ponds", "BottomWidth");
            DropColumn("dbo.Ponds", "Radius");
            DropColumn("dbo.Ponds", "Diagonal");
            DropColumn("dbo.Ponds", "Dimension1");
            DropColumn("dbo.Ponds", "Dimension2");
            DropColumn("dbo.Ponds", "Dimension3");
            DropColumn("dbo.Ponds", "MinimumDistance");
            DropColumn("dbo.Ponds", "MaximumDistance");
            DropColumn("dbo.Ponds", "TankModelId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Ponds", "TankModelId", c => c.Int(nullable: false));
            AddColumn("dbo.Ponds", "MaximumDistance", c => c.Int());
            AddColumn("dbo.Ponds", "MinimumDistance", c => c.Int());
            AddColumn("dbo.Ponds", "Dimension3", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.Ponds", "Dimension2", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.Ponds", "Dimension1", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.Ponds", "Diagonal", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.Ponds", "Radius", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.Ponds", "BottomWidth", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.Ponds", "FaceLength", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.Ponds", "Length", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.Ponds", "Width", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.Ponds", "Height", c => c.Decimal(precision: 18, scale: 2));
            CreateIndex("dbo.Ponds", "TankModelId");
            AddForeignKey("dbo.Ponds", "TankModelId", "dbo.TankModels", "Id", cascadeDelete: true);
        }
    }
}
