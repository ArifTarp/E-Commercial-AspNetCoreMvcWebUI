using System;
using System.Collections.Generic;
using System.Text;
using E_Commercial.Entity.Concrete;

namespace E_Commercial.Business.Abstract
{
    public interface ICategoryService
    {
        List<Category> GetAll();
    }
}
