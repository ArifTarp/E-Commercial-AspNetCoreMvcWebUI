using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_Commercial.Business.Abstract;
using E_Commercial.Entity.Concrete;
using E_Commercial.UI.Models;
using E_Commercial.UI.Services;
using Microsoft.AspNetCore.Mvc;

namespace E_Commercial.UI.Controllers
{
    public class CartController : Controller
    {
        private IProductService _productService;
        private ICartSessionService _cartSessionService;
        private ICartService _cartService;
        public CartController(IProductService productService, ICartSessionService cartSessionService, ICartService cartService)
        {
            _productService = productService;
            _cartSessionService = cartSessionService;
            _cartService = cartService;
        }

        public IActionResult AddToCart(int productId)
        {
            var product = _productService.GetProductByProductId(productId);
            var cart = _cartSessionService.GetCartFromSession();

            _cartService.AddToCard(cart,product);
            _cartSessionService.SetCartToSession(cart);

            TempData.Add("message",String.Format("Your product {0}, successfully added to cart!",product.ProductName));

            return RedirectToAction("Index","Product");
        }

        public IActionResult CartList()
        {
            var cartFromSession = _cartSessionService.GetCartFromSession();
            CartSummaryViewModel model = new CartSummaryViewModel
            {
                Cart = cartFromSession
            };

            return View(model);
        }

        public IActionResult Remove(int productId)
        {
            var cart = _cartSessionService.GetCartFromSession();
            _cartService.RemoveFromCart(cart,productId);
            _cartSessionService.SetCartToSession(cart);

            TempData.Add("message", String.Format("Your product, successfully removed from cart!"));

            return RedirectToAction("CartList","Cart");
        }

        [HttpGet]
        public IActionResult Complete()
        {
            var model = new ShippingDetailsViewModel
            {
                ShippingDetails = new ShippingDetails()
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Complete(ShippingDetails shippingDetails)
        {
            if (!ModelState.IsValid)
            {
                TempData.Add("warning", "Please Check Your Informations...");
                return View();
            }
            else
            {
                TempData.Add("message", String.Format("Thank you {0}. Your order is in process",shippingDetails.FirstName));
                return RedirectToAction("Complete","Cart");
            }
        }
    }
}