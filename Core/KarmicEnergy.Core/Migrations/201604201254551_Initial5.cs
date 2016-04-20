namespace KarmicEnergy.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Alarms", "Value", c => c.String(maxLength: 256));
            AddColumn("dbo.Alarms", "StartDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Alarms", "EndDate", c => c.DateTime());
            AddColumn("dbo.Alarms", "SensorItemEventId", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Alarms", "SensorItemEventId");
            DropColumn("dbo.Alarms", "EndDate");
            DropColumn("dbo.Alarms", "StartDate");
            DropColumn("dbo.Alarms", "Value");
        }
    }
}
