using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using WebApiCodeFirstDB.Models;

namespace WebApiCodeFirstDB.Data
{
    public class BlogDBContext : DbContext
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostCategory> Categories { get; set; }

        protected readonly IConfiguration Configuration;

        public BlogDBContext(IConfiguration configuration, DbContextOptions<BlogDBContext> options)
            : base(options)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder
            //    .UseLazyLoadingProxies()
            //    .ConfigureWarnings(w => w.Ignore(CoreEventId.LazyLoadOnDisposedContextWarning));
                //.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            //.UseSqlServer("Data Source=MSI;Initial Catalog=PostCategoryDB;User ID=sa;Password=1111;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }
    }
}
