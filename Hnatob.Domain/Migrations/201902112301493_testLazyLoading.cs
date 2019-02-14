namespace Hnatob.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class testLazyLoading : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PositionPersons", "Position_Id", "dbo.Positions");
            DropForeignKey("dbo.PositionPersons", "Person_Id", "dbo.People");
            DropIndex("dbo.PositionPersons", new[] { "Position_Id" });
            DropIndex("dbo.PositionPersons", new[] { "Person_Id" });
            DropTable("dbo.PositionPersons");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.PositionPersons",
                c => new
                    {
                        Position_Id = c.Int(nullable: false),
                        Person_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Position_Id, t.Person_Id });
            
            CreateIndex("dbo.PositionPersons", "Person_Id");
            CreateIndex("dbo.PositionPersons", "Position_Id");
            AddForeignKey("dbo.PositionPersons", "Person_Id", "dbo.People", "Id", cascadeDelete: true);
            AddForeignKey("dbo.PositionPersons", "Position_Id", "dbo.Positions", "Id", cascadeDelete: true);
        }
    }
}
