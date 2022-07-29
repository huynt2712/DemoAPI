using BlogWebApi.Helper;
using BlogWebApi.ViewModel;
using BlogWebApi.ViewModel.Post;

namespace BlogWebApi.Services.Interface
{
    public interface IPostService
    {
        Task<PagedList<PostViewModel>> GetAllPostAsync(PostRequestModel postRequestModel);
        //Task<List<PostViewModel>> GetAllPostAsync(PostRequestModel postRequestModel);
        Task<PostViewModel?> GetPostByIdAsync(int id);
        Task <int?> AddPostAsync(AddPostViewModel addPostViewModel);
        Task <int> UpdatePostAsync(int id, UpdatePostViewModel updatePostViewModel);
        Task <int> DeletePostAsync(int id);
        Task TestUnitOfWork();
    }
}
