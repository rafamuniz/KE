namespace KarmicEnergy.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Sensors");
            AddColumn("dbo.Sensors", "sId", c => c.Guid(nullable: false, identity: true));
            AddPrimaryKey("dbo.Sensors", "sId");
            DropColumn("dbo.Sensors", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Sensors", "Id", c => c.Long(nullable: false, identity: true));
            DropPrimaryKey("dbo.Sensors");
            DropColumn("dbo.Sensors", "sId");
            AddPrimaryKey("dbo.Sensors", "Id");
        }
    }
}
