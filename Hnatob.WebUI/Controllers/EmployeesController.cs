using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

// Embed

// Project
using Hnatob.Domain.Abstract;
using Hnatob.Domain.Helper;

namespace Hnatob.WebUI.Controllers
{
    public class EmployeesController : Controller
    {
        // GET: Employees
        private readonly IPeopleRepository repositoryPeople;

        public EmployeesController(IPeopleRepository repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("No arguments");
            }
            this.repositoryPeople = repository;
        }


        public ActionResult Employees()
        {
            List<Person> employee = repositoryPeople.GetPeople().Include(p => p.Positions).ToList();
            return View(employee);
        }


        [HttpGet]
        public ActionResult Details(int id)
        {
            if (id == 0) return RedirectToAction("Employees");
            var ev = repositoryPeople.GetPerson(id);
            return View(ev);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            repositoryPeople.Delete(id);
            return RedirectToAction("Employees");
        }
    }
}