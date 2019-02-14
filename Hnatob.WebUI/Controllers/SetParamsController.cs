using Hnatob.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hnatob.WebUI.Controllers
{
    public class SetParamsController : Controller
    {
        private readonly ScheduleRepository repositoryEvent;
        public SetParamsController(ScheduleRepository repository)
        {
            if(repository != null)
                repositoryEvent = repository;
            else throw new ArgumentNullException("No arguments");
        }
        // GET: SetParams
        public ActionResult Setting()
        {
            //var list = repositoryEvent.GetEventsTypes();
            var list = repositoryEvent.GetEvents()
                .Select(e => e.EventType)
                //.Where(e => !string.IsNullOrEmpty(e))
                .Where(e => (e != null && e != ""))
                .Distinct()
                .ToList();
            return View(list);
        }
    }
}