namespace Hnatob.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editPageEdit4 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Events", "Producer", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Events", "Producer", c => c.String());
        }
    }
}
