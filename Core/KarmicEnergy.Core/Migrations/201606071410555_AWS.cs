namespace KarmicEnergy.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AWS : DbMigration
    {
        public override void Up()
        {
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
                "dbo.LogTypes",
                c => new
                    {
                        Id = c.Short(nullable: false, identity: true),
                        Name = c.String(maxLength: 16),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreatedDate = c.DateTime(nullable: false),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
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
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OperatorTypes", t => t.OperatorTypeId, cascadeDelete: false)
                .Index(t => t.OperatorTypeId);
            
            CreateTable(
                "dbo.OperatorTypes",
                c => new
                    {
                        Id = c.Short(nullable: false, identity: true),
                        Name = c.String(maxLength: 32),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreatedDate = c.DateTime(nullable: false),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Logs", "SiteId", c => c.Guid());
            AddColumn("dbo.Logs", "LogTypeId", c => c.Short(nullable: false));
            AlterColumn("dbo.Logs", "CustomerId", c => c.Guid());
            AlterColumn("dbo.Logs", "UserId", c => c.Guid());
            CreateIndex("dbo.Logs", "LogTypeId");
            AddForeignKey("dbo.Logs", "LogTypeId", "dbo.LogTypes", "Id", cascadeDelete: false);
            DropColumn("dbo.Logs", "Type");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Logs", "Type", c => c.String(maxLength: 16));
            DropForeignKey("dbo.Operators", "OperatorTypeId", "dbo.OperatorTypes");
            DropForeignKey("dbo.Logs", "LogTypeId", "dbo.LogTypes");
            DropForeignKey("dbo.CustomerUserSites", "CustomerUserId", "dbo.CustomerUsers");
            DropForeignKey("dbo.CustomerUserSites", "SiteId", "dbo.Sites");
            DropIndex("dbo.Operators", new[] { "OperatorTypeId" });
            DropIndex("dbo.Logs", new[] { "LogTypeId" });
            DropIndex("dbo.CustomerUserSites", new[] { "SiteId" });
            DropIndex("dbo.CustomerUserSites", new[] { "CustomerUserId" });
            AlterColumn("dbo.Logs", "UserId", c => c.Guid(nullable: false));
            AlterColumn("dbo.Logs", "CustomerId", c => c.Guid(nullable: false));
            DropColumn("dbo.Logs", "LogTypeId");
            DropColumn("dbo.Logs", "SiteId");
            DropTable("dbo.OperatorTypes");
            DropTable("dbo.Operators");
            DropTable("dbo.LogTypes");
            DropTable("dbo.CustomerUserSites");
        }
    }
}
