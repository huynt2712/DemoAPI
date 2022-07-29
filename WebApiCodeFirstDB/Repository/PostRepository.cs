using BlogWebApi.Data;
using BlogWebApi.Entites;
using BlogWebApi.UnitOfWork;

namespace BlogWebApi.Repository
{
    public class PostRepository : IPostRepository
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly BlogDBContext _blogDBContext;

        public PostRepository(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            _blogDBContext = unitOfWork.GetDBContext();
        }


        public async Task<Post?> AddPostAsync(Post post)
        {
            var newPost = await unitOfWork.GetDBContext().Posts.AddAsync(post);
            return newPost.Entity;
        }

        public async Task SaveAsync()
        {
            await unitOfWork.SaveAsync();
        }
    }
}
