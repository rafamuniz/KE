namespace KarmicEnergy.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Dev : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AlarmHistories", "Action", c => c.String(maxLength: 256));
            AddColumn("dbo.AlarmHistories", "Message", c => c.String(maxLength: 4000));
            AddColumn("dbo.AlarmHistories", "UserId", c => c.Guid(nullable: false));
            DropColumn("dbo.AlarmHistories", "AckUserId");
            DropColumn("dbo.AlarmHistories", "AckDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AlarmHistories", "AckDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.AlarmHistories", "AckUserId", c => c.Guid(nullable: false));
            DropColumn("dbo.AlarmHistories", "UserId");
            DropColumn("dbo.AlarmHistories", "Message");
            DropColumn("dbo.AlarmHistories", "Action");
        }
    }
}
