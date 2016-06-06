namespace KarmicEnergy.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Dev : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Email = c.String(maxLength: 256),
                        PhoneNumberCountryCode = c.String(maxLength: 3),
                        PhoneNumber = c.String(maxLength: 16),
                        MobileNumberCountryCode = c.String(maxLength: 3),
                        MobileNumber = c.String(maxLength: 16),
                        AddressLine1 = c.String(maxLength: 256),
                        AddressLine2 = c.String(maxLength: 256),
                        City = c.String(maxLength: 128),
                        State = c.String(maxLength: 64),
                        Country = c.String(maxLength: 64),
                        ZipCode = c.String(maxLength: 16),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreatedDate = c.DateTime(nullable: false),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AlarmHistories",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Value = c.String(maxLength: 256),
                        CalculatedValue = c.String(maxLength: 256),
                        AckUserId = c.Guid(nullable: false),
                        AckDate = c.DateTime(nullable: false),
                        AlarmId = c.Guid(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreatedDate = c.DateTime(nullable: false),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Alarms",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Value = c.String(maxLength: 256),
                        CalculatedValue = c.String(maxLength: 256),
                        LastAckUserId = c.Guid(),
                        LastAckDate = c.DateTime(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(),
                        SensorItemEventId = c.Guid(nullable: false),
                        TriggerId = c.Guid(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreatedDate = c.DateTime(nullable: false),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Triggers", t => t.TriggerId, cascadeDelete: false)
                .Index(t => t.TriggerId);
            
            CreateTable(
                "dbo.Triggers",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        MinValue = c.String(maxLength: 256),
                        MaxValue = c.String(maxLength: 256),
                        Status = c.String(nullable: false, maxLength: 1, fixedLength: true, unicode: false),
                        SensorItemId = c.Guid(nullable: false),
                        SeverityId = c.Short(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreatedDate = c.DateTime(nullable: false),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SensorItems", t => t.SensorItemId, cascadeDelete: false)
                .ForeignKey("dbo.Severities", t => t.SeverityId, cascadeDelete: false)
                .Index(t => t.SensorItemId)
                .Index(t => t.SeverityId);
            
            CreateTable(
                "dbo.TriggerContacts",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Status = c.String(nullable: false, maxLength: 1, fixedLength: true, unicode: false),
                        TriggerId = c.Guid(nullable: false),
                        ContactId = c.Guid(),
                        UserId = c.Guid(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreatedDate = c.DateTime(nullable: false),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Triggers", t => t.TriggerId, cascadeDelete: false)
                .Index(t => t.TriggerId);
            
            CreateTable(
                "dbo.SensorItems",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Status = c.String(nullable: false, maxLength: 1, fixedLength: true, unicode: false),
                        SensorId = c.Guid(nullable: false),
                        ItemId = c.Int(nullable: false),
                        UnitId = c.Short(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreatedDate = c.DateTime(nullable: false),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Items", t => t.ItemId, cascadeDelete: false)
                .ForeignKey("dbo.Sensors", t => t.SensorId, cascadeDelete: false)
                .ForeignKey("dbo.Units", t => t.UnitId, cascadeDelete: false)
                .Index(t => t.SensorId)
                .Index(t => t.ItemId)
                .Index(t => t.UnitId);
            
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(nullable: false, maxLength: 5),
                        Name = c.String(nullable: false, maxLength: 128),
                        Status = c.String(nullable: false, maxLength: 1, fixedLength: true, unicode: false),
                        UnitTypeId = c.Short(nullable: false),
                        SensorTypeId = c.Short(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreatedDate = c.DateTime(nullable: false),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SensorTypes", t => t.SensorTypeId, cascadeDelete: false)
                .ForeignKey("dbo.UnitTypes", t => t.UnitTypeId, cascadeDelete: false)
                .Index(t => t.UnitTypeId)
                .Index(t => t.SensorTypeId);
            
            CreateTable(
                "dbo.SensorTypes",
                c => new
                    {
                        Id = c.Short(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 128),
                        Status = c.String(nullable: false, maxLength: 1, fixedLength: true, unicode: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreatedDate = c.DateTime(nullable: false),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UnitTypes",
                c => new
                    {
                        Id = c.Short(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 128),
                        Status = c.String(nullable: false, maxLength: 1, fixedLength: true, unicode: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreatedDate = c.DateTime(nullable: false),
                        DeletedDate = c.DateTime(),
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
                        TankId = c.Guid(),
                        SiteId = c.Guid(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreatedDate = c.DateTime(nullable: false),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SensorTypes", t => t.SensorTypeId, cascadeDelete: false)
                .ForeignKey("dbo.Sites", t => t.SiteId)
                .ForeignKey("dbo.Tanks", t => t.TankId)
                .Index(t => t.SensorTypeId)
                .Index(t => t.TankId)
                .Index(t => t.SiteId);
            
            CreateTable(
                "dbo.Sites",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 128),
                        IPAddress = c.String(nullable: false, maxLength: 64),
                        Status = c.String(nullable: false, maxLength: 1, fixedLength: true, unicode: false),
                        Latitude = c.String(maxLength: 64),
                        Longitude = c.String(maxLength: 64),
                        CustomerId = c.Guid(nullable: false),
                        AddressId = c.Guid(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreatedDate = c.DateTime(nullable: false),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Addresses", t => t.AddressId, cascadeDelete: false)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: false)
                .Index(t => t.CustomerId)
                .Index(t => t.AddressId);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        AddressId = c.Guid(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreatedDate = c.DateTime(nullable: false),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Addresses", t => t.AddressId, cascadeDelete: false)
                .Index(t => t.AddressId);
            
            CreateTable(
                "dbo.CustomerSettings",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Key = c.String(nullable: false, maxLength: 128),
                        Value = c.String(nullable: false),
                        CustomerId = c.Guid(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreatedDate = c.DateTime(nullable: false),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: false)
                .Index(t => t.CustomerId);
            
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
                        FaceLength = c.Decimal(precision: 18, scale: 2),
                        BottomWidth = c.Decimal(precision: 18, scale: 2),
                        Radius = c.Decimal(precision: 18, scale: 2),
                        Diagonal = c.Decimal(precision: 18, scale: 2),
                        Dimension1 = c.Decimal(precision: 18, scale: 2),
                        Dimension2 = c.Decimal(precision: 18, scale: 2),
                        Dimension3 = c.Decimal(precision: 18, scale: 2),
                        MinimumDistance = c.Int(),
                        MaximumDistance = c.Int(),
                        SiteId = c.Guid(nullable: false),
                        TankModelId = c.Int(nullable: false),
                        StickConversionId = c.Int(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreatedDate = c.DateTime(nullable: false),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sites", t => t.SiteId, cascadeDelete: false)
                .ForeignKey("dbo.StickConversions", t => t.StickConversionId)
                .ForeignKey("dbo.TankModels", t => t.TankModelId, cascadeDelete: false)
                .Index(t => t.SiteId)
                .Index(t => t.TankModelId)
                .Index(t => t.StickConversionId);
            
            CreateTable(
                "dbo.StickConversions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 128),
                        Status = c.String(nullable: false, maxLength: 1, fixedLength: true, unicode: false),
                        FromUnitId = c.Short(nullable: false),
                        ToUnitId = c.Short(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreatedDate = c.DateTime(nullable: false),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Units", t => t.FromUnitId, cascadeDelete: false)
                .ForeignKey("dbo.Units", t => t.ToUnitId, cascadeDelete: false)
                .Index(t => t.FromUnitId)
                .Index(t => t.ToUnitId);
            
            CreateTable(
                "dbo.Units",
                c => new
                    {
                        Id = c.Short(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 128),
                        NamePlural = c.String(nullable: false, maxLength: 128),
                        Symbol = c.String(nullable: false, maxLength: 8),
                        Status = c.String(nullable: false, maxLength: 1, fixedLength: true, unicode: false),
                        UnitTypeId = c.Short(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreatedDate = c.DateTime(nullable: false),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UnitTypes", t => t.UnitTypeId, cascadeDelete: false)
                .Index(t => t.UnitTypeId);
            
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
                        Radius = c.Decimal(precision: 18, scale: 2),
                        Diagonal = c.Decimal(precision: 18, scale: 2),
                        DimensionValue1 = c.Decimal(precision: 18, scale: 2),
                        DimensionValue2 = c.Decimal(precision: 18, scale: 2),
                        DimensionValue3 = c.Decimal(precision: 18, scale: 2),
                        WaterVolumeCapacity = c.Double(),
                        GeometryId = c.Short(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreatedDate = c.DateTime(nullable: false),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Geometries", t => t.GeometryId, cascadeDelete: false)
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
                        HasRadius = c.Boolean(nullable: false),
                        HasDiagonal = c.Boolean(nullable: false),
                        HasDimension1 = c.Boolean(nullable: false),
                        DimensionTitle1 = c.String(maxLength: 32),
                        HasDimension2 = c.Boolean(nullable: false),
                        DimensionTitle2 = c.String(maxLength: 32),
                        HasDimension3 = c.Boolean(nullable: false),
                        DimensionTitle3 = c.String(maxLength: 32),
                        FormulaVolume = c.String(maxLength: 128),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreatedDate = c.DateTime(nullable: false),
                        DeletedDate = c.DateTime(),
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
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 128),
                        CountryId = c.Short(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreatedDate = c.DateTime(nullable: false),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Countries", t => t.CountryId, cascadeDelete: false)
                .Index(t => t.CountryId);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        Id = c.Short(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 128),
                        IconFilename = c.String(nullable: false, maxLength: 128),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreatedDate = c.DateTime(nullable: false),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Contacts",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 128),
                        CustomerId = c.Guid(nullable: false),
                        AddressId = c.Guid(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreatedDate = c.DateTime(nullable: false),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Addresses", t => t.AddressId, cascadeDelete: false)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: false)
                .Index(t => t.CustomerId)
                .Index(t => t.AddressId);
            
            CreateTable(
                "dbo.CustomerUsers",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CustomerId = c.Guid(nullable: false),
                        AddressId = c.Guid(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreatedDate = c.DateTime(nullable: false),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Addresses", t => t.AddressId, cascadeDelete: false)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: false)
                .Index(t => t.CustomerId)
                .Index(t => t.AddressId);
            
            CreateTable(
                "dbo.CustomerUserSettings",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Key = c.String(nullable: false, maxLength: 128),
                        Value = c.String(nullable: false),
                        CustomerUserId = c.Guid(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreatedDate = c.DateTime(nullable: false),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CustomerUsers", t => t.CustomerUserId, cascadeDelete: false)
                .Index(t => t.CustomerUserId);
            
            CreateTable(
                "dbo.CustomerUserSites",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        CustomerUserId = c.Guid(nullable: false),
                        SiteId = c.Guid(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreatedDate = c.DateTime(nullable: false),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sites", t => t.SiteId, cascadeDelete: false)
                .ForeignKey("dbo.CustomerUsers", t => t.CustomerUserId, cascadeDelete: false)
                .Index(t => t.CustomerUserId)
                .Index(t => t.SiteId);
            
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        SiteId = c.Guid(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreatedDate = c.DateTime(nullable: false),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sites", t => t.SiteId, cascadeDelete: false)
                .Index(t => t.SiteId);
            
            CreateTable(
                "dbo.SensorGroups",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Weight = c.Int(nullable: false),
                        SensorId = c.Guid(nullable: false),
                        GroupId = c.Guid(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreatedDate = c.DateTime(nullable: false),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Groups", t => t.GroupId, cascadeDelete: false)
                .ForeignKey("dbo.Sensors", t => t.SensorId, cascadeDelete: false)
                .Index(t => t.SensorId)
                .Index(t => t.GroupId);
            
            CreateTable(
                "dbo.Logs",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Type = c.String(maxLength: 16),
                        Message = c.String(maxLength: 4000),
                        CustomerId = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreatedDate = c.DateTime(nullable: false),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SensorItemEvents",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Value = c.String(maxLength: 256),
                        CalculatedValue = c.String(maxLength: 256),
                        EventDate = c.DateTime(nullable: false),
                        SensorItemId = c.Guid(nullable: false),
                        SensorItemEventId = c.Guid(),
                        CheckedAlarm = c.Boolean(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreatedDate = c.DateTime(nullable: false),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SensorItems", t => t.SensorItemId, cascadeDelete: false)
                .Index(t => t.SensorItemId);
            
            CreateTable(
                "dbo.StickConversionValues",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ToValue = c.String(nullable: false, maxLength: 128),
                        FromValue = c.String(nullable: false, maxLength: 128),
                        StickConversionId = c.Int(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreatedDate = c.DateTime(nullable: false),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.StickConversions", t => t.StickConversionId, cascadeDelete: false)
                .Index(t => t.StickConversionId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        AddressId = c.Guid(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreatedDate = c.DateTime(nullable: false),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Addresses", t => t.AddressId, cascadeDelete: false)
                .Index(t => t.AddressId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "AddressId", "dbo.Addresses");
            DropForeignKey("dbo.StickConversionValues", "StickConversionId", "dbo.StickConversions");
            DropForeignKey("dbo.SensorItemEvents", "SensorItemId", "dbo.SensorItems");
            DropForeignKey("dbo.Groups", "SiteId", "dbo.Sites");
            DropForeignKey("dbo.SensorGroups", "SensorId", "dbo.Sensors");
            DropForeignKey("dbo.SensorGroups", "GroupId", "dbo.Groups");
            DropForeignKey("dbo.CustomerUserSites", "CustomerUserId", "dbo.CustomerUsers");
            DropForeignKey("dbo.CustomerUserSites", "SiteId", "dbo.Sites");
            DropForeignKey("dbo.CustomerUserSettings", "CustomerUserId", "dbo.CustomerUsers");
            DropForeignKey("dbo.CustomerUsers", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.CustomerUsers", "AddressId", "dbo.Addresses");
            DropForeignKey("dbo.Contacts", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Contacts", "AddressId", "dbo.Addresses");
            DropForeignKey("dbo.Cities", "CountryId", "dbo.Countries");
            DropForeignKey("dbo.Alarms", "TriggerId", "dbo.Triggers");
            DropForeignKey("dbo.Triggers", "SeverityId", "dbo.Severities");
            DropForeignKey("dbo.Triggers", "SensorItemId", "dbo.SensorItems");
            DropForeignKey("dbo.SensorItems", "UnitId", "dbo.Units");
            DropForeignKey("dbo.Tanks", "TankModelId", "dbo.TankModels");
            DropForeignKey("dbo.TankModels", "GeometryId", "dbo.Geometries");
            DropForeignKey("dbo.Tanks", "StickConversionId", "dbo.StickConversions");
            DropForeignKey("dbo.StickConversions", "ToUnitId", "dbo.Units");
            DropForeignKey("dbo.StickConversions", "FromUnitId", "dbo.Units");
            DropForeignKey("dbo.Units", "UnitTypeId", "dbo.UnitTypes");
            DropForeignKey("dbo.Tanks", "SiteId", "dbo.Sites");
            DropForeignKey("dbo.Sensors", "TankId", "dbo.Tanks");
            DropForeignKey("dbo.Sensors", "SiteId", "dbo.Sites");
            DropForeignKey("dbo.Sites", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.CustomerSettings", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Customers", "AddressId", "dbo.Addresses");
            DropForeignKey("dbo.Sites", "AddressId", "dbo.Addresses");
            DropForeignKey("dbo.Sensors", "SensorTypeId", "dbo.SensorTypes");
            DropForeignKey("dbo.SensorItems", "SensorId", "dbo.Sensors");
            DropForeignKey("dbo.SensorItems", "ItemId", "dbo.Items");
            DropForeignKey("dbo.Items", "UnitTypeId", "dbo.UnitTypes");
            DropForeignKey("dbo.Items", "SensorTypeId", "dbo.SensorTypes");
            DropForeignKey("dbo.TriggerContacts", "TriggerId", "dbo.Triggers");
            DropIndex("dbo.Users", new[] { "AddressId" });
            DropIndex("dbo.StickConversionValues", new[] { "StickConversionId" });
            DropIndex("dbo.SensorItemEvents", new[] { "SensorItemId" });
            DropIndex("dbo.SensorGroups", new[] { "GroupId" });
            DropIndex("dbo.SensorGroups", new[] { "SensorId" });
            DropIndex("dbo.Groups", new[] { "SiteId" });
            DropIndex("dbo.CustomerUserSites", new[] { "SiteId" });
            DropIndex("dbo.CustomerUserSites", new[] { "CustomerUserId" });
            DropIndex("dbo.CustomerUserSettings", new[] { "CustomerUserId" });
            DropIndex("dbo.CustomerUsers", new[] { "AddressId" });
            DropIndex("dbo.CustomerUsers", new[] { "CustomerId" });
            DropIndex("dbo.Contacts", new[] { "AddressId" });
            DropIndex("dbo.Contacts", new[] { "CustomerId" });
            DropIndex("dbo.Cities", new[] { "CountryId" });
            DropIndex("dbo.TankModels", new[] { "GeometryId" });
            DropIndex("dbo.Units", new[] { "UnitTypeId" });
            DropIndex("dbo.StickConversions", new[] { "ToUnitId" });
            DropIndex("dbo.StickConversions", new[] { "FromUnitId" });
            DropIndex("dbo.Tanks", new[] { "StickConversionId" });
            DropIndex("dbo.Tanks", new[] { "TankModelId" });
            DropIndex("dbo.Tanks", new[] { "SiteId" });
            DropIndex("dbo.CustomerSettings", new[] { "CustomerId" });
            DropIndex("dbo.Customers", new[] { "AddressId" });
            DropIndex("dbo.Sites", new[] { "AddressId" });
            DropIndex("dbo.Sites", new[] { "CustomerId" });
            DropIndex("dbo.Sensors", new[] { "SiteId" });
            DropIndex("dbo.Sensors", new[] { "TankId" });
            DropIndex("dbo.Sensors", new[] { "SensorTypeId" });
            DropIndex("dbo.Items", new[] { "SensorTypeId" });
            DropIndex("dbo.Items", new[] { "UnitTypeId" });
            DropIndex("dbo.SensorItems", new[] { "UnitId" });
            DropIndex("dbo.SensorItems", new[] { "ItemId" });
            DropIndex("dbo.SensorItems", new[] { "SensorId" });
            DropIndex("dbo.TriggerContacts", new[] { "TriggerId" });
            DropIndex("dbo.Triggers", new[] { "SeverityId" });
            DropIndex("dbo.Triggers", new[] { "SensorItemId" });
            DropIndex("dbo.Alarms", new[] { "TriggerId" });
            DropTable("dbo.Users");
            DropTable("dbo.StickConversionValues");
            DropTable("dbo.SensorItemEvents");
            DropTable("dbo.Logs");
            DropTable("dbo.SensorGroups");
            DropTable("dbo.Groups");
            DropTable("dbo.CustomerUserSites");
            DropTable("dbo.CustomerUserSettings");
            DropTable("dbo.CustomerUsers");
            DropTable("dbo.Contacts");
            DropTable("dbo.Countries");
            DropTable("dbo.Cities");
            DropTable("dbo.Severities");
            DropTable("dbo.Geometries");
            DropTable("dbo.TankModels");
            DropTable("dbo.Units");
            DropTable("dbo.StickConversions");
            DropTable("dbo.Tanks");
            DropTable("dbo.CustomerSettings");
            DropTable("dbo.Customers");
            DropTable("dbo.Sites");
            DropTable("dbo.Sensors");
            DropTable("dbo.UnitTypes");
            DropTable("dbo.SensorTypes");
            DropTable("dbo.Items");
            DropTable("dbo.SensorItems");
            DropTable("dbo.TriggerContacts");
            DropTable("dbo.Triggers");
            DropTable("dbo.Alarms");
            DropTable("dbo.AlarmHistories");
            DropTable("dbo.Addresses");
        }
    }
}
