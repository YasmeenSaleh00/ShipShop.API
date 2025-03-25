using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ShipShop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipShop.Infrastructure.Context
{
    public class ShipShopDbContext : DbContext
    {
        private readonly IConfiguration configuration;

        public ShipShopDbContext(DbContextOptions options , IConfiguration configuration) : base(options)
        {
            this.configuration = configuration; 
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        }
        public DbSet<Category> Categories { get; set; }

        public DbSet<Brand> Brands { get; set; }
        public DbSet<LookupType> LookupTypes { get; set; }
        public DbSet<LookupItem> LookupItems { get; set; }  
        public DbSet<Product> Products { get; set; }
        public DbSet<Role> Roles { get; set; }

        public DbSet<User> Users { get; set; }  
    public DbSet<Customer> Customers { get; set; }
        public DbSet<Cart> Carts { get; set; }      
        public DbSet<CartItem> CartItems { get; set; }  
        public DbSet<Order> Orders { get; set; }    
        public DbSet<OrderItem> OrderItems { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<LookupType>().HasData(
                new LookupType { Id=1,Name= "ProductStatus" },
                new LookupType { Id =2 , Name= "CustomerStatus" },
                new LookupType { Id=3,Name="OrderStatus" },
                new LookupType { Id=4,Name= "CartStatus" }
               
                );

            modelBuilder.Entity<LookupItem>().HasData(
             new LookupItem { Id = 1, Value = "Available",LookupTypeId=1 },
             new LookupItem { Id = 2, Value = "Not Available", LookupTypeId = 1 },
             new LookupItem { Id=3,Value= "Active",LookupTypeId=2 },
             new LookupItem { Id = 4, Value = "Banned", LookupTypeId = 2 },
             new LookupItem { Id = 5, Value = "New", LookupTypeId = 3 },
             new LookupItem { Id = 6, Value = "Confirmed", LookupTypeId = 3 },
             new LookupItem { Id = 7, Value = "Cancelled", LookupTypeId = 3 },
             new LookupItem { Id = 8, Value = "Delivered", LookupTypeId = 3 },
             new LookupItem { Id = 9, Value = "Active", LookupTypeId = 4 },
             new LookupItem { Id = 10, Value = "Ordered", LookupTypeId = 4 },
             new LookupItem { Id = 11, Value = "Abandoned", LookupTypeId = 4 }
             );
            modelBuilder.Entity<Role>().HasData(

                new Role
                {
                    Id = 1,
                    Name = "Admin"
                }
                , new Role {
                    Id = 2, 
                    Name = "Customer" 
                }
            );
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    FirstName = "Yasmeen",
                    LastName = "Saleh",
                    Email = "yasmeensaleh147@gmail.com",
                    Password = "yas12345",
                    RoleId = 1,
             

                });
            modelBuilder.Entity<User>()
        .HasDiscriminator<string>("Discriminator")
        .HasValue<User>("Admin")
        .HasValue<Customer>("Customer");
    
            modelBuilder.Entity<Category>()
           .Property(e => e.CreatedOn)
           .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Category>()
           .Property(e => e.IsActive)
           .HasDefaultValue(true);

            modelBuilder.Entity<Brand>()
        .Property(e => e.CreatedOn)
        .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Brand>()
           .Property(e => e.IsActive)
           .HasDefaultValue(true);

            modelBuilder.Entity<LookupType>()
           .Property(e => e.CreatedOn)
           .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<LookupType>()
           .Property(e => e.IsActive)
           .HasDefaultValue(true);

            modelBuilder.Entity<LookupItem>()
           .Property(e => e.CreatedOn)
           .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<LookupItem>()
           .Property(e => e.IsActive)
           .HasDefaultValue(true);

        
            modelBuilder.Entity<Product>()
          .Property(e => e.CreatedOn)
          .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Product>()
           .Property(e => e.IsActive)
           .HasDefaultValue(true);

            modelBuilder.Entity<Role>()
           .Property(e => e.CreatedOn)
           .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Role>()
           .Property(e => e.IsActive)
           .HasDefaultValue(true);
         

            modelBuilder.Entity<User>()
      .Property(e => e.CreatedOn)
      .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<User>()
           .Property(e => e.IsActive)
           .HasDefaultValue(true);



            modelBuilder.Entity<CartItem>()
 .Property(e => e.CreatedOn)
 .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<CartItem>()
           .Property(e => e.IsActive)
           .HasDefaultValue(true);

            modelBuilder.Entity<Order>()
 .Property(e => e.CreatedOn)
 .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Order>()
           .Property(e => e.IsActive)
           .HasDefaultValue(true);


            modelBuilder.Entity<OrderItem>()
 .Property(e => e.CreatedOn)
 .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<OrderItem>()
           .Property(e => e.IsActive)
           .HasDefaultValue(true);
            modelBuilder.Entity<Customer>()
.Property(e => e.CreatedOn)
.HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Customer>()
           .Property(e => e.IsActive)
           .HasDefaultValue(true);
            // fluent API Approach

            modelBuilder.Entity<Order>()
                  .HasOne(o => o.LookupItem)
                  .WithMany(l => l.Orders) 
                  .HasForeignKey(o => o.OrderStatusId)
                  .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Customer)
                .WithMany(c => c.Orders)  
                .HasForeignKey(o => o.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);  

         
            modelBuilder.Entity<Customer>()
                .HasOne(c => c.LookupItem)
                .WithMany(l => l.Customers)
                .HasForeignKey(c => c.CustomerStatusId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Cart>()
      .HasOne(c => c.Product)
      .WithMany()
      .HasForeignKey(c => c.ProductId)
      .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Cart>()
                .HasOne(c => c.Customer)
                .WithMany(l=>l.Carts)
                .HasForeignKey(c => c.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Cart>()
                .HasOne(c => c.LookupItem)
                .WithMany(l=>l.Carts)
                .HasForeignKey(c => c.StatusCartId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
