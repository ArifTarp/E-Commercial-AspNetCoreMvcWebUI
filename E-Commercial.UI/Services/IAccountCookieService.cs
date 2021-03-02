using E_Commercial.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commercial.UI.Services
{
    public interface IAccountCookieService
    {
        void SetAccountToCookie(LoginViewModel loginViewModel);
        LoginViewModel GetAccountFromCookie();
    }
}
