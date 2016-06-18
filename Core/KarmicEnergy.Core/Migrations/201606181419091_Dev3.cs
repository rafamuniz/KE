namespace KarmicEnergy.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Dev3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Logs", "UserId", "dbo.Users");
            DropIndex("dbo.Logs", new[] { "UserId" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.Logs", "UserId");
            AddForeignKey("dbo.Logs", "UserId", "dbo.Users", "Id");
        }
    }
}
