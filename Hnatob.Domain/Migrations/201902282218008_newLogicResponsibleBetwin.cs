namespace Hnatob.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newLogicResponsibleBetwin : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CommentToServices", "ServiceName", "dbo.Services");
            DropIndex("dbo.CommentToServices", new[] { "ServiceName" });
            DropPrimaryKey("dbo.CommentToServices");
            AlterColumn("dbo.CommentToServices", "ServiceName", c => c.String(nullable: false, maxLength: 64));
            AddPrimaryKey("dbo.CommentToServices", new[] { "EventId", "ServiceName" });
            CreateIndex("dbo.CommentToServices", "ServiceName");
            AddForeignKey("dbo.CommentToServices", "ServiceName", "dbo.Services", "Name", cascadeDelete: false);
            DropColumn("dbo.CommentToServices", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CommentToServices", "Id", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.CommentToServices", "ServiceName", "dbo.Services");
            DropIndex("dbo.CommentToServices", new[] { "ServiceName" });
            DropPrimaryKey("dbo.CommentToServices");
            AlterColumn("dbo.CommentToServices", "ServiceName", c => c.String(maxLength: 64));
            AddPrimaryKey("dbo.CommentToServices", "Id");
            CreateIndex("dbo.CommentToServices", "ServiceName");
            AddForeignKey("dbo.CommentToServices", "ServiceName", "dbo.Services", "Name");
        }
    }
}
