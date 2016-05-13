namespace KarmicEnergy.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Dev : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Sensors", "TankId", "dbo.Tanks");
            DropIndex("dbo.Sensors", new[] { "TankId" });
            AddColumn("dbo.Sensors", "SiteId", c => c.Guid());
            AlterColumn("dbo.Sensors", "TankId", c => c.Guid());
            CreateIndex("dbo.Sensors", "TankId");
            CreateIndex("dbo.Sensors", "SiteId");
            AddForeignKey("dbo.Sensors", "SiteId", "dbo.Sites", "Id");
            AddForeignKey("dbo.Sensors", "TankId", "dbo.Tanks", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sensors", "TankId", "dbo.Tanks");
            DropForeignKey("dbo.Sensors", "SiteId", "dbo.Sites");
            DropIndex("dbo.Sensors", new[] { "SiteId" });
            DropIndex("dbo.Sensors", new[] { "TankId" });
            AlterColumn("dbo.Sensors", "TankId", c => c.Guid(nullable: false));
            DropColumn("dbo.Sensors", "SiteId");
            CreateIndex("dbo.Sensors", "TankId");
            AddForeignKey("dbo.Sensors", "TankId", "dbo.Tanks", "Id", cascadeDelete: true);
        }
    }
}
