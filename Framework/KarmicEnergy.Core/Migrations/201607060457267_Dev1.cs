namespace KarmicEnergy.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Dev1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Notifications", "IsSentSuccess", c => c.DateTime());
            DropColumn("dbo.Notifications", "IsSent");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Notifications", "IsSent", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Notifications", "IsSentSuccess", c => c.Boolean());
        }
    }
}
