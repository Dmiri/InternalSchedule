using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Hnatob.Domain.Helper;
using System.ComponentModel.DataAnnotations.Schema;
using Hnatob.Domain.Abstract;
using Hnatob.Domain.Concrete;

namespace Hnatob.Domain.Helper
{
    //[Table(name: "PositionPersons")]
    //public class Employee
    //{
    //    [Key, Column("PositionId", Order = 0)]
    //    public int PositionId { get; set; }

    //    [Key, Column("PersonId", Order = 1)]
    //    public int PersonId { get; set; }

    //    [ForeignKey("PositionId")]
    //    public Position Position { get; set; }

    //    [ForeignKey("PersonId")]
    //    public Person Person { get; set; }
    //}
}
