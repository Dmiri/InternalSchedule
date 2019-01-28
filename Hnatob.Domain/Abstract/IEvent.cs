// System
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

// Embed

// Project
using Hnatob.Domain.Concrete;

namespace Hnatob.Domain.Abstract
{
    public abstract class IEvent
    {
        public IEvent()
        {
            Start = DateTime.Now;
        }


        [HiddenInput,
            Key,
            DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        //[HiddenInput]
        [Required(ErrorMessage = "Please, enter access")]
        public string Access { get; set; }

        //[Display(Name = "Title")]
        [Required(ErrorMessage = "Please, enter Title")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please, enter name location")]
        public string Location { get; set; }

        [Required(ErrorMessage = "Please, enter the start time")]
        //[Range(DateTime.Now, DateTime.MaxValue, ErrorMessage = "Please, enter the start time")]
        public DateTime Start { get; set; }

        public DateTime Duration { get; set; }

        public string Description { get; set; }
    }
}
