namespace KarmicEnergy.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Dev1 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Logs", "CustomerId");
            AddForeignKey("dbo.Logs", "CustomerId", "dbo.Customers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Logs", "CustomerId", "dbo.Customers");
            DropIndex("dbo.Logs", new[] { "CustomerId" });
        }
    }
}
