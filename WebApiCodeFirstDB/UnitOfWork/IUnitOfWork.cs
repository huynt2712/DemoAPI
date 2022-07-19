using BlogWebApi.Data;

namespace BlogWebApi.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task SaveAsync();

        BlogDBContext GetDbContext();
    }
}
