using Microsoft.Extensions.DependencyInjection;
using ShipShop.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipShop.Application
{
    public static class ApplicationServiceRegistration
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<CategoryService>();
            services.AddScoped<BrandService>();
            services.AddScoped<LookupService>();   
            services.AddScoped<ProductService>();
            services.AddScoped<RoleService>();
            services.AddScoped<UserService>();  
            services.AddScoped<CartService>();
            services.AddScoped<OrderService>(); 
           

        }
    }
}
