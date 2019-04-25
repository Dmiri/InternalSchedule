using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Hnatob.Domain.Abstract;
using Hnatob.Domain.Concrete;

namespace Hnatob.Domain.Helper
{
    public class Position
    {
        public Position()
        {
            People = new List<Person>();
            Responsibles = new List<Responsible>();
        }

        [Key, Column(Order = 0)]
        public int Id { get; set; }

        [Index(IsUnique = true)]
        [MaxLength(32)]
        [Required(ErrorMessage = "Please, enter position's name")]
        public string Name { get; set; }

        public virtual ICollection<Person> People { get; set; }
        public virtual ICollection<Responsible> Responsibles { get; set; }
    }
}
