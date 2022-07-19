using BlogWebApi.Data;

namespace BlogWebApi.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BlogDBContext _blogDBContext;

        public UnitOfWork(BlogDBContext blogDBContext)
        {
            _blogDBContext = blogDBContext;
        }

        public async Task SaveAsync()
        {
            await _blogDBContext.SaveChangesAsync();
        }

        public BlogDBContext GetDbContext()
        {
            return _blogDBContext;
        }
    }
}
