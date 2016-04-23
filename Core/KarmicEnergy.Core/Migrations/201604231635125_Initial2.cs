namespace KarmicEnergy.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Contacts", "CustomerId", c => c.Guid(nullable: false));
            CreateIndex("dbo.Contacts", "CustomerId");
            AddForeignKey("dbo.Contacts", "CustomerId", "dbo.Customers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Contacts", "CustomerId", "dbo.Customers");
            DropIndex("dbo.Contacts", new[] { "CustomerId" });
            DropColumn("dbo.Contacts", "CustomerId");
        }
    }
}
