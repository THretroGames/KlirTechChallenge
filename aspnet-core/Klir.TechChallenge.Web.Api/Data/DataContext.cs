using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Klir.TechChallenge.Web.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Klir.TechChallenge.Web.Api.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Promotion> Promotions { get; set; }
        public DbSet<CartProduct> CartProducts { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
    }
}