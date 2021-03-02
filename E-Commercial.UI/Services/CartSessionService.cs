using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_Commercial.Entity.Concrete;
using E_Commercial.UI.Extensions;
using Microsoft.AspNetCore.Http;

namespace E_Commercial.UI.Services
{
    public class CartSessionService:ICartSessionService
    {
        private IHttpContextAccessor _httpContextAccessor;
        public CartSessionService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void SetCartToSession(Cart cart)
        {
            _httpContextAccessor.HttpContext.Session.SetObject("cart", cart);
        }

        public Cart GetCartFromSession()
        {
            Cart cart = _httpContextAccessor.HttpContext.Session.GetObject<Cart>("cart");
            if (cart==null)
            {
                SetCartToSession(new Cart());
                cart = _httpContextAccessor.HttpContext.Session.GetObject<Cart>("cart");
            }

            return cart;
        }
    }
}
