namespace KarmicEnergy.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial6 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TankModels", "DimensionValue1", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.TankModels", "DimensionValue2", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.TankModels", "DimensionValue3", c => c.Decimal(precision: 18, scale: 2));
            DropColumn("dbo.TankModels", "Dim1");
            DropColumn("dbo.TankModels", "Dim2Value");
            DropColumn("dbo.TankModels", "Dim3Value");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TankModels", "Dim3Value", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.TankModels", "Dim2Value", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.TankModels", "Dim1", c => c.Decimal(precision: 18, scale: 2));
            DropColumn("dbo.TankModels", "DimensionValue3");
            DropColumn("dbo.TankModels", "DimensionValue2");
            DropColumn("dbo.TankModels", "DimensionValue1");
        }
    }
}
