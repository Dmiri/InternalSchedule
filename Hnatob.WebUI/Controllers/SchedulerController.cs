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
        private readonly IPeopleRepository repositoryPeople = new EfPeopleRepository();

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
                dbEntry = repositoryEvent.GetObject((int)eventId);
            else dbEntry = new Event();



            //ViewBag.Employees = repositoryPeople.GetList()
            //    .Where(p => p.Position == EPosition.Accompanist.ToString() ||
            //        p.Position == EPosition.Actor.ToString() || 
            //        p.Position == EPosition.Choirmaster.ToString() || 
            //        p.Position == EPosition.Conductor.ToString() || 
            //        p.Position == EPosition.LightingDesigner.ToString() || 
            //        p.Position == EPosition.SoundEngineer.ToString() ||
            //        p.Position == EPosition.Producer.ToString())
            //    .Select(p => new ModelForEvent { Id = p.Id, Name = p.Name + " " + p.Patronymic, Position = p.Position })
            //    .ToList();
            //


            //TODO:: Function in fuction
            ViewBag.Employees = repositoryPeople.GetEmployee()
                            .Where((p) =>
                             {
                                 if (p.PositionId == (int)EPosition.Accompanist ||
                                      p.PositionId == (int)EPosition.Actor ||
                                      p.PositionId == (int)EPosition.Choirmaster ||
                                      p.PositionId == (int)EPosition.Conductor ||
                                      p.PositionId == (int)EPosition.LightingDesigner ||
                                      p.PositionId == (int)EPosition.SoundEngineer ||
                                      p.PositionId == (int)EPosition.Producer
                                     ) return true;
                                 return false;
                             })
                .Join(repositoryPeople.GetPositions(),
                        pos => pos.PositionId,
                        title => title.Id,
                        (pos, title) => new { PeopleId = pos.PersonId, Position = title.Name }
                        )
                       .Join(repositoryPeople.GetPeople(),
                            pos => pos.PeopleId,
                            per => per.Id,
                            (pos, per) => new ModelForEvent
                            { Id = per.Id, Name = per.Name + " " + per.Surname, Position = pos.Position }
                            )
                //.Where((p) =>
                //{
                //    if (p.Position == EPosition.Accompanist.ToString() ||
                //    p.Position == EPosition.Actor.ToString() ||
                //    p.Position == EPosition.Choirmaster.ToString() ||
                //    p.Position == EPosition.Conductor.ToString() ||
                //    p.Position == EPosition.LightingDesigner.ToString() ||
                //    p.Position == EPosition.SoundEngineer.ToString() ||
                //    p.Position == EPosition.Producer.ToString()
                //    )return true;
                //return false;
                //})
                .ToList();

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