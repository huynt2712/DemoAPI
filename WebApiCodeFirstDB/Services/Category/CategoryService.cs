using BlogWebApi.Data;
using BlogWebApi.Helper;
using BlogWebApi.Models;
using BlogWebApi.Services.Interface;
using BlogWebApi.ViewModel;
using BlogWebApi.ViewModel.Category;
using Microsoft.EntityFrameworkCore;

namespace BlogWebApi.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly BlogDBContext _blogDBContext;

        public CategoryService(BlogDBContext blogDBContext)
        {
            _blogDBContext = blogDBContext;
        }

        public async Task<PagedList<CategoryViewModel>> GetsAsync(CategoryRequestModel requestModel)
        {
            var postCategories = _blogDBContext.Categories
                .Select(c => new CategoryViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    Slug = c.Slug,
                    CreateAt = c.CreateAt,
                    UpdateAt = c.UpdateAt
                });

            //Filter
            if(!string.IsNullOrWhiteSpace(requestModel.SearchText))
            {
                var searchText = requestModel.SearchText.ToLower();
                postCategories = postCategories.Where(c => (c.Name != null 
                && c.Name.ToLower().Contains(searchText))
                || (c.Slug != null && c.Slug.ToLower().Contains(searchText)));
            }

            //Paging
            postCategories = postCategories
                .OrderByDescending(c => c.Name);

            return await PagedList<CategoryViewModel>.ToPagedListAsync(postCategories,
                requestModel.PageNumber, requestModel.PageSize);
        }

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
                CreateAt = DateTime.UtcNow 
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
