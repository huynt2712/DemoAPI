using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using BlogWebApi.Models;

namespace BlogWebApi.Data
{
    public class BlogDBContext : DbContext
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostCategory> Categories { get; set; }

        public BlogDBContext()
        {

        }    
        public BlogDBContext(DbContextOptions<BlogDBContext> options):base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLazyLoadingProxies()
                .ConfigureWarnings(w => w.Ignore(CoreEventId.LazyLoadOnDisposedContextWarning));
                  //.UseSqlServer(@"Data Source=.\SQLEXPRESS;Initial Catalog=BlogDB1;User ID=sa;Password=Asdf@123456");
        }
    }
}
