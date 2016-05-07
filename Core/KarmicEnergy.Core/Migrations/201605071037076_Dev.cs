namespace KarmicEnergy.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Dev : DbMigration
    {
        public override void Up()
        {
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
                .ForeignKey("dbo.StickConversions", t => t.StickConversionId, cascadeDelete: true)
                .Index(t => t.StickConversionId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StickConversionValues", "StickConversionId", "dbo.StickConversions");
            DropForeignKey("dbo.StickConversions", "ToUnitId", "dbo.Units");
            DropForeignKey("dbo.StickConversions", "FromUnitId", "dbo.Units");
            DropIndex("dbo.StickConversionValues", new[] { "StickConversionId" });
            DropIndex("dbo.StickConversions", new[] { "ToUnitId" });
            DropIndex("dbo.StickConversions", new[] { "FromUnitId" });
            DropTable("dbo.StickConversionValues");
            DropTable("dbo.StickConversions");
        }
    }
}
