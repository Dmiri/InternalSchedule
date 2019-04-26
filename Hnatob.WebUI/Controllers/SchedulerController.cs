// System
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

//using System.Web.Http;
// Embed

// Project
using Hnatob.Domain.Abstract;
using Hnatob.Domain.Concrete;
using Hnatob.WebUI.Models;
using Hnatob.Domain.Helper;
using System.Net;
using System.Diagnostics;

namespace Hnatob.WebUI.Controllers
{
    //[Authorize]
    public class SchedulerController : Controller
    {
        private readonly IScheduleRepository repositoryEvent;
        private readonly IPeopleRepository repositoryPeople;
        private readonly IResponsiblesRepository repositoryResponsibles;
        //private readonly ICommentToServiceRepository repositoryCommentToService;

        public SchedulerController(
            IScheduleRepository repositoryEvent, 
            IPeopleRepository repositoryPeople, 
            IResponsiblesRepository repositoryResponsibles
            //ICommentToServiceRepository repositoryCommentToService
            ) : base()
        {
            this.repositoryEvent = repositoryEvent ?? throw new ArgumentNullException("No argument - Event");
            this.repositoryPeople = repositoryPeople ?? throw new ArgumentNullException("No argument - People");
            this.repositoryResponsibles = repositoryResponsibles ?? throw new ArgumentNullException("No argument - Responsibles");
            //this.repositoryCommentToService = repositoryCommentToService ?? throw new ArgumentNullException("No argument - Services");
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
            {
                var entry = repositoryEvent.GetEvents()
                    .Where(o => o.Id == eventId)
                    .Include("Responsibles.Person")
                    .Include("Responsibles.Position")
                    //.Include(c => c.CommentsToServices)
                    //.ThenInclude()
                    .ToList();

                dbEntry = entry[0];

                foreach (var item in dbEntry.Responsibles)
                {
                    //if (string.IsNullOrEmpty(item.PersonName))
                    if (item.PersonId != null)
                        item.PersonName = item.Person?.Name + " " + item.Person?.Surname;
                }
            }
            else dbEntry = new Event();

            ViewBag.Location = repositoryEvent.GetEvents().Select(e => e.Location).Where(e => e != "" && e != null).Distinct().ToList();
            ViewBag.TypeEvent = repositoryEvent.GetEvents().Select(e => e.EventType).Distinct().Where(e => e != "" && e != null).ToList();
            ViewBag.TitleEvent = repositoryEvent.GetEvents().Select(e => e.Title).Distinct().Where(e => e != "" && e != null).ToList();
            ViewBag.Positions = repositoryPeople.GetPositions().ToList();
            //ViewBag.CommentsToService = //repositoryCommentToService.GetCommentToServices().Select(e => e.ServiceName).Distinct().Where(e => e != "" && e != null).ToList();
            using (var context = new EfDbContext())
            {
                ViewBag.Services = context.Services.Select(e => e.Name).Where(e => e != "" && e != null).ToList();
            }


            return View(dbEntry);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(/*Event dbEntry*/)
        {
            if (Request.Form.Count <= 0)// .Content.IsMimeMultipartContent())
            {
                throw new System.Web.Http.HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }
            //string root = HttpContext.Current.Server.MapPath("~/App_Data");
            //var provider = new System.Net.Http.MultipartFormDataStreamProvider(root);
            Event dbEntry;// = new Event();
            foreach (var key in Request.Form.AllKeys)
            {
                foreach (var val in Request.Form.GetValues(key))
                {
                    Trace.WriteLine(string.Format("{0}: {1}", key, val));
                }
            }

            int id;
            int.TryParse(Request.Form.GetValues("Id").FirstOrDefault(), out id);

            var datTimeMass = Request.Form.GetValues("Start").FirstOrDefault().Split(' ');
            var data = datTimeMass[0].Split('.');
            var time = datTimeMass[1].Split(':');
            int day;
            int mount;
            int year;
            int hour;
            int minutes;
            int second = 0;
            int.TryParse(data[0], out mount);
            int.TryParse(data[1], out day);
            int.TryParse(data[2], out year);
            int.TryParse(time[0], out hour);
            int.TryParse(time[1], out minutes);

            var start = new DateTime(year, mount, day, hour, minutes, second);
            int duration;
            int.TryParse(Request.Form.GetValues("Duration").FirstOrDefault(), out duration); ;

            dbEntry = new Event
            {
                Id = id,
                Access = Request.Form.GetValues("Access").FirstOrDefault(),
                Location = Request.Form.GetValues("Location").FirstOrDefault(),
                EventType = Request.Form.GetValues("EventType").FirstOrDefault(),
                Title = Request.Form.GetValues("Title").FirstOrDefault(),
                Start = start,
                Duration = duration,
                Description = Request.Form.GetValues("Description").FirstOrDefault(),
                //Responsibles = { },
                //CommentsToServices = { }
            };

            // Get Responsibles
            var responsibleIds = Request.Form.GetValues("responsibleId");
            var responsibleNames = Request.Form.GetValues("ResponsibleName");
            var hidenParamsPost = Request.Form.GetValues("hidenParamPost");
            var hidenParamsName = Request.Form.GetValues("hidenParamName");
            var comments = Request.Form.GetValues("Comment");
            for (int i = 0; i < responsibleIds?.Length; i++)
            {
                int responsibleId;
                if (string.IsNullOrEmpty(responsibleIds[i]) || string.IsNullOrWhiteSpace(responsibleIds[i]))
                    responsibleId = 0;
                else int.TryParse(responsibleIds[i], out responsibleId);

                int positionId;
                if (string.IsNullOrEmpty(hidenParamsPost[i]) || string.IsNullOrWhiteSpace(hidenParamsPost[i]))
                    positionId = 0;
                else int.TryParse(hidenParamsPost[i], out positionId);

                int personId;
                if (string.IsNullOrEmpty(hidenParamsName[i]) || string.IsNullOrWhiteSpace(hidenParamsName[i]))
                    personId = 0;
                else int.TryParse(hidenParamsName[i], out personId);
                Responsible responsible = new Responsible
                {
                    Id = responsibleId,
                    EventId = id,
                    Comment = comments[i],
                    PositionId = positionId,
                    PersonName = responsibleNames[i],
                };
                if (personId > 0) responsible.PersonId = personId;
                //else responsible.PersonId = null;
                dbEntry.Responsibles.Add(responsible);
            }



            // Get CommentToService
            var serviceNames = Request.Form.GetValues("serviceName");
            var serviceComments = Request.Form.GetValues("serviceComment");

            for (int i = 0; i < serviceNames?.Length; i++)
            {
                var commentToService = new CommentToService();
                if (!string.IsNullOrEmpty(serviceNames[i]) || !string.IsNullOrWhiteSpace(serviceNames[i]))
                    //int.TryParse(serviceNames[i], out responsibleId);
                    commentToService.ServiceName = serviceNames[i];

                if (!string.IsNullOrEmpty(serviceComments[i]) || !string.IsNullOrWhiteSpace(serviceComments[i]))
                    commentToService.Comment = serviceComments[i];

                commentToService.EventId = dbEntry.Id;
                
                dbEntry.CommentsToServices.Add(commentToService);
            }




            bool serverValidation = false;
            if (dbEntry != null
                && !string.IsNullOrWhiteSpace(dbEntry.Title)
                && !string.IsNullOrWhiteSpace(dbEntry.Location)
                && validData(dbEntry.Start, true)
                && (dbEntry.Duration > 0 && dbEntry.Duration < 1440)
                //&& validEmployees(dbEntry.Responsible, true)
                && validSchedule(dbEntry)
                ) serverValidation = true;

            if (serverValidation && ModelState.IsValid)
            {
                try
                {
                    repositoryEvent.Update(dbEntry);
                }
                catch (Exception)
                {
                    return new HttpNotFoundResult();
                }

                TempData["Message"] = string.Format($"The change was saved for the event \"{dbEntry.Title}\"");
                IEnumerable<Event> schedule = repositoryEvent.GetEvents();
                return Redirect("Schedule");
            }
            else
            {
                ViewBag.Location = repositoryEvent.GetEvents().Select(e => e.Location).Distinct();
                ViewBag.TypeEvent = repositoryEvent.GetEvents().Select(e => e.EventType).Distinct().Where(e => e != "");
                ViewBag.TitleEvent = repositoryEvent.GetEvents().Select(e => e.Title).Distinct();
                ViewBag.Positions = repositoryPeople.GetPositions().ToList();
                using (var context = new EfDbContext())
                {
                    ViewBag.Services = context.Services.Select(e => e.Name).Where(e => e != "" && e != null).ToList();
                }
                return View(dbEntry);
            }

            //bool validEmployees(string value, EPosition pos, bool required = false)
            //{
            //    if (string.IsNullOrEmpty(value))// check empty
            //    {
            //        if (required) return false;
            //        else return true;
            //    }
            //    int val;
            //    if (int.TryParse(value, out val))// check numbers
            //    {
            //        var person = repositoryPeople.GetPerson(val);
            //        List<Position> item = person.Positions.ToList();
            //        for (int i = 0; i < item.Count; i++)
            //        {
            //            if (item[i].Name == pos.ToString()) return true;
            //        }
            //        return false;
            //    }
            //    else // check symbol
            //    {
            //        Regex r = new Regex(@"[\[\]{}\d!#\^$.,|\/\\?*@+()]");
            //        Match m = r.Match(value);
            //        if (m == null || m.Length > 0)
            //        {
            //            ModelState.AddModelError("", "Field can't have \"[[]{}d!#\\^$|\\/\\?*@+()]\" symbols.");
            //            return false;
            //        }
            //        else return true;
            //    }
            //}




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


            bool validSchedule(Event entry)
            {
                DateTime dateStart = dbEntry.Start.AddDays(-1);
                DateTime dateEnd = dbEntry.Start.AddDays(1);
                var fullEmployee = repositoryEvent.GetEvents()
                    .Where(p => p.Location == entry.Location
                    && p.Id != entry.Id
                    && p.Start > dateStart
                    && p.Start < dateEnd
                    )
                    .OrderBy(p => p.Start)
                    .ToList();
                foreach (var item in fullEmployee)
                {
                    if (item.Start.AddMinutes(item.Duration) < entry.Start) continue;
                    else if (item.Start <= entry.Start
                        || item.Start <= entry.Start.AddMinutes(entry.Duration))
                    {
                        ModelState.AddModelError("", "Here was a time intersection with another event.");
                        return false;
                    }
                }
                return true;
            }

        }

        //[HttpPost]
        //public ActionResult UploadModelForEdit(ICollection<HttpPostAttribute> httpPostAttributes)
        //{
        //    foreach (var item in httpPostAttributes)
        //    {

        //    }
        //    return null;
        //}

        [HttpPost]
        public ActionResult ResponsibleForEdit()
        {
            ViewBag.Positions = repositoryPeople.GetPositions().ToList();
            return PartialView(
                    new List<Responsible> {
                        new Responsible()
                    });
        }

        [HttpPost]
        public ActionResult GetResponsiblePerson(int position)
        {

            if (position > 0) {
                var employee = repositoryPeople.GetPositions()
                                .FirstOrDefault((p) =>
                                         p.Id == position
                                 )
                                .People
                                .ToList()
                                .Select((p) => new ModelForEvent
                                {
                                    Id = p.Id,
                                    Position = p.Name,
                                    Name = p.Name + " " + p.Surname
                                })
                                .ToList();
                if(employee != null)
                    return PartialView(employee);
            }
            return PartialView(new List<ModelForEvent>());
        }

        
        [HttpPost]
        public ActionResult CommentsToServicesForEdit(string serviceName)
        {
            //;
            using (var context = new EfDbContext())
            {
                ViewBag.Services = context.Services.Select(e => e.Name).Where(e => e != "" && e != null).ToList();
            }
            var entry = new CommentToService { ServiceName = serviceName };
            //if (position > 0)
            //{
            //    var employee = repositoryPeople.GetPositions()
            //                    .FirstOrDefault((p) =>
            //                             p.Id == position
            //                     )
            //                    .People
            //                    .ToList()
            //                    .Select((p) => new ModelForEvent
            //                    {
            //                        Id = p.Id,
            //                        Position = p.Name,
            //                        Name = p.Name + " " + p.Surname
            //                    })
            //                    .ToList();
            //    if (employee != null)
            //        return PartialView(employee);
            //}
            return PartialView(
                    new List<CommentToService> {
                        entry
                    });
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