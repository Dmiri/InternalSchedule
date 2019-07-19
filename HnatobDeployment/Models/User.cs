using Hnatob.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hnatob.WebUI.Models
{
    public class User
    {
        public String Id { get; set; }
        [Display(Name = "Phone number")]
        public String PhoneNumber { get; set; }
        public String Email { get; set; }
        public EmployeeView employeeView;
    }


    public class EmployeeView
    {
        public String Name { get; set; }
        public String Surname { get; set; }
        public List<Position> Positions { get; set; }
        public DateTime? Birthday { get; set; }
    }
}