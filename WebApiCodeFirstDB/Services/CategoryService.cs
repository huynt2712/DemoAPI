using BlogWebApi.Data;
using BlogWebApi.Models;
using BlogWebApi.Services.Interface;
using BlogWebApi.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace BlogWebApi.Services
{
    //Logic related category
    public class CategoryService : ICategoryService
    {
        private readonly BlogDBContext _blogDBContext;

        public CategoryService(BlogDBContext blogDBContext)
        {
            _blogDBContext = blogDBContext;
        }

        public async Task<List<CategoryViewModel>> GetAllCategoryAsync()
        {
            var postCategories = new List<CategoryViewModel>();
            postCategories = await _blogDBContext.Categories
                .Select(c => new CategoryViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    Slug = c.Slug,
                    CreateAt = c.CreateAt,
                    UpdateAt = c.UpdateAt
                })
                .ToListAsync();
            return postCategories;
        }

        //Task => 10 users 10 request GetAllAsync()
        //Synchronous request 1, request 2,...request 10 
        //Asyncrhonous request 1, request 2,...request 10

        //task ko đợi nhau. 
        //Thread 1 Asyncrhonous => swich thread, scheduler thread thread 1
        public async Task<CategoryViewModel?> GetCategoryByIdAsync(int id)
        {
            var category = await _blogDBContext.Categories
                 .Select(c => new CategoryViewModel
                 {
                     Id = c.Id,
                     Name = c.Name,
                     Slug = c.Slug,
                     CreateAt = c.CreateAt,
                     UpdateAt = c.UpdateAt
                 })
                .FirstOrDefaultAsync(c => c.Id == id);
            return category;
        }

        public async Task<int> AddCagtegoryAsync(AddCategoryViewModel postCategory)
        {
            var newCategory = await _blogDBContext.Categories.AddAsync(new PostCategory
            {
                Name = postCategory.Name,
                Slug = postCategory.Slug,
                CreateAt = DateTime.UtcNow //UTC 0 (server)
            });
            await _blogDBContext.SaveChangesAsync();
            return newCategory.Entity.Id;
        }

        public async Task<int> DeleteCategoryAsync(int id)
        {

            var category = await _blogDBContext.Categories
                .FirstOrDefaultAsync(c => c.Id == id);
            if (category == null)
            {
                return 0;
            }
            _blogDBContext.Remove(category);
            await _blogDBContext.SaveChangesAsync();
            return category.Id;
        }

        public async Task<int> UpdateCategoryAsync(int id, UpdateCategoryViewModel updateCategory)
        {
            var category = await _blogDBContext.Categories
                .FirstOrDefaultAsync(c => c.Id == id);
            if (category == null)
            {
                return 0;
            }
            category.Name = updateCategory.Name;
            category.Slug = updateCategory.Slug;
            category.UpdateAt = DateTime.UtcNow;
            await _blogDBContext.SaveChangesAsync();
            return category.Id;
        }
    }
}
