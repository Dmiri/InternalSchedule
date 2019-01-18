namespace Hnatob.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditEvent : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Events", "Access", c => c.String());
            AddColumn("dbo.Events", "Duration", c => c.DateTime(nullable: false));
            DropColumn("dbo.Events", "Purpose");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Events", "Purpose", c => c.String());
            DropColumn("dbo.Events", "Duration");
            DropColumn("dbo.Events", "Access");
        }
    }
}
