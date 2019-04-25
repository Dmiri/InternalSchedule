namespace Hnatob.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ResponsibleOneToOne : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Responsibles", "Position_Id", c => c.Int());
            CreateIndex("dbo.Responsibles", "Position_Id");
            AddForeignKey("dbo.Responsibles", "Position_Id", "dbo.Positions", "Id");
            DropColumn("dbo.Responsibles", "Position");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Responsibles", "Position", c => c.Int(nullable: false));
            DropForeignKey("dbo.Responsibles", "Position_Id", "dbo.Positions");
            DropIndex("dbo.Responsibles", new[] { "Position_Id" });
            DropColumn("dbo.Responsibles", "Position_Id");
        }
    }
}
