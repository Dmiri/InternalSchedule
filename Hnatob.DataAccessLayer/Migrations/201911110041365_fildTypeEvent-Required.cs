namespace Hnatob.DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fildTypeEventRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Events", "EventType", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Events", "EventType", c => c.String());
        }
    }
}
