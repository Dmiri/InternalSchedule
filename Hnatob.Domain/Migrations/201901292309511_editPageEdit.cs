namespace Hnatob.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editPageEdit : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Events", "Producer", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Events", "Producer", c => c.Int(nullable: false));
        }
    }
}
