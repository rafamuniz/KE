namespace KarmicEnergy.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Dev : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TankModels", "WaterVolumeCapacity", c => c.Double());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TankModels", "WaterVolumeCapacity", c => c.Decimal(precision: 18, scale: 2));
        }
    }
}
