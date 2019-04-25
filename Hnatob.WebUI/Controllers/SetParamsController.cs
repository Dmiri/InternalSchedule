using Hnatob.Domain.Abstract;
using Hnatob.Domain.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hnatob.WebUI.Controllers
{
    public class SetParamsController : Controller
    {
        private readonly IResponsiblesRepository repositorySetting;
        private readonly IPeopleRepository repositoryPeople;
        public SetParamsController(IResponsiblesRepository repositorySetting, IPeopleRepository repositoryPeople)
        {
            if(repositorySetting != null)
                this.repositorySetting = repositorySetting;
            else throw new ArgumentNullException("No arguments");
            if (repositoryPeople != null)
                this.repositoryPeople = repositoryPeople;
            else throw new ArgumentNullException("No arguments");
        }
        // GET: SetParams
        public ActionResult Setting()
        {
            //var list = repositoryEvent.GetEventsTypes();
            var listResponsibles = repositorySetting.GetResponsibles().ToList();
            ViewBag.listPosition = repositoryPeople.GetPositions().ToList();
            return View(listResponsibles);
        }

        // GET: SetParams
        [HttpPost]
        public ActionResult Setting(List<Responsible> list)
        {
            var listResponsibles = repositorySetting.GetResponsibles().ToList();
            ViewBag.listPosition = repositoryPeople.GetPositions().ToList();

            if(list != null && list.Count > 0)
            {
                if (validList(list))
                {
                    repositorySetting.UpdateResponsible(list);
                    TempData["Message"] = string.Format($"Settings was saved");
                    listResponsibles = repositorySetting.GetResponsibles().ToList();
                    return View(listResponsibles);
                }
                else TempData["Error"] = string.Format($"Settings wasn't saved");
            }

            return View(listResponsibles);

            bool validList(List<Responsible> listResp)
            {
                for (int i = 0; i < listResp.Count; i++)
                {//TODO /*listResp[i].Position != 0 && */
                    for (int j = i + 1;j < listResp.Count; j++)
                    {
                        //if (listResp[i].Position == listResp[j].Position) return false;
                    }
                }
                return true;
            }
        }
    }
}