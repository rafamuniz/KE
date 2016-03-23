namespace KarmicEnergy.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tanks", "WaterVolumeCapacity", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Tanks", "Latitude", c => c.String(maxLength: 64));
            AddColumn("dbo.Tanks", "Longitude", c => c.String(maxLength: 64));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tanks", "Longitude");
            DropColumn("dbo.Tanks", "Latitude");
            DropColumn("dbo.Tanks", "WaterVolumeCapacity");
        }
    }
}
