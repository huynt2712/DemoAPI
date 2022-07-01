using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using BlogWebApi.Entites;
using BlogWebApi.Entties;

namespace BlogWebApi.Data
{
    public class BlogDBContext : DbContext
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostCategory> Categories { get; set; }
        public DbSet<User> Users { get; set; }

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
            //.UseSqlServer("Server=MSI;Database=PostCategoryDB;Password=1111;Trusted_Connection=True;MultipleActiveResultSets=true");
        }
    }
}
