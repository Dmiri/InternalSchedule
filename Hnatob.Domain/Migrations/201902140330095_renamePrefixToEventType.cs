namespace Hnatob.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class renamePrefixToEventType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Events", "EventType", c => c.String());
            DropColumn("dbo.Events", "Prefix");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Events", "Prefix", c => c.String());
            DropColumn("dbo.Events", "EventType");
        }
    }
}
