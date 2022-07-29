using BlogWebApi.Entites;

namespace BlogWebApi.Repository
{
    public interface IPostRepository
    {
        Task<Post?> AddPostAsync(Post post);

        Task SaveAsync();
    }
}
