using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Embed

// Project
using Hnatob.Domain.Abstract;
using Hnatob.Domain.Concrete;
using Hnatob.Domain.Helper;

namespace Hnatob.Domain.Concrete
{
    public class Event : IEvent
    {
        public Event()
        {
            ActorsId = new List<int>();
        }

        public string Prefix { get; set; }

        //[Required(ErrorMessage = "Please, indicate who will be produce event")]
        public int Producer { get; set; }

        public int Conductor { get; set; }

        public int Choirmaster { get; set; }

        public int Accompanist { get; set; }

        [Display(Name = "Lighting Designer")]
        public int LightingDesigner { get; set; }

        [Display(Name = "Sound Engineer")]
        public int SoundEngineer { get; set; }

        public virtual List<int> ActorsId { get; set; }


        public bool Orchestra { get; set; }

        public bool Choir { get; set; }

        [Display(Name = "Mimic ensemble")]
        public bool Mimic { get; set; }
    }
}
