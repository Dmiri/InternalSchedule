namespace Hnatob.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class maybeFinal_2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Positions", "Name", c => c.String(nullable: false, maxLength: 32));
            CreateIndex("dbo.Positions", "Name", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Positions", new[] { "Name" });
            AlterColumn("dbo.Positions", "Name", c => c.String(nullable: false));
        }
    }
}
