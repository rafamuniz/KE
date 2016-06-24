namespace KarmicEnergy.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AWS : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Ponds",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 128),
                        Description = c.String(maxLength: 4000),
                        WaterVolumeCapacity = c.Decimal(nullable: false, precision: 18, scale: 2),
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
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreatedDate = c.DateTime(nullable: false),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sites", t => t.SiteId, cascadeDelete: false)
                .ForeignKey("dbo.TankModels", t => t.TankModelId, cascadeDelete: false)
                .Index(t => t.SiteId)
                .Index(t => t.TankModelId);
            
            AddColumn("dbo.Sensors", "PondId", c => c.Guid());
            CreateIndex("dbo.Sensors", "PondId");
            CreateIndex("dbo.SensorItemEvents", "SensorItemEventId");
            AddForeignKey("dbo.Sensors", "PondId", "dbo.Ponds", "Id");
            AddForeignKey("dbo.SensorItemEvents", "SensorItemEventId", "dbo.SensorItemEvents", "Id");
            DropColumn("dbo.SensorItemEvents", "CalculatedValue");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SensorItemEvents", "CalculatedValue", c => c.String(maxLength: 256));
            DropForeignKey("dbo.SensorItemEvents", "SensorItemEventId", "dbo.SensorItemEvents");
            DropForeignKey("dbo.Ponds", "TankModelId", "dbo.TankModels");
            DropForeignKey("dbo.Ponds", "SiteId", "dbo.Sites");
            DropForeignKey("dbo.Sensors", "PondId", "dbo.Ponds");
            DropIndex("dbo.SensorItemEvents", new[] { "SensorItemEventId" });
            DropIndex("dbo.Ponds", new[] { "TankModelId" });
            DropIndex("dbo.Ponds", new[] { "SiteId" });
            DropIndex("dbo.Sensors", new[] { "PondId" });
            DropColumn("dbo.Sensors", "PondId");
            DropTable("dbo.Ponds");
        }
    }
}
