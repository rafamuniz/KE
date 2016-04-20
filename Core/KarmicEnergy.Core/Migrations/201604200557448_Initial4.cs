namespace KarmicEnergy.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial4 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Addresses", "Email", c => c.String(maxLength: 256));
            AlterColumn("dbo.Addresses", "MobileNumberCountryCode", c => c.String(maxLength: 3));
            AlterColumn("dbo.Addresses", "MobileNumber", c => c.String(maxLength: 16));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Addresses", "MobileNumber", c => c.String(nullable: false, maxLength: 16));
            AlterColumn("dbo.Addresses", "MobileNumberCountryCode", c => c.String(nullable: false, maxLength: 3));
            AlterColumn("dbo.Addresses", "Email", c => c.String(nullable: false, maxLength: 256));
        }
    }
}
