namespace KarmicEnergy.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Customers", "ContactId", "dbo.Contacts");
            DropForeignKey("dbo.CustomerUsers", "ContactId", "dbo.Contacts");
            DropIndex("dbo.Customers", new[] { "ContactId" });
            DropIndex("dbo.CustomerUsers", new[] { "ContactId" });
            DropPrimaryKey("dbo.CustomerUserSettings");
            DropPrimaryKey("dbo.CustomerSettings");
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Email = c.String(nullable: false, maxLength: 256),
                        PhoneNumberCountryCode = c.String(maxLength: 3),
                        PhoneNumber = c.String(maxLength: 16),
                        MobileNumberCountryCode = c.String(nullable: false, maxLength: 3),
                        MobileNumber = c.String(nullable: false, maxLength: 16),
                        AddressLine1 = c.String(maxLength: 256),
                        AddressLine2 = c.String(maxLength: 256),
                        City = c.String(maxLength: 128),
                        State = c.String(maxLength: 64),
                        Country = c.String(maxLength: 64),
                        ZipCode = c.String(maxLength: 16),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreatedDate = c.DateTime(nullable: false),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Sites", "Latitude", c => c.String(maxLength: 64));
            AddColumn("dbo.Sites", "Longitude", c => c.String(maxLength: 64));
            AddColumn("dbo.Sites", "AddressId", c => c.Guid(nullable: false));
            AddColumn("dbo.Contacts", "Name", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.CustomerUserSettings", "Id", c => c.Guid(nullable: false, identity: true));
            AlterColumn("dbo.CustomerSettings", "Id", c => c.Guid(nullable: false, identity: true));
            AddPrimaryKey("dbo.CustomerUserSettings", "Id");
            AddPrimaryKey("dbo.CustomerSettings", "Id");
            CreateIndex("dbo.Sites", "AddressId");
            AddForeignKey("dbo.Sites", "AddressId", "dbo.Addresses", "Id", cascadeDelete: true);
            DropColumn("dbo.Customers", "ContactId");
            DropColumn("dbo.Contacts", "Email");
            DropColumn("dbo.Contacts", "PhoneNumberCountryCode");
            DropColumn("dbo.Contacts", "PhoneNumber");
            DropColumn("dbo.Contacts", "MobileNumberCountryCode");
            DropColumn("dbo.Contacts", "MobileNumber");
            DropColumn("dbo.Contacts", "AddressLine1");
            DropColumn("dbo.Contacts", "AddressLine2");
            DropColumn("dbo.Contacts", "City");
            DropColumn("dbo.Contacts", "State");
            DropColumn("dbo.Contacts", "Country");
            DropColumn("dbo.Contacts", "ZipCode");
            DropColumn("dbo.CustomerUsers", "ContactId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CustomerUsers", "ContactId", c => c.Guid(nullable: false));
            AddColumn("dbo.Contacts", "ZipCode", c => c.String(maxLength: 16));
            AddColumn("dbo.Contacts", "Country", c => c.String(maxLength: 64));
            AddColumn("dbo.Contacts", "State", c => c.String(maxLength: 64));
            AddColumn("dbo.Contacts", "City", c => c.String(maxLength: 128));
            AddColumn("dbo.Contacts", "AddressLine2", c => c.String(maxLength: 256));
            AddColumn("dbo.Contacts", "AddressLine1", c => c.String(maxLength: 256));
            AddColumn("dbo.Contacts", "MobileNumber", c => c.String(nullable: false, maxLength: 16));
            AddColumn("dbo.Contacts", "MobileNumberCountryCode", c => c.String(nullable: false, maxLength: 3));
            AddColumn("dbo.Contacts", "PhoneNumber", c => c.String(maxLength: 16));
            AddColumn("dbo.Contacts", "PhoneNumberCountryCode", c => c.String(maxLength: 3));
            AddColumn("dbo.Contacts", "Email", c => c.String(nullable: false, maxLength: 256));
            AddColumn("dbo.Customers", "ContactId", c => c.Guid(nullable: false));
            DropForeignKey("dbo.Sites", "AddressId", "dbo.Addresses");
            DropIndex("dbo.Sites", new[] { "AddressId" });
            DropPrimaryKey("dbo.CustomerSettings");
            DropPrimaryKey("dbo.CustomerUserSettings");
            AlterColumn("dbo.CustomerSettings", "Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.CustomerUserSettings", "Id", c => c.Guid(nullable: false));
            DropColumn("dbo.Contacts", "Name");
            DropColumn("dbo.Sites", "AddressId");
            DropColumn("dbo.Sites", "Longitude");
            DropColumn("dbo.Sites", "Latitude");
            DropTable("dbo.Addresses");
            AddPrimaryKey("dbo.CustomerSettings", "Id");
            AddPrimaryKey("dbo.CustomerUserSettings", "Id");
            CreateIndex("dbo.CustomerUsers", "ContactId");
            CreateIndex("dbo.Customers", "ContactId");
            AddForeignKey("dbo.CustomerUsers", "ContactId", "dbo.Contacts", "Id");
            AddForeignKey("dbo.Customers", "ContactId", "dbo.Contacts", "Id", cascadeDelete: true);
        }
    }
}
