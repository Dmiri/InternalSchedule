// System
using System.Collections.Generic;
using System.Data.Entity;

// Project
using Hnatob.Domain.Models;

namespace Hnatob.DataAccessLayer.Context
{
    interface IDomainModelsContext
    {
        DbSet<Event> Events { get; set; }
        DbSet<Employee> Employee { get; set; }
        DbSet<Position> Positions { get; set; }
        DbSet<Service> Services { get; set; }

        DbSet<Responsible> Responsibles { get; set; }
        DbSet<CommentToService> CommentsToservices { get; set; }
    }

    //public class ApplicationDbContext : DbContext
    //{
    //    public ApplicationDbContext() : base("DefaultConnection")
    //    {
    //        //Database.SetInitializer( new MigrateDatabaseToLatestVersion<ApplicationDbContext, EF6Console.Migration.Configuration>());
    //        Database.SetInitializer(new SettingInitializer());
    //    }

    //    public DbSet<Event> Events { get; set; }
    //    public DbSet<Employee> People { get; set; }
    //    public DbSet<Position> Positions { get; set; }
    //    public DbSet<Service> Services { get; set; }

    //    public DbSet<Responsible> Responsibles { get; set; }
    //    public DbSet<CommentToService> CommentsToservices { get; set; }

    //    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    //    {
    //        //HasRequired(t => t.Agent).WithMany(a => a.Balances).HasForeignKey(t => t.AgentId);
    //        //modelBuilder.Entity<Responsible>().HasRequired(r => r.Event).WithMany().HasForeignKey(r => r.EventId);
    //        //modelBuilder.Entity<Responsible>().HasRequired(r => r.Person).WithMany().HasForeignKey(r => r.PersonId);
    //        base.OnModelCreating(modelBuilder);
    //    }
    //}

    //class SettingInitializer : CreateDatabaseIfNotExists<ApplicationDbContext>
    //{
    //    protected override void Seed(ApplicationDbContext context)
    //    {
    //        base.Seed(context);
    //    }
    //}

}
