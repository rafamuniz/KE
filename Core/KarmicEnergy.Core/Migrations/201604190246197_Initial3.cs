namespace KarmicEnergy.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CustomerUsers", "AddressId", c => c.Guid(nullable: false));
            CreateIndex("dbo.CustomerUsers", "AddressId");
            AddForeignKey("dbo.CustomerUsers", "AddressId", "dbo.Addresses", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CustomerUsers", "AddressId", "dbo.Addresses");
            DropIndex("dbo.CustomerUsers", new[] { "AddressId" });
            DropColumn("dbo.CustomerUsers", "AddressId");
        }
    }
}
