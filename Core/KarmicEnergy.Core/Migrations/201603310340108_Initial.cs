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
                        SensorItemAlarmId = c.Guid(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SensorItemAlarms", t => t.SensorItemAlarmId, cascadeDelete: true)
                .Index(t => t.SensorItemAlarmId);
            
            CreateTable(
                "dbo.SensorItemAlarms",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 128),
                        Email = c.String(nullable: false, maxLength: 256),
                        Status = c.String(nullable: false, maxLength: 1, fixedLength: true, unicode: false),
                        SensorItemId = c.Guid(nullable: false),
                        SeverityId = c.Short(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SensorItems", t => t.SensorItemId, cascadeDelete: true)
                .ForeignKey("dbo.Severities", t => t.SeverityId, cascadeDelete: true)
                .Index(t => t.SensorItemId)
                .Index(t => t.SeverityId);
            
            CreateTable(
                "dbo.SensorItems",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Status = c.String(nullable: false, maxLength: 1, fixedLength: true, unicode: false),
                        SensorId = c.Long(nullable: false),
                        ItemId = c.Int(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Item", t => t.ItemId, cascadeDelete: true)
                .ForeignKey("dbo.Sensors", t => t.SensorId, cascadeDelete: true)
                .Index(t => t.SensorId)
                .Index(t => t.ItemId);
            
            CreateTable(
                "dbo.Item",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(nullable: false, maxLength: 5),
                        Name = c.String(nullable: false, maxLength: 128),
                        Status = c.String(nullable: false, maxLength: 1, fixedLength: true, unicode: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Sensors",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 128),
                        Status = c.String(nullable: false, maxLength: 1, fixedLength: true, unicode: false),
                        SensorTypeId = c.Short(nullable: false),
                        TankId = c.Guid(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SensorTypes", t => t.SensorTypeId, cascadeDelete: true)
                .ForeignKey("dbo.Tanks", t => t.TankId, cascadeDelete: true)
                .Index(t => t.SensorTypeId)
                .Index(t => t.TankId);
            
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
                "dbo.Tanks",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 128),
                        Description = c.String(maxLength: 4000),
                        WaterVolumeCapacity = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Status = c.String(nullable: false, maxLength: 1, fixedLength: true, unicode: false),
                        Latitude = c.String(maxLength: 64),
                        Longitude = c.String(maxLength: 64),
                        SiteId = c.Guid(nullable: false),
                        TankModelId = c.Int(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sites", t => t.SiteId, cascadeDelete: true)
                .ForeignKey("dbo.TankModels", t => t.TankModelId, cascadeDelete: true)
                .Index(t => t.SiteId)
                .Index(t => t.TankModelId);
            
            CreateTable(
                "dbo.Sites",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 128),
                        IPAddress = c.String(nullable: false, maxLength: 64),
                        Status = c.String(nullable: false, maxLength: 1, fixedLength: true, unicode: false),
                        CustomerId = c.Guid(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TankModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 128),
                        ImageFileName = c.String(maxLength: 256),
                        Image = c.Binary(),
                        Status = c.String(nullable: false, maxLength: 1, fixedLength: true, unicode: false),
                        Height = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Width = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Length = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FaceLength = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BottomWidth = c.Decimal(nullable: false, precision: 18, scale: 2),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Severities",
                c => new
                    {
                        Id = c.Short(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 128),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        Id = c.Short(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 128),
                        IconFilename = c.String(nullable: false, maxLength: 128),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CustomerUsers",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CustomerId = c.Guid(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.SensorItemEvents",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Value = c.String(maxLength: 256),
                        SensorItemId = c.Guid(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SensorItems", t => t.SensorItemId, cascadeDelete: true)
                .Index(t => t.SensorItemId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SensorItemEvents", "SensorItemId", "dbo.SensorItems");
            DropForeignKey("dbo.CustomerUsers", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Alarms", "SensorItemAlarmId", "dbo.SensorItemAlarms");
            DropForeignKey("dbo.SensorItemAlarms", "SeverityId", "dbo.Severities");
            DropForeignKey("dbo.SensorItemAlarms", "SensorItemId", "dbo.SensorItems");
            DropForeignKey("dbo.SensorItems", "SensorId", "dbo.Sensors");
            DropForeignKey("dbo.Sensors", "TankId", "dbo.Tanks");
            DropForeignKey("dbo.Tanks", "TankModelId", "dbo.TankModels");
            DropForeignKey("dbo.Tanks", "SiteId", "dbo.Sites");
            DropForeignKey("dbo.Sites", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Sensors", "SensorTypeId", "dbo.SensorTypes");
            DropForeignKey("dbo.SensorItems", "ItemId", "dbo.Item");
            DropIndex("dbo.SensorItemEvents", new[] { "SensorItemId" });
            DropIndex("dbo.CustomerUsers", new[] { "CustomerId" });
            DropIndex("dbo.Sites", new[] { "CustomerId" });
            DropIndex("dbo.Tanks", new[] { "TankModelId" });
            DropIndex("dbo.Tanks", new[] { "SiteId" });
            DropIndex("dbo.Sensors", new[] { "TankId" });
            DropIndex("dbo.Sensors", new[] { "SensorTypeId" });
            DropIndex("dbo.SensorItems", new[] { "ItemId" });
            DropIndex("dbo.SensorItems", new[] { "SensorId" });
            DropIndex("dbo.SensorItemAlarms", new[] { "SeverityId" });
            DropIndex("dbo.SensorItemAlarms", new[] { "SensorItemId" });
            DropIndex("dbo.Alarms", new[] { "SensorItemAlarmId" });
            DropTable("dbo.SensorItemEvents");
            DropTable("dbo.CustomerUsers");
            DropTable("dbo.Countries");
            DropTable("dbo.Severities");
            DropTable("dbo.TankModels");
            DropTable("dbo.Customers");
            DropTable("dbo.Sites");
            DropTable("dbo.Tanks");
            DropTable("dbo.SensorTypes");
            DropTable("dbo.Sensors");
            DropTable("dbo.Item");
            DropTable("dbo.SensorItems");
            DropTable("dbo.SensorItemAlarms");
            DropTable("dbo.Alarms");
        }
    }
}
