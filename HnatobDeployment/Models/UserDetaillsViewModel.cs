using Hnatob.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hnatob.WebUI.Models
{
    public class UserDetaillsViewModel
    {
        public String Id { get; set; }
        public String PhoneNumber { get; set; }
        public String Email { get; set; }

        public String Name { get; set; }
        public String Surname { get; set; }
        public string Patronymic { get; set; }
        public DateTime? Birthday { get; set; }

        public List<Position> Positions { get; set; }

        public string Nickname { get; set; }
        public string Introduction { get; set; }
        public string Description { get; set; }

    }
}