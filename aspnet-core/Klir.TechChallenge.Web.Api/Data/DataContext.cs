using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Klir.TechChallenge.Web.Api.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Entities.Product> Products { get; set; }
        public DbSet<Entities.Promotion> Promotions { get; set; }
    }
}