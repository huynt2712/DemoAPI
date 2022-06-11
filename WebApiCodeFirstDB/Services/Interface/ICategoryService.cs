using BlogWebApi.Helper;
using BlogWebApi.Entites;
using BlogWebApi.ViewModel;
using BlogWebApi.ViewModel.Category;

namespace BlogWebApi.Services.Interface
{
    public interface ICategoryService
    {
        Task<PagedList<CategoryViewModel>> GetsAsync(CategoryRequestModel requestModel);
        Task<CategoryViewModel?> GetCategoryByIdAsync(int id);

        Task<int> AddCagtegoryAsync(AddCategoryViewModel postCategory);

        Task<int> DeleteCategoryAsync(int id);

        Task<int> UpdateCategoryAsync(int id, UpdateCategoryViewModel updateCategory);
    }
}
