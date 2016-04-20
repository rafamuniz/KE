namespace KarmicEnergy.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Contacts", "AddressId", c => c.Guid(nullable: false));
            CreateIndex("dbo.Contacts", "AddressId");
            AddForeignKey("dbo.Contacts", "AddressId", "dbo.Addresses", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Contacts", "AddressId", "dbo.Addresses");
            DropIndex("dbo.Contacts", new[] { "AddressId" });
            DropColumn("dbo.Contacts", "AddressId");
        }
    }
}
