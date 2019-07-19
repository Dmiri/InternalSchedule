using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Hnatob.Domain.Models;

namespace Hnatob.Domain.Abstract
{
    public abstract class IPerson
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public string Surname { get; set; }
        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }

        [Column(name: "UserId")]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}
