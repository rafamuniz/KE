namespace KarmicEnergy.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Dev3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Notifications", "ErrorMessage", c => c.String(storeType: "ntext"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Notifications", "ErrorMessage", c => c.String(maxLength: 4000));
        }
    }
}
