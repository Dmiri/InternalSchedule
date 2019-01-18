// System
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Project
using Hnatob.Domain.Abstract;

namespace Hnatob.Domain.Abstract
{
    public interface IPeopleRepository
    {
        IEnumerable<IPerson> People { get; }
        void SavePerson(IPerson person);
        IPerson DeletePerson(int personId);
    }
}
