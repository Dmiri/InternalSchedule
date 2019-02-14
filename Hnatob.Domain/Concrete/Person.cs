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

namespace Hnatob.Domain.Concrete
{
    public class Person : IPerson
    {
        public Person()
        {
            Positions = new List<Position>();
        }

        //[Maxlenght]
        public string Nickname { get; set; }
        public string Introduction { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Position> Positions { get; set; }

    }
}
