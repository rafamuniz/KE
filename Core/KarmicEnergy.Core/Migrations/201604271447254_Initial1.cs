namespace KarmicEnergy.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial1 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Contacts");
            AlterColumn("dbo.Contacts", "Id", c => c.Guid(nullable: false, identity: true));
            AddPrimaryKey("dbo.Contacts", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Contacts");
            AlterColumn("dbo.Contacts", "Id", c => c.Guid(nullable: false));
            AddPrimaryKey("dbo.Contacts", "Id");
        }
    }
}
