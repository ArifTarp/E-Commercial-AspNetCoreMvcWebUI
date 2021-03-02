using E_Commercial.Business.Abstract;
using E_Commercial.Business.Concrete;
using E_Commercial.DataAccess.Abstract;
using E_Commercial.DataAccess.Concrete.EntityFramework;
using E_Commercial.UI.Custom_Middlewares;
using E_Commercial.UI.Entities;
using E_Commercial.UI.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commercial.UI
{
    public class Startup
    {
        private IConfiguration _configuration;
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options => { options.EnableEndpointRouting = false; });

            // dependency injections
            services.AddSingleton<IProductService, ProductManager>();
            services.AddSingleton<IProductDal, EfProductDal>();
            services.AddSingleton<ICategoryService, CategoryManager>();
            services.AddSingleton<ICategoryDal, EfCategoryDal>();
            services.AddSingleton<ICartService, CartManager>();
            services.AddSingleton<ICartSessionService, CartSessionService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IAccountCookieService, AccountCookieService>();
            // session
            services.AddSession();
            services.AddDistributedMemoryCache();

            // identity
            services.AddDbContext<CustomIdentityDbContext>(options => options.UseSqlServer(_configuration.GetConnectionString("IdentityDbContext")));
            services.AddIdentity<CustomIdentityUser, CustomIdentityRole>().AddEntityFrameworkStores<CustomIdentityDbContext>().AddDefaultTokenProviders();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseFileServer();
            app.UseNodeModules(env.ContentRootPath);

            app.UseSession();

            app.UseAuthentication();

            app.UseMvc(ConfigureRoutes);
        }

        public void ConfigureRoutes(IRouteBuilder routeBuilder)
        {
            routeBuilder.MapRoute("Default", "{controller=Product}/{action=Index}");
        }
    }
}
