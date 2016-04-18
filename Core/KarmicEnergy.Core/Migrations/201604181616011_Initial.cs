namespace KarmicEnergy.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Contacts",
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
            
            CreateTable(
                "dbo.Units",
                c => new
                    {
                        Id = c.Short(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 128),
                        NamePlural = c.String(nullable: false, maxLength: 128),
                        Status = c.String(nullable: false, maxLength: 1, fixedLength: true, unicode: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreatedDate = c.DateTime(nullable: false),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Alarms", "DeletedDate", c => c.DateTime());
            AddColumn("dbo.Triggers", "DeletedDate", c => c.DateTime());
            AddColumn("dbo.SensorItems", "DeletedDate", c => c.DateTime());
            AddColumn("dbo.Items", "DeletedDate", c => c.DateTime());
            AddColumn("dbo.Sensors", "DeletedDate", c => c.DateTime());
            AddColumn("dbo.SensorTypes", "DeletedDate", c => c.DateTime());
            AddColumn("dbo.Tanks", "DeletedDate", c => c.DateTime());
            AddColumn("dbo.Sites", "DeletedDate", c => c.DateTime());
            AddColumn("dbo.Customers", "ContactId", c => c.Guid(nullable: false));
            AddColumn("dbo.Customers", "DeletedDate", c => c.DateTime());
            AddColumn("dbo.TankModels", "DeletedDate", c => c.DateTime());
            AddColumn("dbo.Geometries", "DeletedDate", c => c.DateTime());
            AddColumn("dbo.Severities", "DeletedDate", c => c.DateTime());
            AddColumn("dbo.Countries", "DeletedDate", c => c.DateTime());
            AddColumn("dbo.CustomerSettings", "DeletedDate", c => c.DateTime());
            AddColumn("dbo.CustomerUsers", "ContactId", c => c.Guid(nullable: false));
            AddColumn("dbo.CustomerUsers", "DeletedDate", c => c.DateTime());
            AddColumn("dbo.CustomerUserSettings", "DeletedDate", c => c.DateTime());
            AddColumn("dbo.SensorItemEvents", "DeletedDate", c => c.DateTime());
            CreateIndex("dbo.Customers", "ContactId");
            CreateIndex("dbo.CustomerUsers", "ContactId");
            AddForeignKey("dbo.Customers", "ContactId", "dbo.Contacts", "Id", cascadeDelete: true);
            AddForeignKey("dbo.CustomerUsers", "ContactId", "dbo.Contacts", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CustomerUsers", "ContactId", "dbo.Contacts");
            DropForeignKey("dbo.Customers", "ContactId", "dbo.Contacts");
            DropIndex("dbo.CustomerUsers", new[] { "ContactId" });
            DropIndex("dbo.Customers", new[] { "ContactId" });
            DropColumn("dbo.SensorItemEvents", "DeletedDate");
            DropColumn("dbo.CustomerUserSettings", "DeletedDate");
            DropColumn("dbo.CustomerUsers", "DeletedDate");
            DropColumn("dbo.CustomerUsers", "ContactId");
            DropColumn("dbo.CustomerSettings", "DeletedDate");
            DropColumn("dbo.Countries", "DeletedDate");
            DropColumn("dbo.Severities", "DeletedDate");
            DropColumn("dbo.Geometries", "DeletedDate");
            DropColumn("dbo.TankModels", "DeletedDate");
            DropColumn("dbo.Customers", "DeletedDate");
            DropColumn("dbo.Customers", "ContactId");
            DropColumn("dbo.Sites", "DeletedDate");
            DropColumn("dbo.Tanks", "DeletedDate");
            DropColumn("dbo.SensorTypes", "DeletedDate");
            DropColumn("dbo.Sensors", "DeletedDate");
            DropColumn("dbo.Items", "DeletedDate");
            DropColumn("dbo.SensorItems", "DeletedDate");
            DropColumn("dbo.Triggers", "DeletedDate");
            DropColumn("dbo.Alarms", "DeletedDate");
            DropTable("dbo.Units");
            DropTable("dbo.Contacts");
        }
    }
}
