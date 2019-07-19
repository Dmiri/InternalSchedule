using Hnatob.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hnatob.WebUI.Models
{
    public class SetDataViewModel
    {
        public string Id { get; set; }
        public string UserName  { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public List<string> CurentData { get; set; }
        public List<string> AllData { get; set; }
    }
}