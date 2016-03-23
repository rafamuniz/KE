namespace KarmicEnergy.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Alarms",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        SensorAlarmId = c.Guid(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SensorAlarms", t => t.SensorAlarmId, cascadeDelete: true)
                .Index(t => t.SensorAlarmId);
            
            CreateTable(
                "dbo.SensorAlarms",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 128),
                        Status = c.String(nullable: false, maxLength: 1, fixedLength: true, unicode: false),
                        SensorId = c.Guid(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sensors", t => t.SensorId, cascadeDelete: true)
                .Index(t => t.SensorId);
            
            CreateTable(
                "dbo.SensorTypes",
                c => new
                    {
                        Id = c.Short(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 128),
                        Status = c.String(nullable: false, maxLength: 1, fixedLength: true, unicode: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SensorData",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        SensorId = c.Guid(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sensors", t => t.SensorId, cascadeDelete: true)
                .Index(t => t.SensorId);
            
            AddColumn("dbo.Sensors", "SensorTypeId", c => c.Short(nullable: false));
            CreateIndex("dbo.Sensors", "SensorTypeId");
            AddForeignKey("dbo.Sensors", "SensorTypeId", "dbo.SensorTypes", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SensorData", "SensorId", "dbo.Sensors");
            DropForeignKey("dbo.Alarms", "SensorAlarmId", "dbo.SensorAlarms");
            DropForeignKey("dbo.SensorAlarms", "SensorId", "dbo.Sensors");
            DropForeignKey("dbo.Sensors", "SensorTypeId", "dbo.SensorTypes");
            DropIndex("dbo.SensorData", new[] { "SensorId" });
            DropIndex("dbo.Sensors", new[] { "SensorTypeId" });
            DropIndex("dbo.SensorAlarms", new[] { "SensorId" });
            DropIndex("dbo.Alarms", new[] { "SensorAlarmId" });
            DropColumn("dbo.Sensors", "SensorTypeId");
            DropTable("dbo.SensorData");
            DropTable("dbo.SensorTypes");
            DropTable("dbo.SensorAlarms");
            DropTable("dbo.Alarms");
        }
    }
}
