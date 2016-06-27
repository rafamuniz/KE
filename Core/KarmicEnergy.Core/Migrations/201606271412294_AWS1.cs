namespace KarmicEnergy.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AWS1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ponds", "Reference", c => c.String(nullable: false, maxLength: 8));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Ponds", "Reference");
        }
    }
}
