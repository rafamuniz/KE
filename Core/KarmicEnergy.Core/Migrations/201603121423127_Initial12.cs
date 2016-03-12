namespace KarmicEnergy.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial12 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Customers", "Name");
            DropColumn("dbo.Customers", "Email");
            DropColumn("dbo.CustomerUsers", "Name");
            DropColumn("dbo.CustomerUsers", "Email");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CustomerUsers", "Email", c => c.String(nullable: false, maxLength: 256));
            AddColumn("dbo.CustomerUsers", "Name", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.Customers", "Email", c => c.String(nullable: false, maxLength: 256));
            AddColumn("dbo.Customers", "Name", c => c.String(nullable: false, maxLength: 128));
        }
    }
}
