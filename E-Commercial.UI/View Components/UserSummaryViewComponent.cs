using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_Commercial.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace E_Commercial.UI.View_Components
{
    public class UserSummaryViewComponent : ViewComponent
    {
        public ViewViewComponentResult Invoke()
        {
            var model = new UserDetailsViewModel
            {
                UserName = HttpContext.User.Identity.Name
            };

            return View(model);
        }
    }
}
