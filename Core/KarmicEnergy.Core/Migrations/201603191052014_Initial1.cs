namespace KarmicEnergy.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TankModels", "ImageFileName", c => c.String(maxLength: 256));
            AlterColumn("dbo.TankModels", "Image", c => c.Binary());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TankModels", "Image", c => c.Binary(nullable: false));
            AlterColumn("dbo.TankModels", "ImageFileName", c => c.String(nullable: false, maxLength: 256));
        }
    }
}
