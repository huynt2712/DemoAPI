using BlogWebApi.Data;
using BlogWebApi.Entites;
using BlogWebApi.UnitOfWork;
using BlogWebApi.ViewModel.Category;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace BlogWebApi.Repository.Category
{
    public class CategoryRepository : ICategoryRepository
    {
        //private readonly BlogDBContext _blogDBContext;
        private readonly IUnitOfWork unitOfWork;
        private readonly BlogDBContext _blogDBContext;

        //public CategoryRepository(BlogDBContext blogDBContext)
        //{
        //    _blogDBContext = blogDBContext;
        //}

        public CategoryRepository(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            _blogDBContext = unitOfWork.GetDbContext();
        }


        public IEnumerable<PostCategory> Gets()
        {
            return _blogDBContext.Categories.ToList();
        }

        public IQueryable<PostCategory> Gets(CategoryRequestModel requestModel)
        {
            var postCategories = _blogDBContext.Categories.AsQueryable();

            //Filter
            if (!string.IsNullOrWhiteSpace(requestModel.SearchText))
            {
                var searchText = requestModel.SearchText.ToLower();
                postCategories = postCategories.Where(c => (c.Name != null
                && c.Name.ToLower().Contains(searchText))
                || (c.Slug != null && c.Slug.ToLower().Contains(searchText)));
            }

            //Paging
            postCategories = postCategories
                .OrderByDescending(c => c.Name);

            return postCategories;
        }

        public async Task<PostCategory?> GetByIdAsync(int id)
        {
            var category = await _blogDBContext.Categories
                .FirstOrDefaultAsync(c => c.Id == id);
            return category;
        }

        public async Task<PostCategory?> AddAsync(PostCategory postCategory)
        {
            var isExisting = await _blogDBContext.Categories
                .AnyAsync(x => x.Name == postCategory.Name || x.Slug == postCategory.Slug);
            if (isExisting)
                return null;
            var newCategory = await _blogDBContext.Categories.AddAsync(postCategory);
            return newCategory.Entity;
        }

        public async Task<int> UpdateAsync(int id, PostCategory updateCategory)
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
            return category.Id;
        }

        public async Task<int> DeleteAsync(int id)
        {
            var category = await _blogDBContext.Categories
                .FirstOrDefaultAsync(c => c.Id == id);
            if (category == null)
            {
                return 0;
            }
            _blogDBContext.Remove(category);
            return category.Id;
        }

        public async Task SaveAsync()
        {
            await unitOfWork.SaveAsync();
        }
    }
}
