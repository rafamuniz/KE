namespace KarmicEnergy.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AWS : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AlarmHistories",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Value = c.String(maxLength: 256),
                        CalculatedValue = c.String(maxLength: 256),
                        AckUserId = c.Guid(nullable: false),
                        AckDate = c.DateTime(nullable: false),
                        AlarmId = c.Guid(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreatedDate = c.DateTime(nullable: false),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Alarms", "CalculatedValue", c => c.String(maxLength: 256));
            AddColumn("dbo.Alarms", "LastAckUserId", c => c.Guid());
            AddColumn("dbo.Alarms", "LastAckDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Alarms", "LastAckDate");
            DropColumn("dbo.Alarms", "LastAckUserId");
            DropColumn("dbo.Alarms", "CalculatedValue");
            DropTable("dbo.AlarmHistories");
        }
    }
}
