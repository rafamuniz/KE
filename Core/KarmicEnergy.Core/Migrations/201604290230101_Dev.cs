namespace KarmicEnergy.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Dev : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Items", "UnitTypeId", c => c.Short(nullable: false, defaultValue: 1));
            AddColumn("dbo.Items", "SensorTypeId", c => c.Short(nullable: false, defaultValue: 1));
            CreateIndex("dbo.Items", "UnitTypeId");
            CreateIndex("dbo.Items", "SensorTypeId");
            AddForeignKey("dbo.Items", "SensorTypeId", "dbo.SensorTypes", "Id", cascadeDelete: false);
            AddForeignKey("dbo.Items", "UnitTypeId", "dbo.UnitTypes", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Items", "UnitTypeId", "dbo.UnitTypes");
            DropForeignKey("dbo.Items", "SensorTypeId", "dbo.SensorTypes");
            DropIndex("dbo.Items", new[] { "SensorTypeId" });
            DropIndex("dbo.Items", new[] { "UnitTypeId" });
            DropColumn("dbo.Items", "SensorTypeId");
            DropColumn("dbo.Items", "UnitTypeId");
        }
    }
}
