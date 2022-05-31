using BlogWebApi.ViewModel;

namespace BlogWebApi.Services.Interface
{
    public interface IPostService
    {
        Task<List<PostViewModel>> GetAllPostAsync();
        Task<PostViewModel?> GetPostByIdAsync(int id);
        Task <int?> AddPostAsync(AddPostViewModel addPostViewModel);
        Task <int> UpdatePostAsync(int id, UpdatePostViewModel updatePostViewModel);
        Task <int> DeletePostAsync(int id);
    }
}
