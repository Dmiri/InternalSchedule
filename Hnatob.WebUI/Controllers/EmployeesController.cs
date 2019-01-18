using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hnatob.WebUI.Controllers
{
    public class EmployeesController : Controller
    {
        // GET: Employees
        public ActionResult Employees()
        {
            return View();
        }
    }
}