namespace KarmicEnergy.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial5 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.CustomerUsers", "CustomerId");
            CreateIndex("dbo.Sites", "CustomerId");
            AddForeignKey("dbo.CustomerUsers", "CustomerId", "dbo.Customers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Sites", "CustomerId", "dbo.Customers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sites", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.CustomerUsers", "CustomerId", "dbo.Customers");
            DropIndex("dbo.Sites", new[] { "CustomerId" });
            DropIndex("dbo.CustomerUsers", new[] { "CustomerId" });
        }
    }
}
