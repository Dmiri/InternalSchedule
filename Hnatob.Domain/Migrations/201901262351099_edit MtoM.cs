namespace Hnatob.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editMtoM : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        PositionId = c.Int(nullable: false),
                        PersonId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PositionId, t.PersonId });
            
            AlterColumn("dbo.Positions", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Positions", "Name", c => c.String());
            DropTable("dbo.Employees");
        }
    }
}
