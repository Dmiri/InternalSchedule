// System
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

// Embed

// Project
using Hnatob.Domain.Abstract;
using Hnatob.Domain.Concrete;
using Hnatob.Domain.Helper;
using Hnatob.Domain.Models;

namespace Hnatob.WebUI.Controllers
{
    public class SchedulerController : Controller
    {
        private readonly ScheduleRepository repositoryEvent = new EfScheduleRepository();

        // TODO: There will be "Constuctor Injection" when get "new EfScheduleRepository()"
        public SchedulerController(ScheduleRepository repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("No arguments");
            }
            this.repositoryEvent = repository;
        }



        // GET: Scheduler
        public ViewResult Schedule()
        {
            //IScheduleRepository repository = new EfScheduleRepository();
            List<Event> schedule;
            if (User.IsInRole("Editor"))
            {
                //TODU wiew all schedule and add ability to edit
                schedule = repositoryEvent.GetList().ToList();
            }

            else
            {
                if (User.IsInRole("Employee"))
                {
                    //TODU wiew all schedule
                    schedule = repositoryEvent.GetList().ToList();
                }
                //TODU wiew public schedule
                else schedule = repositoryEvent.GetList()/*.Where(l => l.Access == Access.Public.ToString())*/.ToList();
            }
            return View("Schedule", schedule);
        }

        public ActionResult Edit(int? eventId)
        {
            Event dbEntry;
            if (eventId != 0 && eventId != null)
                dbEntry = (Event)repositoryEvent.GetObject((int)eventId);
            else dbEntry = new Event();
            return View(dbEntry);
        }

        [HttpPost]
        public ActionResult Edit(Event dbEntry)
        {
            bool serverValidation = false;
            if (!string.IsNullOrWhiteSpace(dbEntry.Title)
                && !string.IsNullOrWhiteSpace(dbEntry.Location)
                //&& !string.IsNullOrWhiteSpace(dbEntry.Description)
                && dbEntry.Start > DateTime.Now && dbEntry.Start < DateTime.MaxValue
                ) serverValidation = true;

            if (serverValidation && ModelState.IsValid)
            {
                //ScheduleRepository repository = new EfScheduleRepository();
                repositoryEvent.Update(dbEntry);
                TempData["Message"] = string.Format($"The change was saved for the event \"{dbEntry.Title}\"");
            }
            else return View(dbEntry);
            IEnumerable<Event> schedule = repositoryEvent.GetList();
            return View("Schedule", schedule);
        }
    }
}