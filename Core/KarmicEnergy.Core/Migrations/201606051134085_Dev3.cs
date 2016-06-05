namespace KarmicEnergy.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Dev3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SensorItemEvents", "SensorItemEventId", c => c.Guid());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SensorItemEvents", "SensorItemEventId", c => c.Guid(nullable: false));
        }
    }
}
