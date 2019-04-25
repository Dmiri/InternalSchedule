using Hnatob.Domain.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hnatob.Domain.Helper
{
    public class Responsible
    {
        [Key]
        public int Id { get; set; }

        public int EventId { get; set; }

        public int? PersonId { get; set; }

        public int PositionId { get; set; }

        [MaxLength(64)]
        public string PersonName { get; set; }

        [MaxLength(128)]
        public string Comment { get; set; }

        public Event Event { get; set; }
        public Person Person { get; set; }
        public Position Position { get; set; }
    }
}
