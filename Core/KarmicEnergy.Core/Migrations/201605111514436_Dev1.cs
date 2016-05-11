namespace KarmicEnergy.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Dev1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TriggerContacts",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Status = c.String(nullable: false, maxLength: 1, fixedLength: true, unicode: false),
                        TriggerId = c.Guid(nullable: false),
                        ContactId = c.Guid(),
                        UserId = c.Guid(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreatedDate = c.DateTime(nullable: false),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Triggers", t => t.TriggerId, cascadeDelete: true)
                .Index(t => t.TriggerId);
            
            AddColumn("dbo.Triggers", "MinValue", c => c.String(maxLength: 256));
            AddColumn("dbo.Triggers", "MaxValue", c => c.String(maxLength: 256));
            DropColumn("dbo.Triggers", "Name");
            DropColumn("dbo.Triggers", "Email");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Triggers", "Email", c => c.String(nullable: false, maxLength: 256));
            AddColumn("dbo.Triggers", "Name", c => c.String(nullable: false, maxLength: 128));
            DropForeignKey("dbo.TriggerContacts", "TriggerId", "dbo.Triggers");
            DropIndex("dbo.TriggerContacts", new[] { "TriggerId" });
            DropColumn("dbo.Triggers", "MaxValue");
            DropColumn("dbo.Triggers", "MinValue");
            DropTable("dbo.TriggerContacts");
        }
    }
}
