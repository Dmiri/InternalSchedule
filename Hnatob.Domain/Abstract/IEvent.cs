// System
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

// Embed

// Project

namespace Hnatob.Domain.Abstract
{
    public abstract class IEvent
    {
        public IEvent()
        {
            Start = DateTime.Now;
        }


        [HiddenInput, Key]
        public int Id { get; set; }

        [Display(Name = "Access"), Required(ErrorMessage = "Please, enter access")]
        public string Access { get; set; }

        [Display(Name = "Type")]
        //[RegularExpression(@"^[[]{}#^$.,'|\/\\?*@+()]", ErrorMessage = "Поле не может содержать цифры и символов []{}#^$.,'\"|/\\?*@+()]")]
        public string EventType { get; set; }

        [Display(Name = "Title"), Required(ErrorMessage = "Please, enter Title")]
        public string Title { get; set; }

        [Display(Name = "Location"), Required(ErrorMessage = "Please, enter name location")]
        public string Location { get; set; }

        [Display(Name = "Start"), Required(ErrorMessage = "Please, enter the start time")]
        public DateTime Start { get; set; }

        [Display(Name = "Duration")]
        [Required(ErrorMessage = "Please, enter duration")]
        [Range(1, 1440, ErrorMessage = "Please, enter the duration event between 0 and 1440 minutes")]
        public int Duration { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}
