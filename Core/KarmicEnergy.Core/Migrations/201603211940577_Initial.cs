namespace KarmicEnergy.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Sensors",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 128),
                        Status = c.String(nullable: false, maxLength: 1, fixedLength: true, unicode: false),
                        TankId = c.Guid(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tanks", t => t.TankId, cascadeDelete: true)
                .Index(t => t.TankId);
            
            AddColumn("dbo.TankModels", "ImageFileName", c => c.String(maxLength: 256));
            AlterColumn("dbo.TankModels", "Image", c => c.Binary());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sensors", "TankId", "dbo.Tanks");
            DropIndex("dbo.Sensors", new[] { "TankId" });
            AlterColumn("dbo.TankModels", "Image", c => c.Binary(nullable: false));
            DropColumn("dbo.TankModels", "ImageFileName");
            DropTable("dbo.Sensors");
        }
    }
}
