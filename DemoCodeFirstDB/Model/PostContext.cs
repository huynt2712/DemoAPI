using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoCodeFirstDB.Model
{
    internal class PostContext : DbContext
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<Category> Categories { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLazyLoadingProxies()
                .UseSqlServer("Data Source=MSI;Initial Catalog=PostDB;User ID=sa;Password=1111;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }
    }
}
