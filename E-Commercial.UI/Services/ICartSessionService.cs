using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_Commercial.Entity.Concrete;

namespace E_Commercial.UI.Services
{
    public interface ICartSessionService
    {
        void SetCartToSession(Cart cart);
        Cart GetCartFromSession();
    }
}
