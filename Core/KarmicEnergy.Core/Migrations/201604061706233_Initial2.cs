namespace KarmicEnergy.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SensorItems", "SensorId", c => c.Guid(nullable: false));
            CreateIndex("dbo.SensorItems", "SensorId");
            AddForeignKey("dbo.SensorItems", "SensorId", "dbo.Sensors", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SensorItems", "SensorId", "dbo.Sensors");
            DropIndex("dbo.SensorItems", new[] { "SensorId" });
            DropColumn("dbo.SensorItems", "SensorId");
        }
    }
}
