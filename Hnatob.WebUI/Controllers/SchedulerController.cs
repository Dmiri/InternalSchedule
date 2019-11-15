// System
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Net;
using System.Diagnostics;

//using System.Web.Http;
// Embed

// Project
using Hnatob.DataAccessLayer.Abstract;
using Hnatob.DataAccessLayer.Concrete;
using Hnatob.WebUI.Models;
using Hnatob.Domain.Models;
using Hnatob.Domain.Helper;
using Hnatob.DataAccessLayer.Context;
using Hnatob.WebUI.Models.Pagination;
using Hnatob.WebUI.Models.Helpers;

namespace Hnatob.WebUI.Controllers
{
    [Authorize]
    public class SchedulerController : Controller
    {
        private readonly IScheduleRepository repositoryEvent;
        private readonly IUserRepository repositoryPeople;
        private readonly IResponsiblesRepository repositoryResponsibles;
        //private readonly ICommentToServiceRepository repositoryCommentToService;
        //private int pageSize = 4;

        public SchedulerController(
            IScheduleRepository repositoryEvent, 
            IUserRepository repositoryPeople, 
            IResponsiblesRepository repositoryResponsibles
            //ICommentToServiceRepository repositoryCommentToService
            ) : base()
        {
            this.repositoryEvent = repositoryEvent ?? throw new ArgumentNullException("No argument - Event");
            this.repositoryPeople = repositoryPeople ?? throw new ArgumentNullException("No argument - People");
            this.repositoryResponsibles = repositoryResponsibles ?? throw new ArgumentNullException("No argument - Responsibles");
            //this.repositoryCommentToService = repositoryCommentToService ?? throw new ArgumentNullException("No argument - Services");
        }


        //TODO: Filter & sort
        // GET: Scheduler
        [AllowAnonymous]
        public ActionResult Schedule(int page = 0, string pageSize = "week", string access = "all", string type = "", string name = "")
        {
            ScheduleListPaginationViewModel model;
            //int pageSizeForPageInfo = 0;
            // Paging
            // -----------------------------------------
            DateTime startSelection = DateTime.Today;
            DateTime endSelection;

            var dateThis = DateTime.Today;

            var dateLastEvent = repositoryEvent.GetEvents()
                .OrderByDescending(e => e.Start).ThenBy(e => e.Location).ThenBy(e => e.Title)
                .FirstOrDefault()
                .Start;

            var dateTimeFirstEvent = repositoryEvent.GetEvents()
                .OrderBy(e => e.Start).ThenBy(e => e.Location).ThenBy(e => e.Title)
                .FirstOrDefault()
                .Start;

            DateTime dateFirstEvent = new DateTime(dateTimeFirstEvent.Year, dateTimeFirstEvent.Month, dateTimeFirstEvent.Day);
            

            int defaultPage;
            int totalPage;

            string patternText = @"[\w\s-.]*";

            switch (pageSize.ToLower())
            {
                case "day":
                    defaultPage = (int)Math.Ceiling((dateThis - dateFirstEvent).TotalDays);
                    totalPage = (int)Math.Ceiling((dateLastEvent - dateFirstEvent).TotalDays);
                    if (page < 1) page = defaultPage;
                    startSelection = dateFirstEvent.AddDays(page-1);
                    endSelection = startSelection.AddDays(1);
                    break;
                case "week":
                    defaultPage = (int)Math.Ceiling((dateThis - dateFirstEvent).TotalDays / 7);
                    totalPage = (int)Math.Ceiling((dateLastEvent - dateFirstEvent).TotalDays / 7);
                    if (page < 1) page = defaultPage;
                    startSelection = dateFirstEvent
                        .AddDays(7*(page-1) - ((int)dateFirstEvent.DayOfWeek - 1));
                    endSelection = startSelection.AddDays(7);
                    break;
                case "month":
                    defaultPage = (int)Math.Ceiling((dateThis - dateFirstEvent).TotalDays / 30);
                    totalPage = (int)Math.Ceiling((dateLastEvent - dateFirstEvent).TotalDays / 30);
                    if (page < 1) page = defaultPage;
                    startSelection = dateFirstEvent
                        .AddDays(-1 * (dateFirstEvent.Day - 1))
                        .AddMonths(page-1);
                    endSelection = startSelection.AddMonths(1);
                    break;
                default:
                    defaultPage = (int)Math.Ceiling((dateThis - dateFirstEvent).TotalDays / 7);
                    totalPage = (int)Math.Ceiling((dateLastEvent - dateFirstEvent).TotalDays / 7);
                    startSelection = DateTime.Today;
                    endSelection = startSelection.AddDays(7);
                    break;
            }
            if (page == 0) page = defaultPage;

            // Filter
            // -----------------------------------------
            Func<Event, bool> filterAccess = e => {
                bool result = true;
                if ( Regex.IsMatch(access, patternText, RegexOptions.IgnoreCase) 
                &&( string.IsNullOrEmpty(access)
                    || access.ToLower() == "all" 
                    || e.Access.ToLower() == access)
                    )
                    result = result && true;
                else result = result && false;

                if (Regex.IsMatch(type, patternText, RegexOptions.IgnoreCase)
                && (string.IsNullOrEmpty(type)
                    || type.ToLower() == "all" 
                    || e.EventType.ToLower() == type)
                    )
                    result = result && true;
                else result = result && false;

                if (Regex.IsMatch(name, patternText, RegexOptions.IgnoreCase) 
                && (string.IsNullOrEmpty(name) || e.Title.ToLower().Contains(name.ToLower())))
                    result = result && true;
                else result = result && false;

                if (e.Start >= startSelection && e.Start < endSelection)
                    result = result && true;
                else result = result && false;

                return result;
            };

            // return all schedule
            if (User.IsInRole("editor") || User.IsInRole("employee"))
            {
                model = new ScheduleListPaginationViewModel
                {
                    Schedule = repositoryEvent.GetEvents()
                        .OrderBy(e => e.Start).ThenBy(e => e.Location).ThenBy(e => e.Title)
                        .Where(filterAccess)
                        .ToList(),
                    PagingInfo = new PagingInfo
                    {
                        DefaulpPage = defaultPage,
                        CurrentPage = page,
                        //ItemsPerPage = pageSizeForPageInfo,
                        TotalItems = repositoryEvent.GetEvents().Count(),
                        TotalPages = totalPage,
                    }
                };
            }

            // return only public schedule
            else
            {
                model = new ScheduleListPaginationViewModel
                {
                    Schedule = repositoryEvent.GetEvents()
                        .Where(l => l.Access == Access.Public.ToString())
                        .OrderBy(e => e.Start).ThenBy(e => e.Location).ThenBy(e => e.Title)
                        .Where(filterAccess)
                        .ToList(),
                    PagingInfo = new PagingInfo
                    {
                        DefaulpPage = defaultPage,
                        CurrentPage = page,
                        //ItemsPerPage = pageSizeForPageInfo,
                        TotalItems = repositoryEvent.GetEvents().Where(l => l.Access == Access.Public.ToString()).Count(),
                        TotalPages = totalPage,
                    }
                };
            }


            ViewBag.Location = repositoryEvent.GetEvents().Select(e => e.Location).Where(e => e != "" && e != null).Distinct().ToList();
            ViewBag.TypeEvent = repositoryEvent.GetEvents().Select(e => e.EventType).Distinct().Where(e => e != "" && e != null).ToList();
            ViewBag.TitleEvent = repositoryEvent.GetEvents().Select(e => e.Title).Distinct().Where(e => e != "" && e != null).ToList();

            ViewBag.Start = startSelection.ToShortDateString();
            ViewBag.End = endSelection.ToShortDateString();

            return View("Schedule", model);
        }



        [Authorize(Roles = "editor")]
        public ActionResult Edit(int? eventId)
        {
            Event dbEntry;
            if (eventId != 0 && eventId != null)
            {
                var entry = repositoryEvent.GetEvents()
                    .Where(o => o.Id == eventId)
                    .Include("Responsibles.Person")
                    .Include("Responsibles.Position")
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
            using (var context = new ApplicationDbContext())
            {
                ViewBag.Services = context.Services.Select(e => e.Name).Where(e => e != "" && e != null).ToList();
            }
            return View(dbEntry);
        }



        [HttpPost]
        [Authorize(Roles = "editor")]
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

            //-------------------------------------------------------------------
            int id;
            int.TryParse(Request.Form.GetValues("Id").FirstOrDefault(), out id);

            //-------------------------------------------------------------------
            var datTimeMass = Request.Form.GetValues("Start");
            var start = DateTime.Parse(datTimeMass[0]); //new DateTime()
            //-------------------------------------------------------------------
            int duration;
            int.TryParse(Request.Form.GetValues("Duration").FirstOrDefault(), out duration);

            //-------------------------------------------------------------------
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

            //-------------------------------------------------------------------
            bool serverValidation = false;
            if (dbEntry != null
                && !string.IsNullOrWhiteSpace(dbEntry.Title)
                && !string.IsNullOrWhiteSpace(dbEntry.Location)
                && validData(dbEntry.Start, true)
                //&& (dbEntry.Duration > 0 && dbEntry.Duration < 1440)
                //&& validEmployees(dbEntry.Responsible, true)
                && validationCreateSchedule(dbEntry)
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
                //IEnumerable<Event> schedule = repositoryEvent.GetEvents();
                return Redirect("Schedule");
            }
            else
            {
                ViewBag.Location = repositoryEvent.GetEvents().Select(e => e.Location).Distinct();
                ViewBag.TypeEvent = repositoryEvent.GetEvents().Select(e => e.EventType).Distinct().Where(e => e != "");
                ViewBag.TitleEvent = repositoryEvent.GetEvents().Select(e => e.Title).Distinct();
                ViewBag.Positions = repositoryPeople.GetPositions().ToList();
                using (var context = new ApplicationDbContext())
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




        }

        [HttpPost]
        [Authorize(Roles = "editor")]
        public ActionResult ResponsibleForEdit(int position = 0)
        {
            var posts =  repositoryPeople.GetPositions().ToList();
            ViewBag.Positions = posts;
            var entry = new Responsible();
            entry.PositionId = position;
            entry.Position = posts.FirstOrDefault(p => p.Id == position);
            return PartialView(
                    new List<Responsible> {
                        entry
                    });
        }

        [HttpPost]
        [Authorize(Roles = "editor")]
        public ActionResult GetResponsiblePerson(int position)
        {
            if (position > 0)
            {
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
                if (employee != null)
                    return PartialView(employee);
            }
            return PartialView(new List<ModelForEvent>());
        }


        [HttpPost]
        [Authorize(Roles = "editor")]
        public ActionResult CommentsToServicesForEdit(string serviceName)
        {
            //;
            using (var context = new ApplicationDbContext())
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
        [Authorize(Roles = "employee")]
        public ActionResult Details(int eventId)
        {
            if (eventId == 0) return RedirectToAction("Schedule");
            var ev = repositoryEvent.GetEvents().FirstOrDefault(e => e.Id == eventId);
            return View(ev);
        }


        [HttpPost]
        [Authorize(Roles = "editor")]
        public ActionResult Delete(int id)
        {
            var obj = repositoryEvent.GetEvent(id);
            var model = new RoutViewModels();
            if (obj != null)
            {
                model.Controller = "Scheduler";
                model.Action = "SaveDelete";
                model.Id = id.ToString();
                model.Title = "Delete";
                model.Data = new HtmlString(
                    "Are you sure want to delete this event:</br>"
                    + obj.Start + " - " + obj.Title + "?"
                    );
            }
            else
            {
                model.Controller = "Scheduler";
                model.Action = "Schedule";
                model.Id = id.ToString();
                model.Title = "Delete";
                model.Data = new HtmlString("This event not found");
            }
            return PartialView("ModalConfirm", model);
        }

        [HttpPost]
        [Authorize(Roles = "editor")]
        public ActionResult SaveDelete(RoutViewModels model)
        {
            int id;
            int.TryParse(model.Id, out id);
            if (id == 0) return RedirectToAction("Schedule");
            var ev = repositoryEvent.GetEvents().FirstOrDefault(e => e.Id == id);
            if (ev != null) repositoryEvent.Delete(id);
            return RedirectToAction("Schedule");
        }


        // Server validation
        //=========================================================================================
        private bool validationCreateSchedule(Event model)
        {
            bool result = true;
            var temp = string.IsNullOrEmpty(Regex.Match(model.EventType, ValidatorsRegex.patternText).Value);
            //model.Access
            if (model.Access.ToLower() != Access.Private.ToString().ToLower()
                && model.Access.ToLower() != Access.Public.ToString().ToLower())
                result = false;

            //model.Start;
            if (!validSchedule(model)) result = false;

            //model.EventType;
            if (model.EventType == null
                || string.IsNullOrEmpty(Regex.Match(model.EventType, ValidatorsRegex.patternText).Value)
                || model.EventType.Length > ValidatorsRegex.maxMediumLenght)
            {
                ModelState.AddModelError("EventType", "Field \"EventType\" must include only letter, number and symbols: \"-+_.,;:?()\"");
                result = result && false;
            }

            //model.Title;
            if (model.Title == null
                || string.IsNullOrEmpty(Regex.Match(model.Title, ValidatorsRegex.patternText).Value)
                || model.Title.Length > ValidatorsRegex.maxShortLenght)
            {
                ModelState.AddModelError("Title", "Field \"Title\" must include only letter, number and symbols: \"-+_.,;:?()\"");
                result = result && false;
            }
            
            //model.Location;
            if (model.Location == null
                || string.IsNullOrEmpty(Regex.Match(model.Location, ValidatorsRegex.patternText).Value)
                || model.Location.Length > ValidatorsRegex.maxShortLenght)
            {
                ModelState.AddModelError("Location", "Field \"Location\" must include only letter, number and symbols: \"-+_.,;:?()\"");
                result = result && false;
            }

            //model.Description;
            if (model.Description != null
                && string.IsNullOrEmpty(Regex.Match(model.Description, ValidatorsRegex.patternText).Value)
                && model.Description.Length > ValidatorsRegex.maxLongLenght)
            {
                ModelState.AddModelError("Description", "Field \"Description\" must include only letter, number and symbols: \"-+_.,;:?()\"");
                result = result && false;
            }


            //model.Duration;
            if (model.Duration < 10
                || model.Duration > 1440)
            {
                ModelState.AddModelError("Description", "Field \"Duration\" must be betwin 10 min and 24 hour");
                result = result && false;
            }

            ////model.CommentsToServices;
            //if (model.CommentsToServices != null
            //    && Regex.Matches(model., patternText).Count != 2
            //    && model.Description.Length < maxLongLenght)
            //{
            //    ModelState.AddModelError("CommentsToServices", "Field \"CommentsToServices\" must include only letter, number and symbols: \"-+_.,;:?()\"");
            //    result = result && false;
            //}


            ////model.Responsibles;
            //if (model.Responsibles != null
            //    && Regex.Matches(model.Responsibles., patternText).Count != 2
            //    && model.Description.Length < maxLongLenght)
            //{
            //    ModelState.AddModelError("Responsibles", "Field \"Description\" must include only letter, number and symbols: \"-+_.,;:?()\"");
            //    result = result && false;
            //}



            return result;
        }


        private bool validSchedule(Event entry)
        {
            if (!validData(entry.Start, true))
            {
                ModelState.AddModelError("Start", "Date and time mast be betwin now and {DateTime.MaxValue}.");
                //return false;
            }
            DateTime dateStart = entry.Start.AddDays(-1);
            DateTime dateEnd = entry.Start.AddDays(1);
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
                    ModelState.AddModelError("Start", "Here was a time intersection with another event.");
                    return false;
                }
            }
            return true;
        }


        private bool validData(DateTime value, bool required = false)
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
            if (value > DateTime.Now && value < DateTime.MaxValue)// check numbers
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
            //ModelState.AddModelError("", $"Date and time mast be betwin now and {DateTime.MaxValue}.");
            return false;
        }





    }
}