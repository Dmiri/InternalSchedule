using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hnatob.Domain.Models
{
    public class Position
    {
        public Position()
        {
            People = new List<Employee>();
            Responsibles = new List<Responsible>();
        }

        [Key, Column(Order = 0)]
        public int Id { get; set; }

        [Index(IsUnique = true)]
        [MaxLength(32)]
        [Required(ErrorMessage = "Please, enter position's name")]
        public string Name { get; set; }

        public virtual ICollection<Employee> People { get; set; }
        public virtual ICollection<Responsible> Responsibles { get; set; }
    }
}
