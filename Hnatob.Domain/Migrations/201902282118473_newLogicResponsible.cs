namespace Hnatob.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newLogicResponsible : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Responsibles", "Position_Id", "dbo.Positions");
            DropIndex("dbo.Responsibles", new[] { "Position_Id" });
            CreateTable(
                "dbo.CommentToServices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EventId = c.Int(nullable: false),
                        ServiceName = c.String(maxLength: 64),
                        Comment = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Events", t => t.EventId, cascadeDelete: true)
                .ForeignKey("dbo.Services", t => t.ServiceName)
                .Index(t => t.EventId)
                .Index(t => t.ServiceName);
            
            CreateTable(
                "dbo.Services",
                c => new
                    {
                        Name = c.String(nullable: false, maxLength: 64),
                        PersonId = c.Int(nullable: false),
                        Event_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Name)
                .ForeignKey("dbo.People", t => t.PersonId, cascadeDelete: true)
                .ForeignKey("dbo.Events", t => t.Event_Id)
                .Index(t => t.PersonId)
                .Index(t => t.Event_Id);
            
            AddColumn("dbo.People", "Service_Name", c => c.String(maxLength: 64));
            AddColumn("dbo.Responsibles", "EventId", c => c.Int(nullable: false));
            AddColumn("dbo.Responsibles", "PositionName", c => c.String(maxLength: 32));
            AddColumn("dbo.Responsibles", "PersonId", c => c.Int(nullable: false));
            AddColumn("dbo.Responsibles", "PersonName", c => c.String());
            CreateIndex("dbo.Responsibles", "EventId");
            CreateIndex("dbo.Responsibles", "PersonId");
            CreateIndex("dbo.People", "Service_Name");
            //AddForeignKey("dbo.Responsibles", "EventId", "dbo.Events", "Id", cascadeDelete: true);
            //AddForeignKey("dbo.Responsibles", "PersonId", "dbo.People", "Id", cascadeDelete: true);
            AddForeignKey("dbo.People", "Service_Name", "dbo.Services", "Name");
            DropColumn("dbo.Events", "Producer");
            DropColumn("dbo.Events", "Conductor");
            DropColumn("dbo.Events", "Choirmaster");
            DropColumn("dbo.Events", "Accompanist");
            DropColumn("dbo.Events", "LightingDesigner");
            DropColumn("dbo.Events", "SoundEngineer");
            DropColumn("dbo.Events", "Orchestra");
            DropColumn("dbo.Events", "Choir");
            DropColumn("dbo.Events", "Mimic");
            DropColumn("dbo.Responsibles", "Position_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Responsibles", "Position_Id", c => c.Int());
            AddColumn("dbo.Events", "Mimic", c => c.Boolean(nullable: false));
            AddColumn("dbo.Events", "Choir", c => c.Boolean(nullable: false));
            AddColumn("dbo.Events", "Orchestra", c => c.Boolean(nullable: false));
            AddColumn("dbo.Events", "SoundEngineer", c => c.String());
            AddColumn("dbo.Events", "LightingDesigner", c => c.String());
            AddColumn("dbo.Events", "Accompanist", c => c.String());
            AddColumn("dbo.Events", "Choirmaster", c => c.String());
            AddColumn("dbo.Events", "Conductor", c => c.String());
            AddColumn("dbo.Events", "Producer", c => c.String(nullable: false));
            DropForeignKey("dbo.CommentToServices", "ServiceName", "dbo.Services");
            DropForeignKey("dbo.CommentToServices", "EventId", "dbo.Events");
            DropForeignKey("dbo.Services", "Event_Id", "dbo.Events");
            DropForeignKey("dbo.Services", "PersonId", "dbo.People");
            DropForeignKey("dbo.People", "Service_Name", "dbo.Services");
            DropForeignKey("dbo.Responsibles", "PersonId", "dbo.People");
            DropForeignKey("dbo.Responsibles", "EventId", "dbo.Events");
            DropIndex("dbo.Services", new[] { "Event_Id" });
            DropIndex("dbo.Services", new[] { "PersonId" });
            DropIndex("dbo.People", new[] { "Service_Name" });
            DropIndex("dbo.Responsibles", new[] { "PersonId" });
            DropIndex("dbo.Responsibles", new[] { "EventId" });
            DropIndex("dbo.CommentToServices", new[] { "ServiceName" });
            DropIndex("dbo.CommentToServices", new[] { "EventId" });
            DropColumn("dbo.Responsibles", "PersonName");
            DropColumn("dbo.Responsibles", "PersonId");
            DropColumn("dbo.Responsibles", "PositionName");
            DropColumn("dbo.Responsibles", "EventId");
            DropColumn("dbo.People", "Service_Name");
            DropTable("dbo.Services");
            DropTable("dbo.CommentToServices");
            CreateIndex("dbo.Responsibles", "Position_Id");
            AddForeignKey("dbo.Responsibles", "Position_Id", "dbo.Positions", "Id");
        }
    }
}
