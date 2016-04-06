namespace KarmicEnergy.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial3 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.SensorItemAlarms", newName: "Triggers");
            RenameColumn(table: "dbo.Alarms", name: "SensorItemAlarmId", newName: "TriggerId");
            RenameIndex(table: "dbo.Alarms", name: "IX_SensorItemAlarmId", newName: "IX_TriggerId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Alarms", name: "IX_TriggerId", newName: "IX_SensorItemAlarmId");
            RenameColumn(table: "dbo.Alarms", name: "TriggerId", newName: "SensorItemAlarmId");
            RenameTable(name: "dbo.Triggers", newName: "SensorItemAlarms");
        }
    }
}
