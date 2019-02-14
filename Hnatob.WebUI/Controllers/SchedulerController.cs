// System
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

// Embed

// Project
using Hnatob.Domain.Abstract;
using Hnatob.Domain.Concrete;
using Hnatob.Domain.Helper;
using Hnatob.WebUI.Models;

namespace Hnatob.WebUI.Controllers
{
    public class SchedulerController : Controller
    {
        private readonly ScheduleRepository repositoryEvent;// = new EfScheduleRepository();
        private readonly IPeopleRepository repositoryPeople;// = new EfPeopleRepository();

        // TODO: There will be "Constuctor Injection" when get "new EfScheduleRepository()" , IPeopleRepository
        public SchedulerController(ScheduleRepository repositoryEvent, IPeopleRepository repositoryPeople)
        {
            if (repositoryEvent == null)
            {
                throw new ArgumentNullException("No arguments");
            }
            if (repositoryPeople == null)
            {
                throw new ArgumentNullException("No arguments");
            }
            this.repositoryEvent = repositoryEvent;
            this.repositoryPeople = repositoryPeople;
        }



        // GET: Scheduler
        public ViewResult Schedule()
        {
            List<Event> schedule;
            //TODU: use case
            if (User.IsInRole("Editor"))
            {
                //TODU wiew all schedule and add ability to edit
                schedule = repositoryEvent.GetEvents()
                    .OrderBy(e => e.Start).ThenBy(e => e.Location).ThenBy(e => e.Title)
                    .ToList();
            }

            else
            {
                if (User.IsInRole("Employee"))
                {
                    //TODU wiew all schedule
                    schedule = repositoryEvent.GetEvents()
                        .OrderBy(e => e.Start).ThenBy(e => e.Location).ThenBy(e => e.Title)
                        .ToList();
                }
                //TODU wiew public schedule
                else schedule = repositoryEvent.GetEvents()/*.Where(l => l.Access == Access.Public.ToString())*/
                        .OrderBy(e => e.Start).ThenBy(e => e.Location).ThenBy(e => e.Title)
                        .ToList();
            }
            return View("Schedule", schedule);
        }

        public ActionResult Edit(int? eventId)
        {
            Event dbEntry;
            if (eventId != 0 && eventId != null)
                dbEntry = repositoryEvent.GetObject((int)eventId);
            else dbEntry = new Event();

            //TODO: check optimum query
            //ViewBag.Employees = repositoryPeople.GetEmployee()
            //                .Where((p) =>
            //                 {
            //                     if (p.PositionId == (int)EPosition.Accompanist ||
            //                          p.PositionId == (int)EPosition.Actor ||
            //                          p.PositionId == (int)EPosition.Choirmaster ||
            //                          p.PositionId == (int)EPosition.Conductor ||
            //                          p.PositionId == (int)EPosition.LightingDesigner ||
            //                          p.PositionId == (int)EPosition.SoundEngineer ||
            //                          p.PositionId == (int)EPosition.Producer
            //                         ) return true;
            //                     return false;
            //                 })
            //    .Join(repositoryPeople.GetPositions(),
            //            pos => pos.PositionId,
            //            title => title.Id,
            //            (pos, title) => new { PeopleId = pos.PersonId, Position = title.Name }
            //            )
            //           .Join(repositoryPeople.GetPeople(),
            //                pos => pos.PeopleId,
            //                per => per.Id,
            //                (pos, per) => new ModelForEvent
            //                { Id = per.Id, Name = per.Name + " " + per.Surname, Position = pos.Position }
            //                )
            //    //.Where((p) =>
            //    //{
            //    //    if (p.Position == EPosition.Accompanist.ToString() ||
            //    //    p.Position == EPosition.Actor.ToString() ||
            //    //    p.Position == EPosition.Choirmaster.ToString() ||
            //    //    p.Position == EPosition.Conductor.ToString() ||
            //    //    p.Position == EPosition.LightingDesigner.ToString() ||
            //    //    p.Position == EPosition.SoundEngineer.ToString() ||
            //    //    p.Position == EPosition.Producer.ToString()
            //    //    )return true;
            //    //return false;
            //    //})
            //    .ToList();

            var fullEmployee = repositoryPeople.GetPositions()
                            .Where((p) =>
                                     p.Name == EPosition.Accompanist.ToString()
                                     || p.Name == EPosition.Actor.ToString()
                                     || p.Name == EPosition.Choirmaster.ToString()
                                     || p.Name == EPosition.Conductor.ToString()
                                     || p.Name == EPosition.LightingDesigner.ToString()
                                     || p.Name == EPosition.SoundEngineer.ToString()
                                     || p.Name == EPosition.Producer.ToString()
                            )
                            .Include(post => post.People)
                            //.Select(p => new Tuple<string, string>(p.Name, p.People.ToString())
                            .ToList();


            List<ModelForEvent> res = new List<ModelForEvent>();
            foreach (var item in fullEmployee)
            {
                for (int i = 0; i < item.People.Count; i++)
                {
                    res.Add(new ModelForEvent
                    {
                        Id = item.Id,
                        Position = item.Name,
                        Name = ((List<Person>)item.People)[i].Name + " " + ((List<Person>)item.People)[i].Surname
                    });

                }
            }

            ViewBag.Employees = res;//fullEmployee.Select(e => e.Name);

            ViewBag.TitleEvent = repositoryEvent.GetEvents().Select(e => e.Title).Distinct();
            ViewBag.Location = repositoryEvent.GetEvents().Select(e => e.Location).Distinct();


            return View(dbEntry);
        }

        [HttpPost]
        public ActionResult Edit(Event dbEntry)
        {
            bool serverValidation = false;
            if ( dbEntry != null
                && !string.IsNullOrWhiteSpace(dbEntry.Title)
                && !string.IsNullOrWhiteSpace(dbEntry.Location)
                && validData(dbEntry.Start, true)
                && (dbEntry.Duration != null && dbEntry.Duration > 0 && dbEntry.Duration < 1440)
                && validEmployees(dbEntry.Producer, EPosition.Producer, true)
                && validEmployees(dbEntry.Conductor, EPosition.Conductor)
                && validEmployees(dbEntry.Choirmaster, EPosition.Choirmaster)
                && validEmployees(dbEntry.Accompanist, EPosition.Accompanist)
                && validEmployees(dbEntry.LightingDesigner, EPosition.LightingDesigner)
                && validEmployees(dbEntry.SoundEngineer, EPosition.SoundEngineer)
                ) serverValidation = true;

            if (serverValidation && ModelState.IsValid)
            {
                //ScheduleRepository repository = new EfScheduleRepository();
                repositoryEvent.Update(dbEntry);
                TempData["Message"] = string.Format($"The change was saved for the event \"{dbEntry.Title}\"");
                IEnumerable<Event> schedule = repositoryEvent.GetEvents();
                return RedirectToAction("Schedule");
            }
            else
            {
                //ViewBag.Employees = repositoryPeople.GetEmployee()
                //            .Where((p) =>
                //            {
                //                if (p.PositionId == (int)EPosition.Accompanist ||
                //                     p.PositionId == (int)EPosition.Actor ||
                //                     p.PositionId == (int)EPosition.Choirmaster ||
                //                     p.PositionId == (int)EPosition.Conductor ||
                //                     p.PositionId == (int)EPosition.LightingDesigner ||
                //                     p.PositionId == (int)EPosition.SoundEngineer ||
                //                     p.PositionId == (int)EPosition.Producer
                //                    ) return true;
                //                return false;
                //            })
                //.Join(repositoryPeople.GetPositions(),
                //        pos => pos.PositionId,
                //        title => title.Id,
                //        (pos, title) => new { PeopleId = pos.PersonId, Position = title.Name }
                //        )
                //       .Join(repositoryPeople.GetPeople(),
                //            pos => pos.PeopleId,
                //            per => per.Id,
                //            (pos, per) => new ModelForEvent
                //            { Id = per.Id, Name = per.Name + " " + per.Surname, Position = pos.Position }
                //            )
                //.ToList();
                var fullEmployee = repositoryPeople.GetPositions()
                            .Where((p) =>
                                     p.Name == EPosition.Accompanist.ToString()
                                     || p.Name == EPosition.Actor.ToString()
                                     || p.Name == EPosition.Choirmaster.ToString()
                                     || p.Name == EPosition.Conductor.ToString()
                                     || p.Name == EPosition.LightingDesigner.ToString()
                                     || p.Name == EPosition.SoundEngineer.ToString()
                                     || p.Name == EPosition.Producer.ToString()
                            )
                            .Include(post => post.People)
                            //.Select(p => new Tuple<string, string>(p.Name, p.People.ToString())
                            .ToList();


                List<ModelForEvent> res = new List<ModelForEvent>();
                foreach (var item in fullEmployee)
                {
                    for (int i = 0; i < item.People.Count; i++)
                    {
                        res.Add(new ModelForEvent
                        {
                            Id = item.Id,
                            Position = item.Name,
                            Name = ((List<Person>)item.People)[i].Name + " " + ((List<Person>)item.People)[i].Surname
                        });

                    }
                }

                ViewBag.Employees = res;//fullEmployee.Select(e => e.Name);


                ViewBag.TitleEvent = repositoryEvent.GetEvents().Select(e => e.Title).Distinct();
                ViewBag.Location = repositoryEvent.GetEvents().Select(e => e.Location).Distinct();


                return View(dbEntry);
            }

            bool validEmployees(string value, EPosition pos, bool required = false)
            {
                if (string.IsNullOrEmpty(value))// check empty
                {
                    if (required) return false;
                    else return true;
                }
                int val;
                if (int.TryParse(value, out val))// check numbers
                {
                    var person = repositoryPeople.GetPerson(val);
                    List<Position> item = person.Positions.ToList();
                    for (int i = 0; i < item.Count; i++)
                    {
                        if (item[i].Name == pos.ToString()) return true;
                    }
                    return false;
                }
                else // check symbol
                {
                    Regex r = new Regex(@"[\[\]{}\d!#\^$.,|\/\\?*@+()]");
                    Match m = r.Match(value);
                    if (m == null || m.Length > 0)
                    {
                        ModelState.AddModelError("", "Field can't have \"[[]{}d!#\\^$|\\/\\?*@+()]\" symbols.");
                        return false;
                    }
                    else return true;
                }
            }



            bool validData(DateTime value, bool required = false)
            {
                if (value == null)// check empty
                {
                    if (required)
                    {
                        ModelState.AddModelError("", "Values Date and Time required.");
                        return false;
                    }
                    else return true;
                }
                if (dbEntry.Start > DateTime.Now && dbEntry.Start < DateTime.MaxValue)// check numbers
                {
                    
                    Regex r = new Regex(@"[\[\]{}!#\^$|\/\\?*@+()]");
                    Match m = r.Match(value.ToString());
                    if (m == null || m.Length > 0)
                    {
                        ModelState.AddModelError("", "Field can't have \"[[]{}d!#\\^$|\\/\\?*@+()]\" symbols.");
                        return false;
                    }
                    return true;
                }
                ModelState.AddModelError("", $"Date and time mast be betwin now and {DateTime.MaxValue}.");
                return false;
            }
        }


        [HttpGet]
        public ActionResult Details(int eventId)
        {
            if (eventId == 0) return RedirectToAction("Schedule");
            var ev = repositoryEvent.GetEvents().FirstOrDefault(e => e.Id == eventId);
            return View(ev);
        }

        [HttpGet]
        public ActionResult Delete(int eventId)
        {
            if(eventId == 0) return RedirectToAction("Schedule");
            var ev = repositoryEvent.GetEvents().FirstOrDefault(e => e.Id == eventId);
            if (ev != null) repositoryEvent.Delete(eventId);
            return RedirectToAction("Schedule");
        }
    }
}