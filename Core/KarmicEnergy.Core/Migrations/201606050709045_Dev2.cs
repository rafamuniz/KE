namespace KarmicEnergy.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Dev2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SensorItemEvents", "SensorItemEventId", c => c.Guid(nullable: false));
            AddColumn("dbo.SensorItemEvents", "CheckedAlarm", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SensorItemEvents", "CheckedAlarm");
            DropColumn("dbo.SensorItemEvents", "SensorItemEventId");
        }
    }
}
