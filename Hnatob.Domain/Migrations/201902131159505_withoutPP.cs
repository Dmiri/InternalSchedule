namespace Hnatob.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class withoutPP : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.PositionPersons", newName: "PositionPersons");
            DropTable("dbo.PositionPersons");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.PositionPersons",
                c => new
                    {
                        PositionId = c.Int(nullable: false),
                        PersonId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PositionId, t.PersonId });
            
            RenameTable(name: "dbo.PositionPersons", newName: "PositionPersons");
        }
    }
}
