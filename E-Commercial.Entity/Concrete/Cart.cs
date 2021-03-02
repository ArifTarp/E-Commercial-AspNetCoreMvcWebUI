using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace E_Commercial.Entity.Concrete
{
    public class Cart
    {
        public Cart()
        {
            CartLines = new List<CartLine>();
        }

        public List<CartLine> CartLines { get; set; }

        public decimal Total
        {
            get { return CartLines.Sum(cl => cl.Product.UnitPrice * cl.Quantity); }
        }
    }
}
