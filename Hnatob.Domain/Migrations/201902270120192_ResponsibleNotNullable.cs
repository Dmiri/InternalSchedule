namespace Hnatob.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ResponsibleNotNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Responsibles", "Position", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Responsibles", "Position", c => c.Int());
        }
    }
}
