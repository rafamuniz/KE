namespace KarmicEnergy.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
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
                "dbo.TankModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 128),
                        Image = c.Binary(nullable: false),
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
                        Status = c.String(nullable: false, maxLength: 1, fixedLength: true, unicode: false),
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tanks", "TankModelId", "dbo.TankModels");
            DropForeignKey("dbo.Tanks", "SiteId", "dbo.Sites");
            DropForeignKey("dbo.Sites", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.CustomerUsers", "CustomerId", "dbo.Customers");
            DropIndex("dbo.Tanks", new[] { "TankModelId" });
            DropIndex("dbo.Tanks", new[] { "SiteId" });
            DropIndex("dbo.Sites", new[] { "CustomerId" });
            DropIndex("dbo.CustomerUsers", new[] { "CustomerId" });
            DropTable("dbo.Tanks");
            DropTable("dbo.TankModels");
            DropTable("dbo.Sites");
            DropTable("dbo.CustomerUsers");
            DropTable("dbo.Customers");
            DropTable("dbo.Countries");
        }
    }
}
