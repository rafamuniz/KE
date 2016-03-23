namespace KarmicEnergy.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TankModels", "Height", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.TankModels", "Width", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.TankModels", "Length", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.TankModels", "FaceLength", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.TankModels", "BottomWidth", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TankModels", "BottomWidth");
            DropColumn("dbo.TankModels", "FaceLength");
            DropColumn("dbo.TankModels", "Length");
            DropColumn("dbo.TankModels", "Width");
            DropColumn("dbo.TankModels", "Height");
        }
    }
}
