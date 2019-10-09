using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hnatob.WebUI.Models.Pagination
{
    public class PeopleListPaginationViewMode
    {
        public IEnumerable<User> People { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}