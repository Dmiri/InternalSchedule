namespace Hnatob.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.IEvents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Purpose = c.String(),
                        Prefix = c.String(),
                        Title = c.String(),
                        Location = c.String(),
                        Start = c.DateTime(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IPersons",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Patronymic = c.String(),
                        Surname = c.String(),
                        Birthday = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.IPersons");
            DropTable("dbo.IEvents");
        }
    }
}
