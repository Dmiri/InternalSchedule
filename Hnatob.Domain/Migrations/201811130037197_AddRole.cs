namespace Hnatob.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRole : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.IRoles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PersonId = c.Int(nullable: false),
                        Event_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Events", t => t.Event_Id)
                .Index(t => t.Event_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IRoles", "Event_Id", "dbo.Events");
            DropIndex("dbo.IRoles", new[] { "Event_Id" });
            DropTable("dbo.IRoles");
        }
    }
}
