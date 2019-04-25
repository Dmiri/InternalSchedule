namespace Hnatob.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newLogicResponsibleBetwin5 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.People", "Service_Name", "dbo.Services");
            DropForeignKey("dbo.Services", "PersonId", "dbo.People");
            DropForeignKey("dbo.Services", "Person_Id", "dbo.People");
            DropForeignKey("dbo.Services", "Event_Id", "dbo.Events");
            DropForeignKey("dbo.Responsibles", "Position_Id", "dbo.Positions");
            DropIndex("dbo.Responsibles", new[] { "Position_Id" });
            DropIndex("dbo.People", new[] { "Service_Name" });
            DropIndex("dbo.Services", new[] { "PersonId" });
            DropIndex("dbo.Services", new[] { "Person_Id" });
            DropIndex("dbo.Services", new[] { "Event_Id" });
            DropColumn("dbo.Responsibles", "PositionId");
            RenameColumn(table: "dbo.Responsibles", name: "Position_Id", newName: "PositionId");
            CreateTable(
                "dbo.ServicePersons",
                c => new
                    {
                        Service_Name = c.String(nullable: false, maxLength: 64),
                        Person_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Service_Name, t.Person_Id })
                .ForeignKey("dbo.Services", t => t.Service_Name, cascadeDelete: true)
                .ForeignKey("dbo.People", t => t.Person_Id, cascadeDelete: true)
                .Index(t => t.Service_Name)
                .Index(t => t.Person_Id);
            
            AddColumn("dbo.Services", "Director", c => c.Int(nullable: false));
            AlterColumn("dbo.Responsibles", "PositionId", c => c.Int(nullable: false));
            AlterColumn("dbo.Responsibles", "PositionId", c => c.Int(nullable: false));
            CreateIndex("dbo.Responsibles", "PositionId");
            AddForeignKey("dbo.Responsibles", "PositionId", "dbo.Positions", "Id", cascadeDelete: false);
            DropColumn("dbo.People", "Service_Name");
            DropColumn("dbo.Services", "PersonId");
            DropColumn("dbo.Services", "Person_Id");
            DropColumn("dbo.Services", "Event_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Services", "Event_Id", c => c.Int());
            AddColumn("dbo.Services", "Person_Id", c => c.Int());
            AddColumn("dbo.Services", "PersonId", c => c.Int(nullable: false));
            AddColumn("dbo.People", "Service_Name", c => c.String(maxLength: 64));
            DropForeignKey("dbo.Responsibles", "PositionId", "dbo.Positions");
            DropForeignKey("dbo.ServicePersons", "Person_Id", "dbo.People");
            DropForeignKey("dbo.ServicePersons", "Service_Name", "dbo.Services");
            DropIndex("dbo.ServicePersons", new[] { "Person_Id" });
            DropIndex("dbo.ServicePersons", new[] { "Service_Name" });
            DropIndex("dbo.Responsibles", new[] { "PositionId" });
            AlterColumn("dbo.Responsibles", "PositionId", c => c.Int());
            AlterColumn("dbo.Responsibles", "PositionId", c => c.String());
            DropColumn("dbo.Services", "Director");
            DropTable("dbo.ServicePersons");
            RenameColumn(table: "dbo.Responsibles", name: "PositionId", newName: "Position_Id");
            AddColumn("dbo.Responsibles", "PositionId", c => c.String());
            CreateIndex("dbo.Services", "Event_Id");
            CreateIndex("dbo.Services", "Person_Id");
            CreateIndex("dbo.Services", "PersonId");
            CreateIndex("dbo.People", "Service_Name");
            CreateIndex("dbo.Responsibles", "Position_Id");
            AddForeignKey("dbo.Responsibles", "Position_Id", "dbo.Positions", "Id");
            AddForeignKey("dbo.Services", "Event_Id", "dbo.Events", "Id");
            AddForeignKey("dbo.Services", "Person_Id", "dbo.People", "Id");
            AddForeignKey("dbo.Services", "PersonId", "dbo.People", "Id", cascadeDelete: true);
            AddForeignKey("dbo.People", "Service_Name", "dbo.Services", "Name");
        }
    }
}
