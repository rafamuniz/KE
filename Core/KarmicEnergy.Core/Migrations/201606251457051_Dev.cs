namespace KarmicEnergy.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Dev : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Notifications",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        From = c.String(maxLength: 256),
                        To = c.String(maxLength: 4000),
                        Subject = c.String(maxLength: 128),
                        Message = c.String(maxLength: 4000),
                        ErrorMessage = c.String(maxLength: 4000),
                        IsSentSuccess = c.Boolean(),
                        IsSent = c.Boolean(nullable: false),
                        NotificationTypeId = c.Short(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreatedDate = c.DateTime(nullable: false),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.NotificationTypes", t => t.NotificationTypeId, cascadeDelete: false)
                .Index(t => t.NotificationTypeId);
            
            CreateTable(
                "dbo.NotificationTypes",
                c => new
                    {
                        Id = c.Short(nullable: false, identity: true),
                        Name = c.String(maxLength: 16),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreatedDate = c.DateTime(nullable: false),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Notifications", "NotificationTypeId", "dbo.NotificationTypes");
            DropIndex("dbo.Notifications", new[] { "NotificationTypeId" });
            DropTable("dbo.NotificationTypes");
            DropTable("dbo.Notifications");
        }
    }
}
