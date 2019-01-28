using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Hnatob.Domain.Models
{
    public class ModelForEvent
    {
        [HiddenInput]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
    }
}
