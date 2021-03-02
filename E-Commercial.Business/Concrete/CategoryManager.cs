using System;
using System.Collections.Generic;
using System.Text;
using E_Commercial.Business.Abstract;
using E_Commercial.DataAccess.Abstract;
using E_Commercial.Entity.Concrete;

namespace E_Commercial.Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private ICategoryDal _categoryDal;
        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }

        public List<Category> GetAll()
        {
            return _categoryDal.GetList();
        }
    }
}
