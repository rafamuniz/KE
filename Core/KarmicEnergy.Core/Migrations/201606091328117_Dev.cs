namespace KarmicEnergy.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Dev : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Triggers", "Value", c => c.String(maxLength: 256));
            AddColumn("dbo.Triggers", "OperatorId", c => c.Short(nullable: false));
            CreateIndex("dbo.Triggers", "OperatorId");
            AddForeignKey("dbo.Triggers", "OperatorId", "dbo.Operators", "Id", cascadeDelete: false);
            DropColumn("dbo.Triggers", "MinValue");
            DropColumn("dbo.Triggers", "MaxValue");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Triggers", "MaxValue", c => c.String(maxLength: 256));
            AddColumn("dbo.Triggers", "MinValue", c => c.String(maxLength: 256));
            DropForeignKey("dbo.Triggers", "OperatorId", "dbo.Operators");
            DropIndex("dbo.Triggers", new[] { "OperatorId" });
            DropColumn("dbo.Triggers", "OperatorId");
            DropColumn("dbo.Triggers", "Value");
        }
    }
}
