namespace KarmicEnergy.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AWS : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TankModels", "WaterVolumeCapacity", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Units", "Symbol", c => c.String(nullable: false, maxLength: 8));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Units", "Symbol", c => c.String(nullable: false, maxLength: 4));
            AlterColumn("dbo.TankModels", "WaterVolumeCapacity", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
