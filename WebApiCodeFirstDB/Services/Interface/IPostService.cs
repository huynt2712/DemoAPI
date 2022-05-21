using BlogWebApi.ViewModel;

namespace BlogWebApi.Services.Interface
{
    public interface IPostService
    {
        List<PostViewModel> GetAllPost();
        PostViewModel? GetPostById(int id);
        int AddPost(AddPostViewModel addPostViewModel);
        int UpdatePost(int id, UpdatePostViewModel updatePostViewModel);
        int DeletePost(int id);
    }
}
