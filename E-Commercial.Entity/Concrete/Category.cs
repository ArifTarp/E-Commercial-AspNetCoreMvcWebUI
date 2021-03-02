using E_Commercial.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commercial.Entity.Concrete
{
    public class Category : IEntity
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
