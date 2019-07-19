using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hnatob.WebUI.Models
{
    public class RoutViewModels
    {
        public RoutViewModels()
        {
            //Controller = "";
            //Action = "";
        }

        public string Controller { get; set; }
        public string Action { get; set; }
        public string Id { get; set; }
        public string Title { get; set; }
        public string Data { get; set; }
    }
}