namespace Hnatob.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newLogicResponsibleBetwin3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Responsibles", "PositionId", c => c.String());
            AddColumn("dbo.People", "ServiceName", c => c.String(maxLength: 64));
            AlterColumn("dbo.Responsibles", "PersonName", c => c.String(maxLength: 64));
            CreateIndex("dbo.People", "ServiceName");
            AddForeignKey("dbo.People", "ServiceName", "dbo.Services", "Name");
            DropColumn("dbo.Responsibles", "PositionName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Responsibles", "PositionName", c => c.String(maxLength: 32));
            DropForeignKey("dbo.People", "ServiceName", "dbo.Services");
            DropIndex("dbo.People", new[] { "ServiceName" });
            AlterColumn("dbo.Responsibles", "PersonName", c => c.String());
            DropColumn("dbo.People", "ServiceName");
            DropColumn("dbo.Responsibles", "PositionId");
        }
    }
}
