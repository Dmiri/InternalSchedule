namespace Hnatob.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateEvent : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Events", "Title", c => c.String(nullable: false));
            AlterColumn("dbo.Events", "Location", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Events", "Location", c => c.String());
            AlterColumn("dbo.Events", "Title", c => c.String());
        }
    }
}
