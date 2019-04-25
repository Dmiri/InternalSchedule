// System
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Project
using Hnatob.Domain.Abstract;
using Hnatob.Domain.Helper;

namespace Hnatob.Domain.Helper
{
    public class Person : IPerson
    {
        public Person()
        {
            Services = new List<Service>();
            Positions = new List<Position>();
            Responsibles = new List<Responsible>();
        }

        //[Maxlenght]
        public string Nickname { get; set; }
        public string Introduction { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Service> Services { get; set; }
        public virtual ICollection<Position> Positions { get; set; }
        public virtual ICollection<Responsible> Responsibles { get; set; }
    }
}
