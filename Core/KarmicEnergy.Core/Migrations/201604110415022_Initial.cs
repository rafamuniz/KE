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
                        TriggerId = c.Guid(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Triggers", t => t.TriggerId, cascadeDelete: true)
                .Index(t => t.TriggerId);
            
            CreateTable(
                "dbo.Triggers",
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
                        SensorId = c.Guid(nullable: false),
                        ItemId = c.Int(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Items", t => t.ItemId, cascadeDelete: true)
                .ForeignKey("dbo.Sensors", t => t.SensorId, cascadeDelete: true)
                .Index(t => t.SensorId)
                .Index(t => t.ItemId);
            
            CreateTable(
                "dbo.Items",
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
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 128),
                        Reference = c.String(nullable: false, maxLength: 8),
                        Status = c.String(nullable: false, maxLength: 1, fixedLength: true, unicode: false),
                        SpotGPS = c.String(maxLength: 128),
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
                        Reference = c.String(nullable: false, maxLength: 8),
                        Status = c.String(nullable: false, maxLength: 1, fixedLength: true, unicode: false),
                        Latitude = c.String(maxLength: 64),
                        Longitude = c.String(maxLength: 64),
                        Height = c.Decimal(precision: 18, scale: 2),
                        Width = c.Decimal(precision: 18, scale: 2),
                        Length = c.Decimal(precision: 18, scale: 2),
                        Dimension1 = c.Decimal(precision: 18, scale: 2),
                        Dimension2 = c.Decimal(precision: 18, scale: 2),
                        Dimension3 = c.Decimal(precision: 18, scale: 2),
                        MinimumDistance = c.Int(),
                        MaximumDistance = c.Int(),
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
                        Height = c.Decimal(precision: 18, scale: 2),
                        Width = c.Decimal(precision: 18, scale: 2),
                        Length = c.Decimal(precision: 18, scale: 2),
                        FaceLength = c.Decimal(precision: 18, scale: 2),
                        BottomWidth = c.Decimal(precision: 18, scale: 2),
                        DimensionValue1 = c.Decimal(precision: 18, scale: 2),
                        DimensionValue2 = c.Decimal(precision: 18, scale: 2),
                        DimensionValue3 = c.Decimal(precision: 18, scale: 2),
                        WaterVolumeCapacity = c.Decimal(nullable: false, precision: 18, scale: 2),
                        GeometryId = c.Short(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Geometries", t => t.GeometryId, cascadeDelete: true)
                .Index(t => t.GeometryId);
            
            CreateTable(
                "dbo.Geometries",
                c => new
                    {
                        Id = c.Short(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 128),
                        HasHeight = c.Boolean(nullable: false),
                        HasWidth = c.Boolean(nullable: false),
                        HasLength = c.Boolean(nullable: false),
                        HasFaceLength = c.Boolean(nullable: false),
                        HasBottomWidth = c.Boolean(nullable: false),
                        HasDimension1 = c.Boolean(nullable: false),
                        DimensionTitle1 = c.String(maxLength: 32),
                        HasDimension2 = c.Boolean(nullable: false),
                        DimensionTitle2 = c.String(maxLength: 32),
                        HasDimension3 = c.Boolean(nullable: false),
                        DimensionTitle3 = c.String(maxLength: 32),
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
                "dbo.CustomerSettings",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Key = c.String(nullable: false, maxLength: 128),
                        Value = c.String(nullable: false),
                        CustomerId = c.Guid(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId);
            
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
                "dbo.CustomerUserSettings",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Key = c.String(nullable: false, maxLength: 128),
                        Value = c.String(nullable: false),
                        CustomerUserId = c.Guid(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CustomerUsers", t => t.CustomerUserId, cascadeDelete: true)
                .Index(t => t.CustomerUserId);
            
            CreateTable(
                "dbo.SensorItemEvents",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Value = c.String(maxLength: 256),
                        EventDate = c.DateTime(nullable: false),
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
            DropForeignKey("dbo.CustomerUserSettings", "CustomerUserId", "dbo.CustomerUsers");
            DropForeignKey("dbo.CustomerUsers", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.CustomerSettings", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Alarms", "TriggerId", "dbo.Triggers");
            DropForeignKey("dbo.Triggers", "SeverityId", "dbo.Severities");
            DropForeignKey("dbo.Triggers", "SensorItemId", "dbo.SensorItems");
            DropForeignKey("dbo.Tanks", "TankModelId", "dbo.TankModels");
            DropForeignKey("dbo.TankModels", "GeometryId", "dbo.Geometries");
            DropForeignKey("dbo.Tanks", "SiteId", "dbo.Sites");
            DropForeignKey("dbo.Sites", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Sensors", "TankId", "dbo.Tanks");
            DropForeignKey("dbo.Sensors", "SensorTypeId", "dbo.SensorTypes");
            DropForeignKey("dbo.SensorItems", "SensorId", "dbo.Sensors");
            DropForeignKey("dbo.SensorItems", "ItemId", "dbo.Items");
            DropIndex("dbo.SensorItemEvents", new[] { "SensorItemId" });
            DropIndex("dbo.CustomerUserSettings", new[] { "CustomerUserId" });
            DropIndex("dbo.CustomerUsers", new[] { "CustomerId" });
            DropIndex("dbo.CustomerSettings", new[] { "CustomerId" });
            DropIndex("dbo.TankModels", new[] { "GeometryId" });
            DropIndex("dbo.Sites", new[] { "CustomerId" });
            DropIndex("dbo.Tanks", new[] { "TankModelId" });
            DropIndex("dbo.Tanks", new[] { "SiteId" });
            DropIndex("dbo.Sensors", new[] { "TankId" });
            DropIndex("dbo.Sensors", new[] { "SensorTypeId" });
            DropIndex("dbo.SensorItems", new[] { "ItemId" });
            DropIndex("dbo.SensorItems", new[] { "SensorId" });
            DropIndex("dbo.Triggers", new[] { "SeverityId" });
            DropIndex("dbo.Triggers", new[] { "SensorItemId" });
            DropIndex("dbo.Alarms", new[] { "TriggerId" });
            DropTable("dbo.SensorItemEvents");
            DropTable("dbo.CustomerUserSettings");
            DropTable("dbo.CustomerUsers");
            DropTable("dbo.CustomerSettings");
            DropTable("dbo.Countries");
            DropTable("dbo.Severities");
            DropTable("dbo.Geometries");
            DropTable("dbo.TankModels");
            DropTable("dbo.Customers");
            DropTable("dbo.Sites");
            DropTable("dbo.Tanks");
            DropTable("dbo.SensorTypes");
            DropTable("dbo.Sensors");
            DropTable("dbo.Items");
            DropTable("dbo.SensorItems");
            DropTable("dbo.Triggers");
            DropTable("dbo.Alarms");
        }
    }
}
