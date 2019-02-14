namespace Hnatob.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class testLazyLoading2 : DbMigration
    {
        public override void Up()
        {
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
            DropForeignKey("dbo.PositionPersons", "Person_Id", "dbo.People");
            DropForeignKey("dbo.PositionPersons", "Position_Id", "dbo.Positions");
            DropIndex("dbo.PositionPersons", new[] { "Person_Id" });
            DropIndex("dbo.PositionPersons", new[] { "Position_Id" });
            DropTable("dbo.PositionPersons");
        }
    }
}
