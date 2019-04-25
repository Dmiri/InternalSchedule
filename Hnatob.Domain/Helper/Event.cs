using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Embed

// Project
using Hnatob.Domain.Abstract;
using Hnatob.Domain.Helper;

namespace Hnatob.Domain.Helper
{
    public class Event : IEvent
    {
        public Event()
        {
            //ActorsId = new List<int>();
            Responsibles = new List<Responsible>();
            CommentsToServices = new List<CommentToService>();
        }

        //[Range(1, 16, ErrorMessage = "The number of responsible must be between 1 and 16")]
        [Required(ErrorMessage = "Please, indicate who will be produce event")]
        public virtual List<Responsible> Responsibles { get; set; }

        public virtual List<CommentToService> CommentsToServices { get; set; }
        //public virtual ICollection<Service> Services { get; set; }

        //public virtual List<Person> ActorsId { get; set; }

    }
}
