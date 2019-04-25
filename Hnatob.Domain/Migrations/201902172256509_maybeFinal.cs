namespace Hnatob.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class maybeFinal : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Positions", "SetResponsible_Id", "dbo.SetResponsibles");
            DropIndex("dbo.Positions", new[] { "SetResponsible_Id" });
            CreateTable(
                "dbo.Responsibles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Position = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropColumn("dbo.Positions", "SetResponsible_Id");
            DropTable("dbo.SetResponsibles");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.SetResponsibles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PositionId = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Positions", "SetResponsible_Id", c => c.Int());
            DropTable("dbo.Responsibles");
            CreateIndex("dbo.Positions", "SetResponsible_Id");
            AddForeignKey("dbo.Positions", "SetResponsible_Id", "dbo.SetResponsibles", "Id");
        }
    }
}
