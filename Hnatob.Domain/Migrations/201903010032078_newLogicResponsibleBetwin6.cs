namespace Hnatob.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newLogicResponsibleBetwin6 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ServicePersons", newName: "PersonServices");
            DropPrimaryKey("dbo.PersonServices");
            AddPrimaryKey("dbo.PersonServices", new[] { "Person_Id", "Service_Name" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.PersonServices");
            AddPrimaryKey("dbo.PersonServices", new[] { "Service_Name", "Person_Id" });
            RenameTable(name: "dbo.PersonServices", newName: "ServicePersons");
        }
    }
}
