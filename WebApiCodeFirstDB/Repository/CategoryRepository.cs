using BlogWebApi.Data;
using BlogWebApi.Entites;
using BlogWebApi.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace BlogWebApi.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly BlogDBContext _blogDBContext;

        public CategoryRepository(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            _blogDBContext = unitOfWork.GetDBContext();
        }
        public async Task<PostCategory?> GetCategoryByIdAsync(int id)
        {
            try
            {
                var category = await _blogDBContext.Categories
                     .FirstOrDefaultAsync(c => c.Id == id);
                return category;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<PostCategory?> AddCagtegoryAsync(PostCategory postCategory)
        {
            var newCategory = await unitOfWork.GetDBContext().Categories.AddAsync(postCategory); //data access
            return newCategory.Entity;
        }

        public async Task SaveAsync()
        {
            await unitOfWork.SaveAsync(); //data acess
        }

        public async Task<bool> IsExisting(string name, string slug)
        {
            var isExisting = await _blogDBContext.Categories
               .AnyAsync(x => x.Name == name || x.Slug == slug); //data access
            return isExisting;
        }

        public void DeleteCategory(PostCategory category)
        {
            _blogDBContext.Remove(category);
        }

        public void UpdateCategory(PostCategory category, string name, string slug)
        {
            category.Name = name;
            category.Slug = slug;
            category.UpdateAt = DateTime.UtcNow;
        }

        public IQueryable<PostCategory> GetAll()
        {
            return _blogDBContext.Categories;
        }
    }
}
