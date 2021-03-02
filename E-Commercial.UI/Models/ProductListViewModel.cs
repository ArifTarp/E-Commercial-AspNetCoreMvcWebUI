using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_Commercial.Entity.Concrete;

namespace E_Commercial.UI.Models
{
    public class ProductListViewModel
    {
        public List<Product> Products { get; set; }
        public int PageCount { get; set; }
        public int PageSize { get; set; }
        public int CurrentCategoryId { get; set; }
        public int CurrentPage { get; set; }
    }
}
