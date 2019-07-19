using System.ComponentModel.DataAnnotations;

namespace Hnatob.Domain.Models
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
        public Employee Person { get; set; }
        public Position Position { get; set; }
    }
}
