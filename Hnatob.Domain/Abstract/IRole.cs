using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Hnatob.Domain.Abstract
{
    public abstract class IRole
    {
        [HiddenInput, Key]
        public int Id { get; set; }
        public int EventId { get; set; }
        public int PersonId { get; set; }
    }
}
