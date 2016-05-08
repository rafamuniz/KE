namespace KarmicEnergy.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AWS : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tanks", "StickConversionId", c => c.Int());
            AddColumn("dbo.Groups", "SiteId", c => c.Guid(nullable: false));
            CreateIndex("dbo.Tanks", "StickConversionId");
            CreateIndex("dbo.Groups", "SiteId");
            AddForeignKey("dbo.Tanks", "StickConversionId", "dbo.StickConversions", "Id");
            AddForeignKey("dbo.Groups", "SiteId", "dbo.Sites", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Groups", "SiteId", "dbo.Sites");
            DropForeignKey("dbo.Tanks", "StickConversionId", "dbo.StickConversions");
            DropIndex("dbo.Groups", new[] { "SiteId" });
            DropIndex("dbo.Tanks", new[] { "StickConversionId" });
            DropColumn("dbo.Groups", "SiteId");
            DropColumn("dbo.Tanks", "StickConversionId");
        }
    }
}
