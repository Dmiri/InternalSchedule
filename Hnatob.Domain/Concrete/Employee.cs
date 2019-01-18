using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Hnatob.Domain.Helper;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hnatob.Domain.Concrete
{
    public class Employee
    {
        [Key, Column(Order = 0)]
        public int PersonId { get; set; }
        [Key, Column(Order = 1)]
        public EPosition Position { get; set; }
    }
}
