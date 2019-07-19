using System;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

using Hnatob.Domain.Abstract;
using Hnatob.Domain.Models;

namespace Hnatob.DataAccessLayer.Context
{
    ////You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    //public class ApplicationUser : IdentityUser
    //{
    //    public int EmployeeId { get; set; }
    //    public Employee Employee { get; set; }

    //    public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
    //    {
    //        // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
    //        var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
    //        // Add custom user claims here
    //        return userIdentity;
    //    }
    //}

    //public class RoleInit : CreateDatabaseIfNotExists<ApplicationDbContext>
    //{
    //    protected override void Seed(ApplicationDbContext context)
    //    {
    //        base.Seed(context);

    //        var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
    //        var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

    //        // создаем две роли
    //        var role0 = new IdentityRole { Name = "admin" };
    //        var role1 = new IdentityRole { Name = "user" };
    //        var role2 = new IdentityRole { Name = "employee" };
    //        var role3 = new IdentityRole { Name = "editor" };
    //        var role4 = new IdentityRole { Name = "manager" };

    //        // добавляем роли в бд
    //        roleManager.Create(role0);
    //        roleManager.Create(role1);
    //        roleManager.Create(role2);
    //        roleManager.Create(role3);
    //        roleManager.Create(role4);

    //        // создаем пользователей
    //        var admin = new ApplicationUser { Email = "adddmin@gmail.com", UserName = "admin" };
    //        string password = "Qe12439524";
    //        var result = userManager.Create(admin, password);

    //        // если создание пользователя прошло успешно
    //        if (result.Succeeded)
    //        {
    //            // добавляем для пользователя роль
    //            userManager.AddToRole(admin.Id, role0.Name);
    //            userManager.AddToRole(admin.Id, role1.Name);
    //            userManager.AddToRole(admin.Id, role2.Name);
    //            userManager.AddToRole(admin.Id, role3.Name);
    //            userManager.AddToRole(admin.Id, role4.Name);
    //        }

    //        context.SaveChanges();


    //    }
    //}


    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IDomainModelsContext
    {
        public DbSet<Event> Events { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Service> Services { get; set; }

        public DbSet<Responsible> Responsibles { get; set; }
        public DbSet<CommentToService> CommentsToservices { get; set; }



        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            //Database.SetInitializer<ApplicationDbContext>(new RoleInit());
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}