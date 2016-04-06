namespace KarmicEnergy.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TankModels", "Height", c => c.Boolean(nullable: false));
            AlterColumn("dbo.TankModels", "Width", c => c.Boolean(nullable: false));
            AlterColumn("dbo.TankModels", "Length", c => c.Boolean(nullable: false));
            AlterColumn("dbo.TankModels", "FaceLength", c => c.Boolean(nullable: false));
            AlterColumn("dbo.TankModels", "BottomWidth", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TankModels", "BottomWidth", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.TankModels", "FaceLength", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.TankModels", "Length", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.TankModels", "Width", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.TankModels", "Height", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
