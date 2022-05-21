using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using BlogApi.Models;

namespace BlogApi.Data
{
    public class BlogDBContext : DbContext
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostCategory> Categories { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLazyLoadingProxies()
                .ConfigureWarnings(w => w.Ignore(CoreEventId.LazyLoadOnDisposedContextWarning))
                  .UseSqlServer("Data Source=MSI;Initial Catalog=PostCategoryDB;User ID=sa;Password=1111;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }
    }
}
