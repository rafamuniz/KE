namespace KarmicEnergy.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Dev1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sensors", "Latitude", c => c.String(maxLength: 64));
            AddColumn("dbo.Sensors", "Longitude", c => c.String(maxLength: 64));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sensors", "Longitude");
            DropColumn("dbo.Sensors", "Latitude");
        }
    }
}
