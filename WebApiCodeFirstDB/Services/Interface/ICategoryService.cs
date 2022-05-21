using BlogWebApi.Models;

namespace BlogWebApi.Services.Interface
{
    //logic
    public interface ICategoryService
    {
        List<PostCategory> GetAllCategory();

        PostCategory? GetCategoryById(int id);

        int AddCagtegory(PostCategory postCategory);

        int UpdateCategory(int id, PostCategory updateCategory);

        int DeleteCategory(int id);
    }
}
