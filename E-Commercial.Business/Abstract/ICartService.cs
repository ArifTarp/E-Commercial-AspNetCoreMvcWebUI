using System;
using System.Collections.Generic;
using System.Text;
using E_Commercial.Entity.Concrete;

namespace E_Commercial.Business.Abstract
{
    public interface ICartService
    {
        void AddToCard(Cart cart, Product product);
        void RemoveFromCart(Cart cart, int productId);
        List<CartLine> ListOfCartLines(Cart cart);
    }
}
