namespace KarmicEnergy.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial4 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.CustomerUsers");
            CreateTable(
                "dbo.Sites",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 128),
                        IPAddress = c.String(nullable: false, maxLength: 64),
                        CustomerId = c.Guid(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AlterColumn("dbo.CustomerUsers", "Id", c => c.Guid(nullable: false));
            AddPrimaryKey("dbo.CustomerUsers", "Id");
            DropColumn("dbo.CustomerUsers", "UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CustomerUsers", "UserId", c => c.Guid(nullable: false));
            DropPrimaryKey("dbo.CustomerUsers");
            AlterColumn("dbo.CustomerUsers", "Id", c => c.Guid(nullable: false, identity: true));
            DropTable("dbo.Sites");
            AddPrimaryKey("dbo.CustomerUsers", "Id");
        }
    }
}
