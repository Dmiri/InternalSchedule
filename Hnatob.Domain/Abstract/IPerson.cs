using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hnatob.Domain.Abstract
{
    public abstract class IPerson
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public string Surname { get; set; }
        public DateTime Birthday { get; set; }
    }
}
