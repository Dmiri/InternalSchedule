namespace Hnatob.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class reternMtoM : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.PositionPersons");
            CreateTable(
            "dbo.PositionPersons",
            c => new
            {
                Position_Id = c.Int(nullable: false),
                Person_Id = c.Int(nullable: false),
            })
            .PrimaryKey(t => new { t.Position_Id, t.Person_Id })
            .ForeignKey("dbo.Positions", t => t.Position_Id, cascadeDelete: true)
            .ForeignKey("dbo.People", t => t.Person_Id, cascadeDelete: true)
            .Index(t => t.Position_Id)
            .Index(t => t.Person_Id);
        }

        public override void Down()
        {
            DropIndex("dbo.PositionPersons", new[] { "Person_Id" });
            DropIndex("dbo.PositionPersons", new[] { "Position_Id" });
            DropTable("dbo.PositionPersons");
        }
    }
}
