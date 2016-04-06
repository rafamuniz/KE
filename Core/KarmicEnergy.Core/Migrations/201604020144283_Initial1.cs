namespace KarmicEnergy.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial1 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Item", newName: "Items");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Items", newName: "Item");
        }
    }
}
