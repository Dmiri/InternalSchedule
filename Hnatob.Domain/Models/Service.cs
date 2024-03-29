﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hnatob.Domain.Models
{
    public class Service
    {
        public Service()
        {
            People = new List<Employee>();
        }

        [Key]
        [MaxLength(64)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please, indicate who will be services director.")]
        public int Director { get; set; }


        //public int PersonId { get; set; }
        //public virtual Person Person { get; set; }

        public ICollection<Employee> People { get; set; }
    }
}
