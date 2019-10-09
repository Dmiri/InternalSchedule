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
            Data = new HtmlString("");
            ButtonCancel = "Cancel";
            ButtonConfirm = "Confirm";
        }

        public string Controller { get; set; }
        public string Action { get; set; }
        public string Id { get; set; }
        public string Title { get; set; }
        public HtmlString Data { get; set; }
        public string ButtonCancel { get; set; }
        public string ButtonConfirm { get; set; }
    }
}