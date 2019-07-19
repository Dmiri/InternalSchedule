using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hnatob.Domain.Models
{
    public class CommentToService
    {
        [Key, Column(Order = 0)]
        public int EventId { get; set; }

        [Key, Column(Order = 1)]
        public String ServiceName { get; set; }

        [MaxLength(256)]
        public string Comment { get; set; }


        public Service Service { get; set; }

        public Event Event { get; set; }
    }
}
