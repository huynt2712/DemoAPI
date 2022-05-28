using BlogWebApi.Models;

namespace BlogWebApi.Services.Interface
{
    public interface ICategoryService
    {
        Task<List<PostCategory>> GetAllCategoryAsync();
        Task<PostCategory?> GetCategoryByIdAsync(int id);

        Task<int> AddCagtegoryAsync(PostCategory postCategory);

        Task<int> DeleteCategoryAsync(int id);

        Task<int> UpdateCategoryAsync(int id, PostCategory updateCategory);
    }
}
