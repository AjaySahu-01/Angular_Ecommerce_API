using Angular_Ecommerce.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Angular_Ecommerce.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {


        }
       
        public DbSet<User> users { get; set; }
        public DbSet<Product> Products { get; set; }  
        public DbSet<Cart> carts { get; set; }

   
    }
}
