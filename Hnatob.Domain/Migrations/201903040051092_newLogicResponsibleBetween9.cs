namespace Hnatob.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newLogicResponsibleBetween9 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Responsibles", "Event_Id", "dbo.Events");
            DropForeignKey("dbo.Responsibles", "PersonId", "dbo.People");
            DropIndex("dbo.Responsibles", new[] { "EventId" });
            DropIndex("dbo.Responsibles", new[] { "PersonId" });
            DropIndex("dbo.Responsibles", new[] { "Person_Id" });
            DropIndex("dbo.Responsibles", new[] { "Event_Id" });
            DropColumn("dbo.Responsibles", "EventId");
            DropColumn("dbo.Responsibles", "PersonId");
            RenameColumn(table: "dbo.Responsibles", name: "Event_Id", newName: "EventId");
            RenameColumn(table: "dbo.Responsibles", name: "Person_Id", newName: "PersonId");
            AlterColumn("dbo.Responsibles", "PersonId", c => c.Int());
            AlterColumn("dbo.Responsibles", "EventId", c => c.Int(nullable: false));
            CreateIndex("dbo.Responsibles", "EventId");
            CreateIndex("dbo.Responsibles", "PersonId");
            AddForeignKey("dbo.Responsibles", "EventId", "dbo.Events", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Responsibles", "PersonId", "dbo.People", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Responsibles", "PersonId", "dbo.People");
            DropForeignKey("dbo.Responsibles", "EventId", "dbo.Events");
            DropIndex("dbo.Responsibles", new[] { "PersonId" });
            DropIndex("dbo.Responsibles", new[] { "EventId" });
            AlterColumn("dbo.Responsibles", "EventId", c => c.Int());
            AlterColumn("dbo.Responsibles", "PersonId", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Responsibles", name: "PersonId", newName: "Person_Id");
            RenameColumn(table: "dbo.Responsibles", name: "EventId", newName: "Event_Id");
            AddColumn("dbo.Responsibles", "PersonId", c => c.Int(nullable: false));
            AddColumn("dbo.Responsibles", "EventId", c => c.Int(nullable: false));
            CreateIndex("dbo.Responsibles", "Event_Id");
            CreateIndex("dbo.Responsibles", "Person_Id");
            CreateIndex("dbo.Responsibles", "PersonId");
            CreateIndex("dbo.Responsibles", "EventId");
            AddForeignKey("dbo.Responsibles", "PersonId", "dbo.People", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Responsibles", "Event_Id", "dbo.Events", "Id");
        }
    }
}
