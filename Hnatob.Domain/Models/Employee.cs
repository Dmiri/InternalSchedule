// System
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Project
using Hnatob.Domain.Abstract;

namespace Hnatob.Domain.Models
{
    public class Employee : IPerson
    {
        public Employee()
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
