using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Routing;
using System.Web.Mvc;

// Project
using Hnatob.Domain.Abstract;
using Hnatob.Domain.Concrete;
using Hnatob.Domain.Helper;

namespace Hnatob.WebUI.Controllers.ApiControllers
{
    public class EventsAsyncController : ApiController
    {
        private EfDbContext db = new EfDbContext();
        private readonly IScheduleRepository repositoryEvent = new EfScheduleRepository();

        // GET: api/EventsAsync
        public IQueryable<Event> GetEvents()
        {
            return db.Events;
        }

        // GET: api/EventsAsync/5
        [ResponseType(typeof(Event))]
        public async Task<IHttpActionResult> GetEvent(int id)
        {
            Event @event = await db.Events.FindAsync(id);
            if (@event == null)
            {
                return NotFound();
            }

            return Ok(@event);
        }

        // PUT: api/EventsAsync/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutEvent(int id, Event @event)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != @event.Id)
            {
                return BadRequest();
            }

            db.Entry(@event).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        [ValidateAntiForgeryToken]
        public async Task</*IHttpActionResult*/HttpResponseMessage> PostEvent()
        {

            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            string root = HttpContext.Current.Server.MapPath("~/App_Data");
            var provider = new MultipartFormDataStreamProvider(root);
            Event dbEntry = new Event();
            try
            {
                // Read the form data.
                await Request.Content.ReadAsMultipartAsync(provider);
                // Show all the key-value pairs.
                foreach (var key in provider.FormData.AllKeys)
                {
                    foreach (var val in provider.FormData.GetValues(key))
                    {
                        Trace.WriteLine(string.Format("{0}: {1}", key, val));
                    }
                }

                int id;
                int.TryParse(provider.FormData.GetValues("Id").FirstOrDefault(), out id); ;
                var access = provider.FormData.GetValues("Access").FirstOrDefault();
                var location = provider.FormData.GetValues("Location").FirstOrDefault();
                var eventType = provider.FormData.GetValues("EventType").FirstOrDefault();
                var title = provider.FormData.GetValues("Title").FirstOrDefault();
                var description = provider.FormData.GetValues("Description").FirstOrDefault();

                var datTimeMass = provider.FormData.GetValues("Start").FirstOrDefault().Split(' ');
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
                int.TryParse(provider.FormData.GetValues("Duration").FirstOrDefault(), out duration); ;


                //dbEntry = new Event {
                //    Id = id,
                //    Access = access,
                //    Location = location,
                //    EventType = eventType,
                //    Title = title,
                //    Start = start,
                //    Duration = duration,
                //    Description = description,
                //};
                dbEntry.Id = id;
                dbEntry.Access = access;
                dbEntry.Location = location;
                dbEntry.EventType = eventType;
                dbEntry.Title = title;
                dbEntry.Start = start;
                dbEntry.Duration = duration;
                dbEntry.Description = description;

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
                    //repositoryEvent.Update(dbEntry);
                    //TempData["Message"] = string.Format($"The change was saved for the event \"{dbEntry.Title}\"");
                    //IEnumerable<Event> schedule = repositoryEvent.GetEvents();
                    //return Redirect(Request.RequestUri); //"Scheduler/Schedule");
                    //return RedirectToRoute(Request.RequestUri);
                    //return Request.CreateResponse(Request.RequestUri);
                    //var response = Request.CreateResponse(HttpStatusCode.Created);
                    //Uri uri = new Uri(Request.RequestUri.Scheme + "://" + Request.RequestUri.Authority + "/Scheduler/Schedule");
                    //response.Headers.Location = uri;
                    //var par = Request.GetRouteData();
                    //return response;
                    //response.Headers.Location.LocalPath = "Scheduler/Schedule";
                    //return RedirectToRoute("Default", HttpStatusCode.Created);
                    /*RedirectToRoute(new {
                        contrcontroller = "Admin",
                        action = "Index",
                        eventId = dbEntry.Id
                    });*/
                    //return CreatedAtRoute( "Edit", dbEntry.Id };
                    //return Request.CreateResponse(HttpStatusCode.Created);// HttpStatusCode.Created;
                    //Url.Action("Schedule", "Scheduler");
                    

                    var response = Request.CreateResponse(HttpStatusCode.Created);
                    response.Headers.Location = new Uri("/Scheduler/Schedule");
                    return response;//Task.FromResult(response);
                }
                else
                {
                    //ViewBag.Location = repositoryEvent.GetEvents().Select(e => e.Location).Distinct();
                    //ViewBag.TypeEvent = repositoryEvent.GetEvents().Select(e => e.EventType).Distinct().Where(e => e != "");
                    //ViewBag.TitleEvent = repositoryEvent.GetEvents().Select(e => e.Title).Distinct();
                    //ViewBag.Positions = repositoryPeople.GetPositions().ToList();
                    //return Redirect("Schedule"); //View(dbEntry);
                    //return Request.CreateResponse<Event>(HttpStatusCode.InternalServerError, dbEntry);
                    //return BadRequest(ModelState);
                    return Request.CreateResponse(HttpStatusCode.Conflict);// BadRequest(ModelState);
                }

                //return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (System.Exception e)
            {
                //return Request.CreateResponse<Event>(HttpStatusCode.InternalServerError, e, );
                //return BadRequest(e.Message);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
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


        // DELETE: api/EventsAsync/5
        [ResponseType(typeof(Event))]
        public async Task<IHttpActionResult> DeleteEvent(int id)
        {
            Event @event = await db.Events.FindAsync(id);
            if (@event == null)
            {
                return NotFound();
            }

            db.Events.Remove(@event);
            await db.SaveChangesAsync();

            return Ok(@event);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EventExists(int id)
        {
            return db.Events.Count(e => e.Id == id) > 0;
        }
    }
}