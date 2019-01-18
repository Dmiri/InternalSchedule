namespace Hnatob.Domain.Migrations
{
    using Hnatob.Domain.Helper;
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EventUpdate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Events", "Access", c => c.String(nullable: false, defaultValue: Access.Private.ToString()));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Events", "Access", c => c.String());
        }
    }
}
