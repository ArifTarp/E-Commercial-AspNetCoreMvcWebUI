using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_Commercial.Business.Abstract;
using E_Commercial.UI.Models;
using Microsoft.AspNetCore.Mvc;

namespace E_Commercial.UI.Controllers
{
    public class ProductController : Controller
    {
        private IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public IActionResult Index(int page=1, int categoryId=0)
        {
            int pageSize = 10;
            var products = _productService.GetProductsByCategoryId(categoryId);
            ProductListViewModel model = new ProductListViewModel
            {
                Products = products.Skip((page-1)*pageSize).Take(10).ToList(),
                PageCount = (int)Math.Ceiling((double)products.Count/pageSize),
                PageSize = pageSize,
                CurrentCategoryId = categoryId,
                CurrentPage = page
            };
            return View(model);
        }
    }
}