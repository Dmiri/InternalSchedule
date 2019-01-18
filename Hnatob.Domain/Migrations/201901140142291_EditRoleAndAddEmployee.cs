namespace Hnatob.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditRoleAndAddEmployee : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.IRoles", "Event_Id", "dbo.Events");
            DropIndex("dbo.IRoles", new[] { "Event_Id" });
            RenameColumn(table: "dbo.IRoles", name: "Event_Id", newName: "EventId");
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        PersonId = c.Int(nullable: false),
                        Position = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PersonId, t.Position });
            
            AlterColumn("dbo.IRoles", "EventId", c => c.Int(nullable: false));
            CreateIndex("dbo.IRoles", "EventId");
            AddForeignKey("dbo.IRoles", "EventId", "dbo.Events", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IRoles", "EventId", "dbo.Events");
            DropIndex("dbo.IRoles", new[] { "EventId" });
            AlterColumn("dbo.IRoles", "EventId", c => c.Int());
            DropTable("dbo.Employees");
            RenameColumn(table: "dbo.IRoles", name: "EventId", newName: "Event_Id");
            CreateIndex("dbo.IRoles", "Event_Id");
            AddForeignKey("dbo.IRoles", "Event_Id", "dbo.Events", "Id");
        }
    }
}
