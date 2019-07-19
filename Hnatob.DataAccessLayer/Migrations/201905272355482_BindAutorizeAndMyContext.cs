namespace Hnatob.DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BindAutorizeAndMyContext : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CommentToServices",
                c => new
                    {
                        EventId = c.Int(nullable: false),
                        ServiceName = c.String(nullable: false, maxLength: 64),
                        Comment = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => new { t.EventId, t.ServiceName })
                .ForeignKey("dbo.Events", t => t.EventId, cascadeDelete: true)
                .ForeignKey("dbo.Services", t => t.ServiceName, cascadeDelete: true)
                .Index(t => t.EventId)
                .Index(t => t.ServiceName);
            
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Access = c.String(nullable: false),
                        EventType = c.String(),
                        Title = c.String(nullable: false),
                        Location = c.String(nullable: false),
                        Start = c.DateTime(nullable: false),
                        Duration = c.Int(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Responsibles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EventId = c.Int(nullable: false),
                        PersonId = c.Int(),
                        PositionId = c.Int(nullable: false),
                        PersonName = c.String(maxLength: 64),
                        Comment = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Events", t => t.EventId, cascadeDelete: true)
                .ForeignKey("dbo.Positions", t => t.PositionId, cascadeDelete: true)
                .ForeignKey("dbo.Employees", t => t.PersonId)
                .Index(t => t.EventId)
                .Index(t => t.PersonId)
                .Index(t => t.PositionId);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nickname = c.String(),
                        Introduction = c.String(),
                        Description = c.String(),
                        Name = c.String(),
                        Patronymic = c.String(),
                        Surname = c.String(),
                        Birthday = c.DateTime(nullable: false),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Positions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 32),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true);
            
            CreateTable(
                "dbo.Services",
                c => new
                    {
                        Name = c.String(nullable: false, maxLength: 64),
                        Director = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Name);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.PositionEmployees",
                c => new
                    {
                        Position_Id = c.Int(nullable: false),
                        Employee_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Position_Id, t.Employee_Id })
                .ForeignKey("dbo.Positions", t => t.Position_Id, cascadeDelete: true)
                .ForeignKey("dbo.Employees", t => t.Employee_Id, cascadeDelete: true)
                .Index(t => t.Position_Id)
                .Index(t => t.Employee_Id);
            
            CreateTable(
                "dbo.ServiceEmployees",
                c => new
                    {
                        Service_Name = c.String(nullable: false, maxLength: 64),
                        Employee_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Service_Name, t.Employee_Id })
                .ForeignKey("dbo.Services", t => t.Service_Name, cascadeDelete: true)
                .ForeignKey("dbo.Employees", t => t.Employee_Id, cascadeDelete: true)
                .Index(t => t.Service_Name)
                .Index(t => t.Employee_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.CommentToServices", "ServiceName", "dbo.Services");
            DropForeignKey("dbo.Employees", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ServiceEmployees", "Employee_Id", "dbo.Employees");
            DropForeignKey("dbo.ServiceEmployees", "Service_Name", "dbo.Services");
            DropForeignKey("dbo.Responsibles", "PersonId", "dbo.Employees");
            DropForeignKey("dbo.Responsibles", "PositionId", "dbo.Positions");
            DropForeignKey("dbo.PositionEmployees", "Employee_Id", "dbo.Employees");
            DropForeignKey("dbo.PositionEmployees", "Position_Id", "dbo.Positions");
            DropForeignKey("dbo.Responsibles", "EventId", "dbo.Events");
            DropForeignKey("dbo.CommentToServices", "EventId", "dbo.Events");
            DropIndex("dbo.ServiceEmployees", new[] { "Employee_Id" });
            DropIndex("dbo.ServiceEmployees", new[] { "Service_Name" });
            DropIndex("dbo.PositionEmployees", new[] { "Employee_Id" });
            DropIndex("dbo.PositionEmployees", new[] { "Position_Id" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Positions", new[] { "Name" });
            DropIndex("dbo.Employees", new[] { "User_Id" });
            DropIndex("dbo.Responsibles", new[] { "PositionId" });
            DropIndex("dbo.Responsibles", new[] { "PersonId" });
            DropIndex("dbo.Responsibles", new[] { "EventId" });
            DropIndex("dbo.CommentToServices", new[] { "ServiceName" });
            DropIndex("dbo.CommentToServices", new[] { "EventId" });
            DropTable("dbo.ServiceEmployees");
            DropTable("dbo.PositionEmployees");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Services");
            DropTable("dbo.Positions");
            DropTable("dbo.Employees");
            DropTable("dbo.Responsibles");
            DropTable("dbo.Events");
            DropTable("dbo.CommentToServices");
        }
    }
}
