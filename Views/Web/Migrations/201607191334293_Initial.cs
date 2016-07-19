namespace KarmicEnergy.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.AspNetUsers", "LastModifiedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.AspNetUsers", "DeletedDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "DeletedDate");
            DropColumn("dbo.AspNetUsers", "LastModifiedDate");
            DropColumn("dbo.AspNetUsers", "CreatedDate");
        }
    }
}
