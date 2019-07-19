using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hnatob.WebUI.Models
{
    public class Menu
    {
        public Menu(string title, string controller, string action)
        {
            Title = title;
            Controller = controller;
            Action = action;
        }
        public string Title { get;}
        public string Controller { get; }
        public string Action { get; }
    }
}