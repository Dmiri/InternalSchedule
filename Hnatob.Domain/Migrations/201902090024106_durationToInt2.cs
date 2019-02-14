namespace Hnatob.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class durationToInt2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Events", "Duration", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Events", "Duration", c => c.Int(nullable: false));
        }
    }
}
