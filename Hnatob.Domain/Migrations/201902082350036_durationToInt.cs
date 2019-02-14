namespace Hnatob.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class durationToInt : DbMigration
    {
        public override void Up()
        {
            //AlterColumn("dbo.Events", "Duration", c => c.String(nullable: true));
            AddColumn("dbo.Events", "Duration", c => c.Int(nullable: true));
        }
        
        public override void Down()
        {
            //AlterColumn("dbo.Events", "Duration", c => c.DateTime(nullable: false));
            DropColumn("dbo.Events", "Duration");
        }
    }
}
