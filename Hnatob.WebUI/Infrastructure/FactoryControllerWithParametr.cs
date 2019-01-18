// System
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

// Project
using Hnatob.Domain.Abstract;
using Hnatob.Domain.Concrete;
using Hnatob.WebUI.Controllers;

namespace Hnatob.WebUI.App_Start
{
    public class FactoryControllerWithParametr : DefaultControllerFactory
    {
        public override IController CreateController(RequestContext requestContext, string controllerName)
        {
            switch (controllerName)
            {
                case "Scheduler":
                    //targetType = typeof(SchedulerController);
                    return new SchedulerController(new EfScheduleRepository());

            }
            return base.CreateController(requestContext, controllerName);
        }
    }
}