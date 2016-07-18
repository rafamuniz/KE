namespace KarmicEnergy.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Dev : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DataSyncs", "Action", c => c.String(nullable: false, maxLength: 32));
            AddColumn("dbo.DataSyncs", "StartDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.DataSyncs", "EndDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.DataSyncs", "EndDate");
            DropColumn("dbo.DataSyncs", "StartDate");
            DropColumn("dbo.DataSyncs", "Action");
        }
    }
}
