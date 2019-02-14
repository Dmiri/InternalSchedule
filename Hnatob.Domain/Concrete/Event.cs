using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Embed

// Project
using Hnatob.Domain.Abstract;

namespace Hnatob.Domain.Concrete
{
    public class Event : IEvent
    {
        public Event()
        {
            ActorsId = new List<int>();
        }

        [Display(Name = "Type")]
        //[RegularExpression(@"^[[]{}#^$.,'|\/\\?*@+()]", ErrorMessage = "Поле не может содержать цифры и символов []{}#^$.,'\"|/\\?*@+()]")]
        public string EventType { get; set; }

        [Display(Name = "Producer")]
        //[RegularExpression("[[]{}#^$.,'\"|/\\?*@+()]]", ErrorMessage = "Некорректное имя. Поле не может содержать цифры и символы []{}#^$.,'\"|/\\?*@+()]")]
        [Required(ErrorMessage = "Please, indicate who will be produce event")]
        public string Producer { get; set; }

        [Display(Name = "Conductor")]
        //[RegularExpression("[[]{}#^$.,'\"|/\\?*@+()]]", ErrorMessage = "Некорректное имя. Поле не может содержать цифры и символы []{}#^$.,'\"|/\\?*@+()]")]
        public string Conductor { get; set; }

        [Display(Name = "Choirmaster")]
        //[RegularExpression("[[]{}#^$.,'\"|/\\?*@+()]]", ErrorMessage = "Некорректное имя. Поле не может содержать цифры и символы []{}#^$.,'\"|/\\?*@+()]")]
        public string Choirmaster { get; set; }

        [Display(Name = "Accompanist")]
        //[RegularExpression("[[]{}#^$.,'\"|/\\?*@+()]]", ErrorMessage = "Некорректное имя. Поле не может содержать цифры и символы []{}#^$.,'\"|/\\?*@+()]")]
        public string Accompanist { get; set; }

        [Display(Name = "Lighting Designer")]
        //[RegularExpression("[[]{}#^$.,'\"|/\\?*@+()]]", ErrorMessage = "Некорректное имя. Поле не может содержать цифры и символы []{}#^$.,'\"|/\\?*@+()]")]
        public string LightingDesigner { get; set; }

        [Display(Name = "Sound Engineer")]
        //[RegularExpression("[[]{}#^$.,'\"|/\\?*@+()]]", ErrorMessage = "Некорректное имя. Поле не может содержать цифры и символы []{}#^$.,'\"|/\\?*@+()]")]
        public string SoundEngineer { get; set; }

        public virtual List<int> ActorsId { get; set; }


        public bool Orchestra { get; set; }

        public bool Choir { get; set; }

        [Display(Name = "Mimic ensemble")]
        public bool Mimic { get; set; }


        public Event ShallowCopy()
        {
            return (Event)this.MemberwiseClone();
        }

        public Event DeepCopy()
        {
            Event other = (Event)this.MemberwiseClone();
            //TODU: add fild
            return other;
        }
    }
}
