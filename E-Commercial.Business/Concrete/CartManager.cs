using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using E_Commercial.Business.Abstract;
using E_Commercial.Entity.Concrete;

namespace E_Commercial.Business.Concrete
{
    public class CartManager : ICartService
    {
        public void AddToCard(Cart cart, Product product)
        {
            CartLine cartLine = cart.CartLines.FirstOrDefault(cl=>cl.Product.ProductId==product.ProductId);
            if (cartLine != null)
            {
                cartLine.Quantity += 1;
            }
            else
            {
                cart.CartLines.Add(new CartLine{Product = product,Quantity = 1});
            }
        }

        public void RemoveFromCart(Cart cart, int productId)
        {
            CartLine cartLine = cart.CartLines.FirstOrDefault(cl => cl.Product.ProductId == productId);
            cart.CartLines.Remove(cartLine);
        }

        public List<CartLine> ListOfCartLines(Cart cart)
        {
            return cart.CartLines;
        }
    }
}
