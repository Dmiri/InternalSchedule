namespace Hnatob.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newLogicResponsibleBetwin4 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.People", "ServiceName", "dbo.Services");
            DropIndex("dbo.People", new[] { "ServiceName" });
            DropColumn("dbo.People", "ServiceName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.People", "ServiceName", c => c.String(maxLength: 64));
            CreateIndex("dbo.People", "ServiceName");
            AddForeignKey("dbo.People", "ServiceName", "dbo.Services", "Name");
        }
    }
}
