using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_Commercial.Business.Abstract;
using E_Commercial.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace E_Commercial.UI.View_Components
{
    public class CategoryListViewComponent:ViewComponent
    {
        private ICategoryService _categoryService;
        public CategoryListViewComponent(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public ViewViewComponentResult Invoke()
        {
            var model = new CategoryListViewModel
            {
                Categories = _categoryService.GetAll(),
                CurrentCategoryId = Convert.ToInt32(HttpContext.Request.Query["categoryId"])
            };

            return View(model);
        }
    }
}
