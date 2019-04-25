namespace Hnatob.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newLogicResponsibleBetwin2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Responsibles", "Position_Id", c => c.Int());
            AddColumn("dbo.Services", "Person_Id", c => c.Int());
            CreateIndex("dbo.Responsibles", "Position_Id");
            CreateIndex("dbo.Services", "Person_Id");
            AddForeignKey("dbo.Responsibles", "Position_Id", "dbo.Positions", "Id");
            AddForeignKey("dbo.Services", "Person_Id", "dbo.People", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Services", "Person_Id", "dbo.People");
            DropForeignKey("dbo.Responsibles", "Position_Id", "dbo.Positions");
            DropIndex("dbo.Services", new[] { "Person_Id" });
            DropIndex("dbo.Responsibles", new[] { "Position_Id" });
            DropColumn("dbo.Services", "Person_Id");
            DropColumn("dbo.Responsibles", "Position_Id");
        }
    }
}
