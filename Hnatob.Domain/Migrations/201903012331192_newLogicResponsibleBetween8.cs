namespace Hnatob.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newLogicResponsibleBetween8 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Responsibles", "PersonId", "dbo.People");
            AddColumn("dbo.Responsibles", "Person_Id", c => c.Int());
            CreateIndex("dbo.Responsibles", "Person_Id");
            AddForeignKey("dbo.Responsibles", "Person_Id", "dbo.People", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Responsibles", "Person_Id", "dbo.People");
            DropIndex("dbo.Responsibles", new[] { "Person_Id" });
            DropColumn("dbo.Responsibles", "Person_Id");
            AddForeignKey("dbo.Responsibles", "PersonId", "dbo.People", "Id", cascadeDelete: true);
        }
    }
}
