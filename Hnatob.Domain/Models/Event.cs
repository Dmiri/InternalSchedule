using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Embed

// Project
using Hnatob.Domain.Abstract;

namespace Hnatob.Domain.Models
{
    public class Event : IEvent
    {
        //public int Id { get; set; }
        //public string Access { get; set; }
        public string Prefix { get; set; }
        //public string Title { get; set; }
        //public string Location { get; set; }
        //public DateTime Start { get; set; }
        public virtual List<IRole> Responsibles { get; set; }

    }
}
