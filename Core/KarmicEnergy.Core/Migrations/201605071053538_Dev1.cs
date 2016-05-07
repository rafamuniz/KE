namespace KarmicEnergy.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Dev1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tanks", "StickConversionId", c => c.Int());
            CreateIndex("dbo.Tanks", "StickConversionId");
            AddForeignKey("dbo.Tanks", "StickConversionId", "dbo.StickConversions", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tanks", "StickConversionId", "dbo.StickConversions");
            DropIndex("dbo.Tanks", new[] { "StickConversionId" });
            DropColumn("dbo.Tanks", "StickConversionId");
        }
    }
}
