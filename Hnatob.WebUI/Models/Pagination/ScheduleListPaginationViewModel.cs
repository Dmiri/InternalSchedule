using Hnatob.Domain.Abstract;
using Hnatob.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hnatob.WebUI.Models.Pagination
{
    public class ScheduleListPaginationViewModel
    {
        public IEnumerable<Event> Schedule { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}