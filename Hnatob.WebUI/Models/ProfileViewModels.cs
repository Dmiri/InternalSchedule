using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hnatob.WebUI.Models
{
    public class ProfileViewModels
    {
        public string UserId { get; set; }

        public string Name { get; set; }
        public string Patronymic { get; set; }
        public string Surname { get; set; }
        [Required(ErrorMessage = "Please enter your Birthday date.")]
        public DateTime Birthday { get; set; }

        public string Nickname { get; set; }
        public string Introduction { get; set; }
        public string Description { get; set; }

        [Display(Name = "Phone number")]
        public String PhoneNumber { get; set; }
        public string Email { get; set; }

        public bool TwoFactorEnabled { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool EmailConfirmed { get; set; }
    }
}
