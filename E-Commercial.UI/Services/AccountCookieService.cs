using E_Commercial.UI.Extensions;
using E_Commercial.UI.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commercial.UI.Services
{
    public class AccountCookieService : IAccountCookieService
    {
        private IHttpContextAccessor _httpContextAccessor;
        public AccountCookieService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void SetAccountToCookie(LoginViewModel loginViewModel)
        {
            _httpContextAccessor.HttpContext.Response.Cookies.SetObject("loginViewModel", loginViewModel);
        }

        public LoginViewModel GetAccountFromCookie()
        {
            LoginViewModel loginViewModel = _httpContextAccessor.HttpContext.Request.Cookies.GetObject<LoginViewModel>("loginViewModel");

            return loginViewModel;
        }
    }
}
