namespace KarmicEnergy.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Dev2 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Logs", "UserId");
            CreateIndex("dbo.Logs", "SiteId");
            AddForeignKey("dbo.Logs", "SiteId", "dbo.Sites", "Id");
            //AddForeignKey("dbo.Logs", "UserId", "dbo.Users", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Logs", "UserId", "dbo.Users");
            DropForeignKey("dbo.Logs", "SiteId", "dbo.Sites");
            DropIndex("dbo.Logs", new[] { "SiteId" });
            DropIndex("dbo.Logs", new[] { "UserId" });
        }
    }
}
