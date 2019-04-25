namespace Hnatob.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newLogicResponsibleBetween7 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Responsibles", "EventId", "dbo.Events");
            AddColumn("dbo.Responsibles", "Comment", c => c.String(maxLength: 128));
            AddColumn("dbo.Responsibles", "Event_Id", c => c.Int());
            CreateIndex("dbo.Responsibles", "Event_Id");
            AddForeignKey("dbo.Responsibles", "Event_Id", "dbo.Events", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Responsibles", "Event_Id", "dbo.Events");
            DropIndex("dbo.Responsibles", new[] { "Event_Id" });
            DropColumn("dbo.Responsibles", "Event_Id");
            DropColumn("dbo.Responsibles", "Comment");
            AddForeignKey("dbo.Responsibles", "EventId", "dbo.Events", "Id", cascadeDelete: true);
        }
    }
}
