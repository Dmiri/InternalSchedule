// System
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

// Project
using Hnatob.DataAccessLayer.Concrete;
using Hnatob.DataAccessLayer.Context;
using Hnatob.WebUI.Controllers;

namespace Hnatob.WebUI.Infrastructure
{
    public class FactoryControllerWithParametr : DefaultControllerFactory
    {
        public override IController CreateController(RequestContext requestContext, string controllerName)
        {
            switch (controllerName)
            {
                case "Scheduler":
                    //targetType = typeof(SchedulerController);
                    return new SchedulerController(
                        new EfScheduleRepository(), 
                        new EfUserRepository(),
                        new EFResponsiblesRepository());
                case "Employees":
                    return new EmployeesController(new EfUserRepository(),new ApplicationDbContext());
                case "Profile":
                    return new ProfileController(new ApplicationDbContext());
            }
            return base.CreateController(requestContext, controllerName);
        }
    }
}