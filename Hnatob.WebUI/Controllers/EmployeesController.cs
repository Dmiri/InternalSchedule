using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

// Embed

// Project
using Hnatob.DataAccessLayer.Abstract;
using Hnatob.Domain.Models;
using Hnatob.WebUI.Models;
using Hnatob.DataAccessLayer.Context;
using Hnatob.DataAccessLayer;
using Hnatob.WebUI.Models.Pagination;

namespace Hnatob.WebUI.Controllers
{
    [Authorize(Roles = "manager")]
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


        public ActionResult Users(int page = 1, int pageSize = 3/*20*/)
        {
            PeopleListPaginationViewMode peopleList = new PeopleListPaginationViewMode
            {
                People = repositoryPeople.GetUsers()
               .GroupJoin(repositoryPeople.GetEmployees(),
                       u => u.Id,
                       e => e.UserId,
                       (u, e) => new
                       {
                           User = u,
                           Employee = e.DefaultIfEmpty()
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
                        )
                        .OrderBy(o=>o.employeeView.Surname).ThenBy(o=>o.employeeView.Name).ThenBy(o=>o.Email)
                        .Skip((page - 1)*pageSize)
                        .Take(pageSize)
                        .ToList(),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = repositoryPeople.GetUsers().Count(),
                }
            };

            return View(peopleList);
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
            var model = new RoutViewModels();
            if (user != null)
            {
                model.Controller = "Employees";
                model.Action = "SaveDelete";
                model.Id = id;
                model.Title = "Delete";
                model.Data = new HtmlString(
                    "Are you shure you want to delete this person - </br><strong>" + user.Email + "</strong>?"
                    );
            }
            else
            {
                model.Controller = "Employees";
                model.Action = "Users";
                model.Id = id;
                model.Title = "Delete";
                model.Data = new HtmlString(
                    "This user not found" + user.Email
                    );
            }
            return PartialView("ModalConfirm", model);
        }

        [HttpPost]
        public ActionResult SaveDelete(RoutViewModels model)
        {
            repositoryPeople.Delete(model.Id);
            return RedirectToAction("Users");
        }

        [HttpPost]
        public ActionResult SetPosition(string id)
        {
            var user = repositoryPeople.GetUser(id);
            var model = new RoutViewModels
            {
                Controller = "Employees",
                Action = "Users",
                Id = id,
                Title = "Position"
            };
            if (user != null)
            {
                if (user.EmailConfirmed == true)
                {
                    model.Controller = "Employees";
                    model.Action = "SavePosition";
                    model.Id = id;
                    model.Title = "Position";
                    string htmlString = "";
                    string check = "";
                    var userPosition = repositoryPeople.GetEmployee(id)
                        ?.Positions.Select(p => p.Name)
                        .ToList() ?? new List<string>();
                    var allPosition = context.Positions.Select(p => p.Name)
                        .ToList() ?? new List<string>();
                    foreach (var item in allPosition)
                    {
                        check = "";
                        if (userPosition.Contains(item))
                        {
                            check = "checked";
                        }
                        htmlString +=
                       "<div class=\"form-check\">" +
                           $"<input id = \"{item}\" name=\"{item}\" type=\"checkbox\" {check} class=\"form-check-input\">" +
                           $"<label class=\"form-check-label\" for=\"{item}\" checked autocomplete=\"off\">{item}</label>" +
                       "</div>";
                    }
                    model.Data = new HtmlString(htmlString);
                }
                else
                {
                    model.Data = new HtmlString("<label>This person don't confirmed email.</label>");
                }
            }
            else
            {
                model.Data = new HtmlString("This user not found" + user.Email);
            }
            return PartialView("ModalConfirm", model);
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
                var empl = context.Employee.FirstOrDefault(e => e.UserId == id);
                //Delete all dependent person with position;
                List<Position> newPosts = new List<Position>();
                foreach (var key in Request.Form.AllKeys)
                {
                    if (key == "Id") continue;
                    var val = posts.FirstOrDefault(p => p.Name == key);
                    //Create new dependent;
                    if (val != null)
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
                    });
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
        public ActionResult SetAccess(string id)
        {
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
            var user = repositoryPeople.GetUser(id);
            var model = new RoutViewModels {
                Controller = "Employees",
                Action = "Users",
                Id = id,
                Title = "Access"
            };
            if (user != null)
            {
                if (user.EmailConfirmed == true)
                {
                    model.Controller = "Employees";
                    model.Action = "SaveAccess";
                    model.Id = id;
                    model.Title = "Access";

                    var userRole = userManager.GetRoles(id).ToList() ?? new List<string>();
                    var allRoles = context.Roles.Where(r => r.Name != "admin").Select(r => r.Name).ToList();
                    string htmlString = "";
                    foreach (var item in allRoles)
                    {
                        string check = "";
                        if (userRole.Contains(item))
                        {
                            check = "checked";
                        }
                        htmlString +=
                        "<div class=\"form-check\">" +
                            $"<input id = \"{item}\" name=\"{item}\" type=\"checkbox\" {check} class=\"form-check-input\">" +
                            $"<label class=\"form-check-label\" for=\"{item}\" checked autocomplete=\"off\">{item}</label>" +
                        "</div>";
                    }
                    model.Data = new HtmlString(htmlString);
                }
                else
                {
                    model.Data = new HtmlString("<label>This person don't confirmed email.</label>");
                }
            }
            else
            {
                model.Data = new HtmlString(
                    "This user not found" + user.Email
                    );
            }
            return PartialView("ModalConfirm", model);
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




