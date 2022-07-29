
using BlogWebApi.Entites;
using BlogWebApi.ViewModel;
using BlogWebApi.ViewModel.Category;

namespace BlogWebApi.Repository
{
    public interface ICategoryRepository //tách xử lý data riêng (dbcontext, entity framework core, database)
    {
        IQueryable<PostCategory> GetAll();

        Task<PostCategory?> GetCategoryByIdAsync(int id);

        Task<PostCategory?> AddCagtegoryAsync(PostCategory postCategory);

        Task<bool> IsExisting(string name, string slug);

        void DeleteCategory(PostCategory category);

        void UpdateCategory(PostCategory postCategory, string name, string slug);

        Task SaveAsync();

    }
}
