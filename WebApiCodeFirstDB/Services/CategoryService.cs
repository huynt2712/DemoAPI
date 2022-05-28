using BlogWebApi.Data;
using BlogWebApi.Models;
using BlogWebApi.Services.Interface;
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

        public async Task<List<PostCategory>> GetAllCategoryAsync()
        {
            var postCategories = new List<PostCategory>();
            postCategories = await _blogDBContext.Categories.ToListAsync();
            return postCategories;
        }

        //Task => 10 users 10 request GetAllAsync()
        //Synchronous request 1, request 2,...request 10 
        //Asyncrhonous request 1, request 2,...request 10

        //task ko đợi nhau. 
        //Thread 1 Asyncrhonous => swich thread, scheduler thread thread 1
        public async Task<PostCategory?> GetCategoryByIdAsync(int id)
        {
            var category = await _blogDBContext.Categories
                .FirstOrDefaultAsync(c => c.Id == id);
            return category;
        }

        public async Task<int> AddCagtegoryAsync(PostCategory postCategory)
        {
            await _blogDBContext.Categories.AddAsync(postCategory);
            await _blogDBContext.SaveChangesAsync();
            return postCategory.Id;
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

        public async Task<int> UpdateCategoryAsync(int id, PostCategory updateCategory)
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
