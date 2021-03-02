using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_Commercial.Business.Abstract;
using E_Commercial.Entity.Concrete;
using E_Commercial.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Commercial.UI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        public AdminController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var products = _productService.GetAll();
            var model = new ProductListViewModel
            {
                Products = products
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult AddProduct()
        {
            var model = new ProductAddViewModel
            {
                Product = new Product(),
                Categories = _categoryService.GetAll()
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult AddProduct(ProductAddViewModel productAddViewModel)
        {
            if (ModelState.IsValid)
            {
                var newProduct = new Product
                {
                    CategoryId = productAddViewModel.Product.CategoryId,
                    ProductName = productAddViewModel.Product.ProductName,
                    UnitPrice = productAddViewModel.Product.UnitPrice,
                    UnitsInStock = productAddViewModel.Product.UnitsInStock
                };
                _productService.Add(newProduct);
                TempData.Add("message", String.Format("Product {0}, successfully added to products store", newProduct.ProductName));
            }
            
            return RedirectToAction("AddProduct","Admin");
        }

        [HttpGet]
        public IActionResult UpdateProduct(int productId)
        {
            var product = _productService.GetProductByProductId(productId);
            var model = new ProductUpdateViewModel
            {
                Product = product,
                Categories = _categoryService.GetAll()
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult UpdateProduct(ProductUpdateViewModel productUpdateViewModel)
        {
            ModelState.Remove("Categories");
            if (ModelState.IsValid)
            {
                var updateNewProduct = new Product
                {
                    ProductId = productUpdateViewModel.Product.ProductId,
                    CategoryId = productUpdateViewModel.Product.CategoryId,
                    ProductName = productUpdateViewModel.Product.ProductName,
                    UnitPrice = productUpdateViewModel.Product.UnitPrice,
                    UnitsInStock = productUpdateViewModel.Product.UnitsInStock
                };
                _productService.Update(updateNewProduct);
                TempData.Add("message", String.Format("Updated product {0}", updateNewProduct.ProductName));
            }

            return RedirectToAction("UpdateProduct", "Admin", new { productId = productUpdateViewModel.Product.ProductId });
        }

        [HttpGet]
        public IActionResult DeleteProduct(int productId)
        {
            var product = _productService.GetProductByProductId(productId);
            if (product == null)
            {
                TempData.Add("NotFoundProduct", "Not Found Product");
                return RedirectToAction("Index","Admin");
            }

            _productService.Delete(productId);
            TempData.Add("message", String.Format("Deleted product {0}", product.ProductName));

            return RedirectToAction("Index","Admin");
        }
    }
}