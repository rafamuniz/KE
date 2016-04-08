namespace KarmicEnergy.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial7 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tanks", "Dimension1", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.Tanks", "Dimension2", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.Tanks", "Dimension3", c => c.Decimal(precision: 18, scale: 2));
            DropColumn("dbo.Tanks", "Dim1");
            DropColumn("dbo.Tanks", "Dim2");
            DropColumn("dbo.Tanks", "Dim3");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tanks", "Dim3", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.Tanks", "Dim2", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.Tanks", "Dim1", c => c.Decimal(precision: 18, scale: 2));
            DropColumn("dbo.Tanks", "Dimension3");
            DropColumn("dbo.Tanks", "Dimension2");
            DropColumn("dbo.Tanks", "Dimension1");
        }
    }
}
