using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_Commercial.Entity.Concrete;

namespace E_Commercial.UI.Models
{
    public class ProductUpdateViewModel
    {
        public Product Product { get; set; }
        public List<Category> Categories { get; set; }
    }
}
