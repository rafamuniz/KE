namespace KarmicEnergy.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial1 : DbMigration
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SensorGroups", "SensorId", "dbo.Sensors");
            DropForeignKey("dbo.SensorGroups", "GroupId", "dbo.Groups");
            DropIndex("dbo.SensorGroups", new[] { "GroupId" });
            DropIndex("dbo.SensorGroups", new[] { "SensorId" });
            DropTable("dbo.SensorGroups");
            DropTable("dbo.Groups");
        }
    }
}
