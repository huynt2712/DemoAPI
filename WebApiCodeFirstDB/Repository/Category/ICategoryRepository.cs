using BlogWebApi.Entites;
using BlogWebApi.ViewModel.Category;

namespace BlogWebApi.Repository.Category
{
    public interface ICategoryRepository
    {
        IEnumerable<PostCategory> Gets();

        IQueryable<PostCategory> Gets(CategoryRequestModel requestModel);

        Task<PostCategory?> GetByIdAsync(int id);

        Task<PostCategory?> AddAsync(PostCategory postCategory);

        Task<int> UpdateAsync(int id, PostCategory updateCategory);

        Task<int> DeleteAsync(int id);

        Task SaveAsync();
    }
}
