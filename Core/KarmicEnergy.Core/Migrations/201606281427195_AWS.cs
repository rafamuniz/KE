namespace KarmicEnergy.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AWS : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ActionTypes",
                c => new
                    {
                        Id = c.Short(nullable: false, identity: true),
                        Name = c.String(maxLength: 16),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreatedDate = c.DateTime(nullable: false),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.AlarmHistories", "UserName", c => c.String(maxLength: 256));
            AddColumn("dbo.AlarmHistories", "ActionTypeId", c => c.Short(nullable: false));
            AddColumn("dbo.Alarms", "LastAckUserName", c => c.String(maxLength: 256));
            CreateIndex("dbo.AlarmHistories", "ActionTypeId");
            AddForeignKey("dbo.AlarmHistories", "ActionTypeId", "dbo.ActionTypes", "Id", cascadeDelete: true);
            DropColumn("dbo.AlarmHistories", "Action");
            DropColumn("dbo.AlarmHistories", "CalculatedValue");
            DropColumn("dbo.Alarms", "CalculatedValue");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Alarms", "CalculatedValue", c => c.String(maxLength: 256));
            AddColumn("dbo.AlarmHistories", "CalculatedValue", c => c.String(maxLength: 256));
            AddColumn("dbo.AlarmHistories", "Action", c => c.String(maxLength: 256));
            DropForeignKey("dbo.AlarmHistories", "ActionTypeId", "dbo.ActionTypes");
            DropIndex("dbo.AlarmHistories", new[] { "ActionTypeId" });
            DropColumn("dbo.Alarms", "LastAckUserName");
            DropColumn("dbo.AlarmHistories", "ActionTypeId");
            DropColumn("dbo.AlarmHistories", "UserName");
            DropTable("dbo.ActionTypes");
        }
    }
}
