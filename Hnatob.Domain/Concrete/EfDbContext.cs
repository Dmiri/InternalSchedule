// System
using System.Collections.Generic;
using System.Data.Entity;

// Project
using Hnatob.Domain.Helper;

namespace Hnatob.Domain.Concrete
{
    public class EfDbContext : DbContext
    {
        public EfDbContext() : base("DefaultConnection")
        {
            //Database.SetInitializer( new MigrateDatabaseToLatestVersion<EfDbContext, EF6Console.Migration.Configuration>());
            Database.SetInitializer(new SettingInitializer());
        }

        public DbSet<Event> Events { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Service> Services { get; set; }

        public DbSet<Responsible> Responsibles { get; set; }
        public DbSet<CommentToService> CommentsToservices { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //HasRequired(t => t.Agent).WithMany(a => a.Balances).HasForeignKey(t => t.AgentId);
            //modelBuilder.Entity<Responsible>().HasRequired(r => r.Event).WithMany().HasForeignKey(r => r.EventId);
            //modelBuilder.Entity<Responsible>().HasRequired(r => r.Person).WithMany().HasForeignKey(r => r.PersonId);
            base.OnModelCreating(modelBuilder);
        }
    }

    class SettingInitializer : CreateDatabaseIfNotExists<EfDbContext>
    {
        protected override void Seed(EfDbContext context)
        {
            base.Seed(context);
        }
    }

}
