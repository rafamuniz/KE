namespace KarmicEnergy.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial3 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Customers");
            AlterColumn("dbo.Customers", "Id", c => c.Guid(nullable: false));
            AddPrimaryKey("dbo.Customers", "Id");
            DropColumn("dbo.Customers", "UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Customers", "UserId", c => c.Guid(nullable: false));
            DropPrimaryKey("dbo.Customers");
            AlterColumn("dbo.Customers", "Id", c => c.Guid(nullable: false, identity: true));
            AddPrimaryKey("dbo.Customers", "Id");
        }
    }
}
