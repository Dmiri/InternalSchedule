using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hnatob.WebUI.Models.Pagination
{
    public class PagingInfo
    {
        // Кол-во товаров
        public int TotalItems { get; set; }

        // Кол-во товаров на одной странице
        public int ItemsPerPage { get; set; }

        // Номер текущей страницы
        public int CurrentPage { get; set; }

        // Номер страницы по умолчанию
        public int DefaulpPage { get; set; } = 1;

        // Общее кол-во страниц
        private int totalPage;
        public int TotalPages
        {
            get
            {
                if (totalPage != 0) return totalPage;
                return (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage);
            }
            set { totalPage = value; }
        }
    }
}