namespace Hnatob.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class staf : DbMigration
    {
        public override void Up()
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
            CreateIndex("dbo.Positions", "SetResponsible_Id");
            AddForeignKey("dbo.Positions", "SetResponsible_Id", "dbo.SetResponsibles", "Id");
            DropTable("dbo.EventsTypes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.EventsTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.Positions", "SetResponsible_Id", "dbo.SetResponsibles");
            DropIndex("dbo.Positions", new[] { "SetResponsible_Id" });
            DropColumn("dbo.Positions", "SetResponsible_Id");
            DropTable("dbo.SetResponsibles");
        }
    }
}
