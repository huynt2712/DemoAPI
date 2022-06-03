using BlogWebApi.Models;
using BlogWebApi.ViewModel;

namespace BlogWebApi.Services.Interface
{
    public interface ICategoryService
    {
        Task<List<CategoryViewModel>> GetAllCategoryAsync();
        Task<CategoryViewModel?> GetCategoryByIdAsync(int id);

        Task<int> AddCagtegoryAsync(AddCategoryViewModel postCategory);

        Task<int> DeleteCategoryAsync(int id);

        Task<int> UpdateCategoryAsync(int id, UpdateCategoryViewModel updateCategory);
    }
}
