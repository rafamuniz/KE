namespace KarmicEnergy.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AWS : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NotificationTemplates",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(maxLength: 64),
                        Subject = c.String(maxLength: 128),
                        Message = c.String(),
                        NotificationTypeId = c.Short(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreatedDate = c.DateTime(nullable: false),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.NotificationTypes", t => t.NotificationTypeId, cascadeDelete: true)
                .Index(t => t.NotificationTypeId);
            
            AddColumn("dbo.SensorItemEvents", "CheckedAlarmDate", c => c.DateTime());
            AlterColumn("dbo.Notifications", "Message", c => c.String());
            DropColumn("dbo.SensorItemEvents", "CheckedAlarm");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SensorItemEvents", "CheckedAlarm", c => c.Boolean(nullable: false));
            DropForeignKey("dbo.NotificationTemplates", "NotificationTypeId", "dbo.NotificationTypes");
            DropIndex("dbo.NotificationTemplates", new[] { "NotificationTypeId" });
            AlterColumn("dbo.Notifications", "Message", c => c.String(maxLength: 4000));
            DropColumn("dbo.SensorItemEvents", "CheckedAlarmDate");
            DropTable("dbo.NotificationTemplates");
        }
    }
}
