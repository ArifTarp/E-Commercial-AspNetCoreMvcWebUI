using System;
using System.Collections.Generic;
using System.Text;
using E_Commercial.Core.DataAccess.EntityFramework;
using E_Commercial.DataAccess.Abstract;
using E_Commercial.Entity.Concrete;

namespace E_Commercial.DataAccess.Concrete.EntityFramework
{
    public class EfCategoryDal : EfEntityRepositoryBase<Category, NorthwindContext>, ICategoryDal
    {
    }
}
