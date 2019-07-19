using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

// Embed

// Project
using Hnatob.DataAccessLayer.Abstract;
using Hnatob.Domain.Models;
using Hnatob.WebUI.Models;
using Microsoft.AspNet.Identity;
using Hnatob.DataAccessLayer.Context;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Threading.Tasks;
using Hnatob.DataAccessLayer;
using System.Diagnostics;

namespace Hnatob.WebUI.Controllers
{
    [Authorize (Roles = "manager")]
    public class EmployeesController : Controller
    {
        // GET: Employees
        private readonly IUserRepository repositoryPeople;
        private readonly ApplicationDbContext context;


        public EmployeesController(IUserRepository repository, ApplicationDbContext context)//)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("No arguments");
            }
            this.repositoryPeople = repository;

            if (context == null)
            {
                throw new ArgumentNullException("No arguments");
            }
            this.context = context;
        }


        public ActionResult Users()
        {
            var userList = repositoryPeople.GetUsers()
               .GroupJoin(repositoryPeople.GetEmployees(),
                       u => u.Id,
                       e => e.UserId,
                       (u, e) => new { User = u, Employee = e.DefaultIfEmpty()
                           .Select(x => new EmployeeView
                           {
                               Name = x.Name,
                               Surname = x.Surname,
                               Positions = x.Positions.ToList(),
                               Birthday = x.Birthday
                           })
                       })
                           .SelectMany(a => a.Employee
                               .Select(b => new User
                               {
                                   Id = a.User.Id,
                                   PhoneNumber = a.User.PhoneNumber,
                                   Email = a.User.Email,
                                   employeeView = b
                               })
                            ).ToList();

            return View(userList);
        }


        [HttpGet]
        public ActionResult Details(string id)
        {
            if (string.IsNullOrEmpty(id)) return RedirectToAction("Users");
            var user = repositoryPeople.GetUser(id);
            var empl = repositoryPeople.GetEmployee(id);

            var model = new UserDetaillsViewModel
            {
                Id = user.Id,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                Name = empl?.Name ?? "",
                Patronymic = empl?.Patronymic ?? "",
                Surname = empl?.Surname ?? "",
                Birthday = empl?.Birthday,
                Nickname = empl?.Nickname ?? "",
                Introduction = empl?.Introduction ?? "",
                Description = empl?.Description ?? ""
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(string id)
        {
            var user = repositoryPeople.GetUser(id);
            var model = new RoutViewModels
            {
                Controller = "Employees",
                Action = "Delete",
                Id = id,
                Title = "Delete",
                Data = "Are you shure you want to delete this person?<br/>" + user.Email
            };

            return PartialView("RoutViewModels", model);
        }

        [HttpPost]
        public ActionResult Delete(RoutViewModels model)
        {
            repositoryPeople.Delete(model.Id);
            return RedirectToAction("Users");
        }

        [HttpPost]
        public async Task<ActionResult> BlockUserAsync(string id)
        {
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
            var roles = await userManager.GetRolesAsync(id);
            await userManager.RemoveFromRolesAsync(id, roles.ToArray());
            await userManager.AddToRoleAsync(id, "user");
            context.SaveChanges();
            TempData["Message"] = string.Format($"User was blocked.");
            return PartialView();//RedirectToAction("Users");
        }

        [HttpPost]
        public async Task<ActionResult> SetPositionAsync(string id)
        {
            // принимает string Id человека, права которого будут изменены.
            var user =  await context.Users.FirstOrDefaultAsync(p => p.Id == id);
            SetDataViewModel viewModel = new SetDataViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                EmailConfirmed = user.EmailConfirmed
            };
            if (viewModel.EmailConfirmed)
            {
                var curentPerson = await context.Employee.FirstOrDefaultAsync(e => e.UserId == id);
                var posts = await context.Positions.Select(p => p.Name).ToListAsync();

                viewModel.CurentData = curentPerson.Positions.Select(p => p.Name).ToList();
                viewModel.AllData = posts;
            }
            // запрос к бд Users.EmailConfirmed, если пользователь подтвердил эмейл идем дальше иначе возвращяем сообщение "Пользователь еще не подтвердил эмейл"

            // этот метод возвращяет частичное представление с отображением действующих для пользователя разрешений.
            // сообщение отображаются в поп-ап - частичное представление
            return PartialView(viewModel);
        }

        [HttpPost]
        public ActionResult SetPosition(string id)
        {
            // принимает string Id человека, права которого будут изменены.
            var user = context.Users.FirstOrDefault(p => p.Id == id);
            SetDataViewModel viewModel = new SetDataViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                EmailConfirmed = user.EmailConfirmed
            };
            if (viewModel.EmailConfirmed)
            {
                var curentPerson = context.Employee.FirstOrDefault(e => e.UserId == id);
                var posts = context.Positions.Select(p => p.Name).ToList() ?? new List<string>();

                viewModel.CurentData = curentPerson?.Positions?.Select(p => p.Name).ToList() ?? new List<string>();
                viewModel.AllData = posts;
            }
            // запрос к бд Users.EmailConfirmed, если пользователь подтвердил эмейл идем дальше иначе возвращяем сообщение "Пользователь еще не подтвердил эмейл"

            // этот метод возвращяет частичное представление с отображением действующих для пользователя разрешений.
            // сообщение отображаются в поп-ап - частичное представление
            return PartialView(viewModel);
        }


        [HttpPost]
        public ActionResult SavePosition()
        {
            foreach (var key in Request.Form.AllKeys)
            {
                foreach (var val in Request.Form.GetValues(key))
                {
                    Trace.WriteLine(string.Format("{0}: {1}", key, val));
                }
            }
            //Find person;
            var id = Request.Form.GetValues("Id").FirstOrDefault();
            var user = context.Users.FirstOrDefault(u => u.Id == id);
            //Check EmailConfirmed;
            if (user.EmailConfirmed)
            {
            //Get all data of positions;
                var posts = context.Positions.ToList();
                var empl  = context.Employee.FirstOrDefault(e => e.UserId == id);
            //Delete all dependent person with position;
                List<Position> newPosts = new List<Position>();
                foreach (var key in Request.Form.AllKeys)
                {
                    if (key == "Id") continue;
                    var val = posts.FirstOrDefault(p => p.Name == key);
            //Create new dependent;
                    if( val != null)
                    {
                        newPosts.Add(val);
                    }

                }
                if (empl == null)
                {
            // запрос к бд Employee на создание нового работника и установление связи его с User(ом);
                    context.Employee.Add(new Employee {
                        UserId = id,
                        Birthday = DateTime.Now,
                        Positions = newPosts
                    } );
                }
                else
                {
                    // если нужно удалить работника и данные о нем при отсутствии у него должности. 
                    /*if(newPosts.Count == 0)
                    {
                        context.Employee.Remove(empl);
                    }
                    else
                    {*/
                        empl.Positions.ToList().Clear();
                        empl.Positions = newPosts;
                    //}
                }
                context.SaveChanges();
            }
            return Redirect("Users");
        }


        [HttpPost]
        public ActionResult SetAccesss(string id)
        {
            // принимает string Id человека, права которого будут изменены.
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
            var user = context.Users.FirstOrDefault(p => p.Id == id);
            SetDataViewModel viewModel = new SetDataViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                EmailConfirmed = user.EmailConfirmed
            };
            if (viewModel.EmailConfirmed)
            {
                viewModel.CurentData = userManager.GetRoles(id).ToList() ?? new List<string>();
                viewModel.AllData = context.Roles.Where(r => r.Name != "admin").Select( r => r.Name ).ToList();
            }
            // запрос к бд Users.EmailConfirmed, если пользователь подтвердил эмейл идем дальше иначе возвращяем сообщение "Пользователь еще не подтвердил эмейл"

            // этот метод возвращяет частичное представление с отображением действующих для пользователя разрешений.
            // сообщение отображаются в поп-ап - частичное представление
            return PartialView(viewModel);
        }


        [HttpPost]
        public ActionResult SaveAccess()
        {
            //Find person;
            var id = Request.Form.GetValues("Id").FirstOrDefault();
            var user = context.Users.FirstOrDefault(u => u.Id == id);
            //Check EmailConfirmed;
            if (user.EmailConfirmed)
            {
                var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
                var oldRoles = userManager.GetRoles(id);
                
                //Get all data of positions;
                var roles = context.Roles.Where(r => r.Name != "admin").Select(r => r.Name).ToList();
                List<string> newRoles = new List<string>();
                foreach (var key in Request.Form.AllKeys)
                {
                    if (key == "Id") continue;
                    if (roles.Contains(key))
                    {
                        newRoles.Add(key);
                    }
                }
                userManager.RemoveFromRoles(id, oldRoles.ToArray());
                userManager.AddToRoles(id, newRoles.ToArray());
                context.SaveChanges();
            }
            TempData["Message"] = string.Format($"Changes saved.");
            return Redirect("Users");
        }


    }
}




