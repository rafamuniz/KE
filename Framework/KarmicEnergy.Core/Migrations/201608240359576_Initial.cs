namespace KarmicEnergy.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ActionTypes",
                c => new
                    {
                        Id = c.Short(nullable: false),
                        Name = c.String(maxLength: 16),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreatedDate = c.DateTime(nullable: false),
                        LastModifiedDate = c.DateTime(nullable: false),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        Id = c.Guid(nullable: false),
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
                        LastModifiedDate = c.DateTime(nullable: false),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AlarmHistories",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Message = c.String(maxLength: 4000),
                        Value = c.String(maxLength: 256),
                        UserId = c.Guid(nullable: false),
                        UserName = c.String(maxLength: 256),
                        AlarmId = c.Guid(nullable: false),
                        ActionTypeId = c.Short(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreatedDate = c.DateTime(nullable: false),
                        LastModifiedDate = c.DateTime(nullable: false),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ActionTypes", t => t.ActionTypeId)
                .Index(t => t.ActionTypeId);
            
            CreateTable(
                "dbo.Alarms",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Value = c.String(maxLength: 256),
                        LastAckUserId = c.Guid(),
                        LastAckUserName = c.String(maxLength: 256),
                        LastAckDate = c.DateTime(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(),
                        SensorItemEventId = c.Guid(nullable: false),
                        TriggerId = c.Guid(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreatedDate = c.DateTime(nullable: false),
                        LastModifiedDate = c.DateTime(nullable: false),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Triggers", t => t.TriggerId)
                .Index(t => t.TriggerId);
            
            CreateTable(
                "dbo.Triggers",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Value = c.String(maxLength: 256),
                        Status = c.String(nullable: false, maxLength: 1, fixedLength: true, unicode: false),
                        SensorItemId = c.Guid(nullable: false),
                        SeverityId = c.Short(nullable: false),
                        OperatorId = c.Short(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreatedDate = c.DateTime(nullable: false),
                        LastModifiedDate = c.DateTime(nullable: false),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Operators", t => t.OperatorId)
                .ForeignKey("dbo.SensorItems", t => t.SensorItemId)
                .ForeignKey("dbo.Severities", t => t.SeverityId)
                .Index(t => t.SensorItemId)
                .Index(t => t.SeverityId)
                .Index(t => t.OperatorId);
            
            CreateTable(
                "dbo.TriggerContacts",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Status = c.String(nullable: false, maxLength: 1, fixedLength: true, unicode: false),
                        TriggerId = c.Guid(nullable: false),
                        ContactId = c.Guid(),
                        UserId = c.Guid(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreatedDate = c.DateTime(nullable: false),
                        LastModifiedDate = c.DateTime(nullable: false),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Triggers", t => t.TriggerId)
                .Index(t => t.TriggerId);
            
            CreateTable(
                "dbo.Operators",
                c => new
                    {
                        Id = c.Short(nullable: false, identity: true),
                        Name = c.String(maxLength: 32),
                        Symbol = c.String(maxLength: 16),
                        Description = c.String(maxLength: 512),
                        OperatorTypeId = c.Short(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreatedDate = c.DateTime(nullable: false),
                        LastModifiedDate = c.DateTime(nullable: false),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OperatorTypes", t => t.OperatorTypeId)
                .Index(t => t.OperatorTypeId);
            
            CreateTable(
                "dbo.OperatorTypes",
                c => new
                    {
                        Id = c.Short(nullable: false),
                        Name = c.String(maxLength: 32),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreatedDate = c.DateTime(nullable: false),
                        LastModifiedDate = c.DateTime(nullable: false),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SensorItems",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Status = c.String(nullable: false, maxLength: 1, fixedLength: true, unicode: false),
                        SensorId = c.Guid(nullable: false),
                        ItemId = c.Int(nullable: false),
                        UnitId = c.Short(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreatedDate = c.DateTime(nullable: false),
                        LastModifiedDate = c.DateTime(nullable: false),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Items", t => t.ItemId)
                .ForeignKey("dbo.Sensors", t => t.SensorId)
                .ForeignKey("dbo.Units", t => t.UnitId)
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
                        LastModifiedDate = c.DateTime(nullable: false),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SensorTypes", t => t.SensorTypeId)
                .ForeignKey("dbo.UnitTypes", t => t.UnitTypeId)
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
                        LastModifiedDate = c.DateTime(nullable: false),
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
                        LastModifiedDate = c.DateTime(nullable: false),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Sensors",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 128),
                        Reference = c.String(nullable: false, maxLength: 8),
                        Status = c.String(nullable: false, maxLength: 1, fixedLength: true, unicode: false),
                        SpotGPS = c.String(maxLength: 128),
                        Latitude = c.String(maxLength: 64),
                        Longitude = c.String(maxLength: 64),
                        SensorTypeId = c.Short(nullable: false),
                        SiteId = c.Guid(),
                        PondId = c.Guid(),
                        TankId = c.Guid(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreatedDate = c.DateTime(nullable: false),
                        LastModifiedDate = c.DateTime(nullable: false),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Ponds", t => t.PondId)
                .ForeignKey("dbo.SensorTypes", t => t.SensorTypeId)
                .ForeignKey("dbo.Sites", t => t.SiteId)
                .ForeignKey("dbo.Tanks", t => t.TankId)
                .Index(t => t.SensorTypeId)
                .Index(t => t.SiteId)
                .Index(t => t.PondId)
                .Index(t => t.TankId);
            
            CreateTable(
                "dbo.Ponds",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 128),
                        Description = c.String(),
                        Reference = c.String(nullable: false, maxLength: 8),
                        WaterVolumeCapacity = c.Decimal(nullable: false, precision: 18, scale: 2),
                        WaterVolumeCapacityUnitId = c.Short(nullable: false),
                        Status = c.String(nullable: false, maxLength: 1, fixedLength: true, unicode: false),
                        Latitude = c.String(maxLength: 64),
                        Longitude = c.String(maxLength: 64),
                        SiteId = c.Guid(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreatedDate = c.DateTime(nullable: false),
                        LastModifiedDate = c.DateTime(nullable: false),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sites", t => t.SiteId)
                .ForeignKey("dbo.Units", t => t.WaterVolumeCapacityUnitId)
                .Index(t => t.WaterVolumeCapacityUnitId)
                .Index(t => t.SiteId);
            
            CreateTable(
                "dbo.Sites",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 128),
                        Reference = c.String(nullable: false, maxLength: 8),
                        IPAddress = c.String(nullable: false, maxLength: 64),
                        Status = c.String(nullable: false, maxLength: 1, fixedLength: true, unicode: false),
                        Latitude = c.String(maxLength: 64),
                        Longitude = c.String(maxLength: 64),
                        CustomerId = c.Guid(nullable: false),
                        AddressId = c.Guid(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreatedDate = c.DateTime(nullable: false),
                        LastModifiedDate = c.DateTime(nullable: false),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Addresses", t => t.AddressId)
                .ForeignKey("dbo.Customers", t => t.CustomerId)
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
                        LastModifiedDate = c.DateTime(nullable: false),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Addresses", t => t.AddressId)
                .Index(t => t.AddressId);
            
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
                        LastModifiedDate = c.DateTime(nullable: false),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.CustomerUsers",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CustomerId = c.Guid(nullable: false),
                        AddressId = c.Guid(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreatedDate = c.DateTime(nullable: false),
                        LastModifiedDate = c.DateTime(nullable: false),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Addresses", t => t.AddressId)
                .ForeignKey("dbo.Customers", t => t.CustomerId)
                .Index(t => t.CustomerId)
                .Index(t => t.AddressId);
            
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
                        LastModifiedDate = c.DateTime(nullable: false),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CustomerUsers", t => t.CustomerUserId)
                .Index(t => t.CustomerUserId);
            
            CreateTable(
                "dbo.CustomerUserSites",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CustomerUserId = c.Guid(nullable: false),
                        SiteId = c.Guid(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreatedDate = c.DateTime(nullable: false),
                        LastModifiedDate = c.DateTime(nullable: false),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sites", t => t.SiteId)
                .ForeignKey("dbo.CustomerUsers", t => t.CustomerUserId)
                .Index(t => t.CustomerUserId)
                .Index(t => t.SiteId);
            
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
                        LastModifiedDate = c.DateTime(nullable: false),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UnitTypes", t => t.UnitTypeId)
                .Index(t => t.UnitTypeId);
            
            CreateTable(
                "dbo.Tanks",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 128),
                        Description = c.String(),
                        WaterVolumeCapacity = c.Decimal(nullable: false, precision: 18, scale: 2),
                        WaterVolumeCapacityUnitId = c.Short(nullable: false),
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
                        StickConversionId = c.Guid(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreatedDate = c.DateTime(nullable: false),
                        LastModifiedDate = c.DateTime(nullable: false),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sites", t => t.SiteId)
                .ForeignKey("dbo.StickConversions", t => t.StickConversionId)
                .ForeignKey("dbo.TankModels", t => t.TankModelId)
                .ForeignKey("dbo.Units", t => t.WaterVolumeCapacityUnitId)
                .Index(t => t.WaterVolumeCapacityUnitId)
                .Index(t => t.SiteId)
                .Index(t => t.TankModelId)
                .Index(t => t.StickConversionId);
            
            CreateTable(
                "dbo.StickConversions",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 128),
                        Status = c.String(nullable: false, maxLength: 1, fixedLength: true, unicode: false),
                        FromUnitId = c.Short(nullable: false),
                        ToUnitId = c.Short(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreatedDate = c.DateTime(nullable: false),
                        LastModifiedDate = c.DateTime(nullable: false),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Units", t => t.FromUnitId)
                .ForeignKey("dbo.Units", t => t.ToUnitId)
                .Index(t => t.FromUnitId)
                .Index(t => t.ToUnitId);
            
            CreateTable(
                "dbo.StickConversionValues",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ToValue = c.String(nullable: false, maxLength: 128),
                        FromValue = c.String(nullable: false, maxLength: 128),
                        StickConversionId = c.Guid(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreatedDate = c.DateTime(nullable: false),
                        LastModifiedDate = c.DateTime(nullable: false),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.StickConversions", t => t.StickConversionId)
                .Index(t => t.StickConversionId);
            
            CreateTable(
                "dbo.TankModels",
                c => new
                    {
                        Id = c.Int(nullable: false),
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
                        LastModifiedDate = c.DateTime(nullable: false),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Geometries", t => t.GeometryId)
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
                        LastModifiedDate = c.DateTime(nullable: false),
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
                        LastModifiedDate = c.DateTime(nullable: false),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 128),
                        CountryId = c.Short(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreatedDate = c.DateTime(nullable: false),
                        LastModifiedDate = c.DateTime(nullable: false),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Countries", t => t.CountryId)
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
                        LastModifiedDate = c.DateTime(nullable: false),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Contacts",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 128),
                        CustomerId = c.Guid(nullable: false),
                        AddressId = c.Guid(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreatedDate = c.DateTime(nullable: false),
                        LastModifiedDate = c.DateTime(nullable: false),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Addresses", t => t.AddressId)
                .ForeignKey("dbo.Customers", t => t.CustomerId)
                .Index(t => t.CustomerId)
                .Index(t => t.AddressId);
            
            CreateTable(
                "dbo.DataSyncs",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        SiteId = c.Guid(nullable: false),
                        Action = c.String(nullable: false, maxLength: 32),
                        SyncDate = c.DateTime(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreatedDate = c.DateTime(nullable: false),
                        LastModifiedDate = c.DateTime(nullable: false),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        SiteId = c.Guid(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreatedDate = c.DateTime(nullable: false),
                        LastModifiedDate = c.DateTime(nullable: false),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sites", t => t.SiteId)
                .Index(t => t.SiteId);
            
            CreateTable(
                "dbo.SensorGroups",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Weight = c.Int(nullable: false),
                        SensorId = c.Guid(nullable: false),
                        GroupId = c.Guid(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreatedDate = c.DateTime(nullable: false),
                        LastModifiedDate = c.DateTime(nullable: false),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Groups", t => t.GroupId)
                .ForeignKey("dbo.Sensors", t => t.SensorId)
                .Index(t => t.SensorId)
                .Index(t => t.GroupId);
            
            CreateTable(
                "dbo.Logs",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Message = c.String(maxLength: 4000),
                        CustomerId = c.Guid(),
                        UserId = c.Guid(),
                        SiteId = c.Guid(),
                        LogTypeId = c.Short(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreatedDate = c.DateTime(nullable: false),
                        LastModifiedDate = c.DateTime(nullable: false),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId)
                .ForeignKey("dbo.LogTypes", t => t.LogTypeId)
                .ForeignKey("dbo.Sites", t => t.SiteId)
                .Index(t => t.CustomerId)
                .Index(t => t.SiteId)
                .Index(t => t.LogTypeId);
            
            CreateTable(
                "dbo.LogTypes",
                c => new
                    {
                        Id = c.Short(nullable: false),
                        Name = c.String(maxLength: 16),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreatedDate = c.DateTime(nullable: false),
                        LastModifiedDate = c.DateTime(nullable: false),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Notifications",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        From = c.String(maxLength: 256),
                        To = c.String(maxLength: 4000),
                        Subject = c.String(maxLength: 128),
                        Message = c.String(),
                        ErrorMessage = c.String(storeType: "ntext"),
                        IsSentSuccess = c.DateTime(),
                        NotificationTypeId = c.Short(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreatedDate = c.DateTime(nullable: false),
                        LastModifiedDate = c.DateTime(nullable: false),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.NotificationTypes", t => t.NotificationTypeId)
                .Index(t => t.NotificationTypeId);
            
            CreateTable(
                "dbo.NotificationTypes",
                c => new
                    {
                        Id = c.Short(nullable: false),
                        Name = c.String(maxLength: 16),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreatedDate = c.DateTime(nullable: false),
                        LastModifiedDate = c.DateTime(nullable: false),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.NotificationTemplates",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(maxLength: 64),
                        Subject = c.String(maxLength: 128),
                        Message = c.String(),
                        NotificationTypeId = c.Short(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreatedDate = c.DateTime(nullable: false),
                        LastModifiedDate = c.DateTime(nullable: false),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.NotificationTypes", t => t.NotificationTypeId)
                .Index(t => t.NotificationTypeId);
            
            CreateTable(
                "dbo.SensorItemEvents",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Value = c.String(maxLength: 256),
                        EventDate = c.DateTime(nullable: false),
                        SensorItemId = c.Guid(nullable: false),
                        SensorItemEventId = c.Guid(),
                        CheckedAlarmDate = c.DateTime(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreatedDate = c.DateTime(nullable: false),
                        LastModifiedDate = c.DateTime(nullable: false),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SensorItems", t => t.SensorItemId)
                .ForeignKey("dbo.SensorItemEvents", t => t.SensorItemEventId)
                .Index(t => t.SensorItemId)
                .Index(t => t.SensorItemEventId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        AddressId = c.Guid(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreatedDate = c.DateTime(nullable: false),
                        LastModifiedDate = c.DateTime(nullable: false),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Addresses", t => t.AddressId)
                .Index(t => t.AddressId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "AddressId", "dbo.Addresses");
            DropForeignKey("dbo.SensorItemEvents", "SensorItemEventId", "dbo.SensorItemEvents");
            DropForeignKey("dbo.SensorItemEvents", "SensorItemId", "dbo.SensorItems");
            DropForeignKey("dbo.NotificationTemplates", "NotificationTypeId", "dbo.NotificationTypes");
            DropForeignKey("dbo.Notifications", "NotificationTypeId", "dbo.NotificationTypes");
            DropForeignKey("dbo.Logs", "SiteId", "dbo.Sites");
            DropForeignKey("dbo.Logs", "LogTypeId", "dbo.LogTypes");
            DropForeignKey("dbo.Logs", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Groups", "SiteId", "dbo.Sites");
            DropForeignKey("dbo.SensorGroups", "SensorId", "dbo.Sensors");
            DropForeignKey("dbo.SensorGroups", "GroupId", "dbo.Groups");
            DropForeignKey("dbo.Contacts", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Contacts", "AddressId", "dbo.Addresses");
            DropForeignKey("dbo.Cities", "CountryId", "dbo.Countries");
            DropForeignKey("dbo.Triggers", "SeverityId", "dbo.Severities");
            DropForeignKey("dbo.Triggers", "SensorItemId", "dbo.SensorItems");
            DropForeignKey("dbo.SensorItems", "UnitId", "dbo.Units");
            DropForeignKey("dbo.Tanks", "WaterVolumeCapacityUnitId", "dbo.Units");
            DropForeignKey("dbo.Tanks", "TankModelId", "dbo.TankModels");
            DropForeignKey("dbo.TankModels", "GeometryId", "dbo.Geometries");
            DropForeignKey("dbo.Tanks", "StickConversionId", "dbo.StickConversions");
            DropForeignKey("dbo.StickConversions", "ToUnitId", "dbo.Units");
            DropForeignKey("dbo.StickConversionValues", "StickConversionId", "dbo.StickConversions");
            DropForeignKey("dbo.StickConversions", "FromUnitId", "dbo.Units");
            DropForeignKey("dbo.Tanks", "SiteId", "dbo.Sites");
            DropForeignKey("dbo.Sensors", "TankId", "dbo.Tanks");
            DropForeignKey("dbo.Sensors", "SiteId", "dbo.Sites");
            DropForeignKey("dbo.Sensors", "SensorTypeId", "dbo.SensorTypes");
            DropForeignKey("dbo.SensorItems", "SensorId", "dbo.Sensors");
            DropForeignKey("dbo.Ponds", "WaterVolumeCapacityUnitId", "dbo.Units");
            DropForeignKey("dbo.Units", "UnitTypeId", "dbo.UnitTypes");
            DropForeignKey("dbo.Ponds", "SiteId", "dbo.Sites");
            DropForeignKey("dbo.Sites", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.CustomerUserSites", "CustomerUserId", "dbo.CustomerUsers");
            DropForeignKey("dbo.CustomerUserSites", "SiteId", "dbo.Sites");
            DropForeignKey("dbo.CustomerUserSettings", "CustomerUserId", "dbo.CustomerUsers");
            DropForeignKey("dbo.CustomerUsers", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.CustomerUsers", "AddressId", "dbo.Addresses");
            DropForeignKey("dbo.CustomerSettings", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Customers", "AddressId", "dbo.Addresses");
            DropForeignKey("dbo.Sites", "AddressId", "dbo.Addresses");
            DropForeignKey("dbo.Sensors", "PondId", "dbo.Ponds");
            DropForeignKey("dbo.SensorItems", "ItemId", "dbo.Items");
            DropForeignKey("dbo.Items", "UnitTypeId", "dbo.UnitTypes");
            DropForeignKey("dbo.Items", "SensorTypeId", "dbo.SensorTypes");
            DropForeignKey("dbo.Triggers", "OperatorId", "dbo.Operators");
            DropForeignKey("dbo.Operators", "OperatorTypeId", "dbo.OperatorTypes");
            DropForeignKey("dbo.TriggerContacts", "TriggerId", "dbo.Triggers");
            DropForeignKey("dbo.Alarms", "TriggerId", "dbo.Triggers");
            DropForeignKey("dbo.AlarmHistories", "ActionTypeId", "dbo.ActionTypes");
            DropIndex("dbo.Users", new[] { "AddressId" });
            DropIndex("dbo.SensorItemEvents", new[] { "SensorItemEventId" });
            DropIndex("dbo.SensorItemEvents", new[] { "SensorItemId" });
            DropIndex("dbo.NotificationTemplates", new[] { "NotificationTypeId" });
            DropIndex("dbo.Notifications", new[] { "NotificationTypeId" });
            DropIndex("dbo.Logs", new[] { "LogTypeId" });
            DropIndex("dbo.Logs", new[] { "SiteId" });
            DropIndex("dbo.Logs", new[] { "CustomerId" });
            DropIndex("dbo.SensorGroups", new[] { "GroupId" });
            DropIndex("dbo.SensorGroups", new[] { "SensorId" });
            DropIndex("dbo.Groups", new[] { "SiteId" });
            DropIndex("dbo.Contacts", new[] { "AddressId" });
            DropIndex("dbo.Contacts", new[] { "CustomerId" });
            DropIndex("dbo.Cities", new[] { "CountryId" });
            DropIndex("dbo.TankModels", new[] { "GeometryId" });
            DropIndex("dbo.StickConversionValues", new[] { "StickConversionId" });
            DropIndex("dbo.StickConversions", new[] { "ToUnitId" });
            DropIndex("dbo.StickConversions", new[] { "FromUnitId" });
            DropIndex("dbo.Tanks", new[] { "StickConversionId" });
            DropIndex("dbo.Tanks", new[] { "TankModelId" });
            DropIndex("dbo.Tanks", new[] { "SiteId" });
            DropIndex("dbo.Tanks", new[] { "WaterVolumeCapacityUnitId" });
            DropIndex("dbo.Units", new[] { "UnitTypeId" });
            DropIndex("dbo.CustomerUserSites", new[] { "SiteId" });
            DropIndex("dbo.CustomerUserSites", new[] { "CustomerUserId" });
            DropIndex("dbo.CustomerUserSettings", new[] { "CustomerUserId" });
            DropIndex("dbo.CustomerUsers", new[] { "AddressId" });
            DropIndex("dbo.CustomerUsers", new[] { "CustomerId" });
            DropIndex("dbo.CustomerSettings", new[] { "CustomerId" });
            DropIndex("dbo.Customers", new[] { "AddressId" });
            DropIndex("dbo.Sites", new[] { "AddressId" });
            DropIndex("dbo.Sites", new[] { "CustomerId" });
            DropIndex("dbo.Ponds", new[] { "SiteId" });
            DropIndex("dbo.Ponds", new[] { "WaterVolumeCapacityUnitId" });
            DropIndex("dbo.Sensors", new[] { "TankId" });
            DropIndex("dbo.Sensors", new[] { "PondId" });
            DropIndex("dbo.Sensors", new[] { "SiteId" });
            DropIndex("dbo.Sensors", new[] { "SensorTypeId" });
            DropIndex("dbo.Items", new[] { "SensorTypeId" });
            DropIndex("dbo.Items", new[] { "UnitTypeId" });
            DropIndex("dbo.SensorItems", new[] { "UnitId" });
            DropIndex("dbo.SensorItems", new[] { "ItemId" });
            DropIndex("dbo.SensorItems", new[] { "SensorId" });
            DropIndex("dbo.Operators", new[] { "OperatorTypeId" });
            DropIndex("dbo.TriggerContacts", new[] { "TriggerId" });
            DropIndex("dbo.Triggers", new[] { "OperatorId" });
            DropIndex("dbo.Triggers", new[] { "SeverityId" });
            DropIndex("dbo.Triggers", new[] { "SensorItemId" });
            DropIndex("dbo.Alarms", new[] { "TriggerId" });
            DropIndex("dbo.AlarmHistories", new[] { "ActionTypeId" });
            DropTable("dbo.Users");
            DropTable("dbo.SensorItemEvents");
            DropTable("dbo.NotificationTemplates");
            DropTable("dbo.NotificationTypes");
            DropTable("dbo.Notifications");
            DropTable("dbo.LogTypes");
            DropTable("dbo.Logs");
            DropTable("dbo.SensorGroups");
            DropTable("dbo.Groups");
            DropTable("dbo.DataSyncs");
            DropTable("dbo.Contacts");
            DropTable("dbo.Countries");
            DropTable("dbo.Cities");
            DropTable("dbo.Severities");
            DropTable("dbo.Geometries");
            DropTable("dbo.TankModels");
            DropTable("dbo.StickConversionValues");
            DropTable("dbo.StickConversions");
            DropTable("dbo.Tanks");
            DropTable("dbo.Units");
            DropTable("dbo.CustomerUserSites");
            DropTable("dbo.CustomerUserSettings");
            DropTable("dbo.CustomerUsers");
            DropTable("dbo.CustomerSettings");
            DropTable("dbo.Customers");
            DropTable("dbo.Sites");
            DropTable("dbo.Ponds");
            DropTable("dbo.Sensors");
            DropTable("dbo.UnitTypes");
            DropTable("dbo.SensorTypes");
            DropTable("dbo.Items");
            DropTable("dbo.SensorItems");
            DropTable("dbo.OperatorTypes");
            DropTable("dbo.Operators");
            DropTable("dbo.TriggerContacts");
            DropTable("dbo.Triggers");
            DropTable("dbo.Alarms");
            DropTable("dbo.AlarmHistories");
            DropTable("dbo.Addresses");
            DropTable("dbo.ActionTypes");
        }
    }
}
