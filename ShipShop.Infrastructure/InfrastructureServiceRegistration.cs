﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Client;
using ShipShop.Core.Entities;
using ShipShop.Core.Interfaces;
using ShipShop.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipShop.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<ISubCategoryRepository, SubCategoryRepository>();
            services.AddScoped<IRepository<Category>, CategoryRepository>();
            services.AddScoped<IRepository<Brand>, BrandRepository>();
            services.AddScoped<ILookupRepository, LookupRepository>();  
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IRepository<Role>, RoleRepository>();  
            services.AddScoped<IUserRepository, UserRepository>();  
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IAuthRepository, AuthRepository>();    
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();    
            services.AddScoped<IWishlistRepository,WishListRepository>();
            services.AddScoped<ITestimonialRepository, TestimonialRepository>();
            services.AddScoped<IMessagesRepository, MessagesRepository>();

        }
    }
}
