namespace KarmicEnergy.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreatedDate = c.DateTime(nullable: false),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
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
                .ForeignKey("dbo.Groups", t => t.GroupId, cascadeDelete: true)
                .ForeignKey("dbo.Sensors", t => t.SensorId, cascadeDelete: true)
                .Index(t => t.SensorId)
                .Index(t => t.GroupId);
            
            AddColumn("dbo.Tanks", "FaceLength", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.Tanks", "BottomWidth", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.Tanks", "Radius", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.Tanks", "Diagonal", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.TankModels", "Radius", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.TankModels", "Diagonal", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.Geometries", "HasRadius", c => c.Boolean(nullable: false));
            AddColumn("dbo.Geometries", "HasDiagonal", c => c.Boolean(nullable: false));
            AddColumn("dbo.Geometries", "FormulaVolume", c => c.String(maxLength: 128));
            AddColumn("dbo.Contacts", "CustomerId", c => c.Guid(nullable: false));
            AddColumn("dbo.SensorItemEvents", "CalculatedValue", c => c.String(maxLength: 256));
            CreateIndex("dbo.Contacts", "CustomerId");
            AddForeignKey("dbo.Contacts", "CustomerId", "dbo.Customers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SensorGroups", "SensorId", "dbo.Sensors");
            DropForeignKey("dbo.SensorGroups", "GroupId", "dbo.Groups");
            DropForeignKey("dbo.Contacts", "CustomerId", "dbo.Customers");
            DropIndex("dbo.SensorGroups", new[] { "GroupId" });
            DropIndex("dbo.SensorGroups", new[] { "SensorId" });
            DropIndex("dbo.Contacts", new[] { "CustomerId" });
            DropColumn("dbo.SensorItemEvents", "CalculatedValue");
            DropColumn("dbo.Contacts", "CustomerId");
            DropColumn("dbo.Geometries", "FormulaVolume");
            DropColumn("dbo.Geometries", "HasDiagonal");
            DropColumn("dbo.Geometries", "HasRadius");
            DropColumn("dbo.TankModels", "Diagonal");
            DropColumn("dbo.TankModels", "Radius");
            DropColumn("dbo.Tanks", "Diagonal");
            DropColumn("dbo.Tanks", "Radius");
            DropColumn("dbo.Tanks", "BottomWidth");
            DropColumn("dbo.Tanks", "FaceLength");
            DropTable("dbo.SensorGroups");
            DropTable("dbo.Groups");
        }
    }
}
