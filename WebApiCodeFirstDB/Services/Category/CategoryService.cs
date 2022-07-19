using BlogWebApi.Data;
using BlogWebApi.Helper;
using BlogWebApi.Entites;
using BlogWebApi.Services.Interface;
using BlogWebApi.ViewModel;
using BlogWebApi.ViewModel.Category;
using Microsoft.EntityFrameworkCore;
using BlogWebApi.Repository.Category;
using BlogWebApi.UnitOfWork;

namespace BlogWebApi.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly BlogDBContext _blogDBContext;
        private readonly ICategoryRepository categoryRepository;
        private readonly IUnitOfWork unitOfWork;

        public CategoryService(BlogDBContext blogDBContext, ICategoryRepository categoryRepository,
            IUnitOfWork unitOfWork)
        {
            _blogDBContext = blogDBContext;
            this.categoryRepository = categoryRepository;
            this.unitOfWork = unitOfWork;
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
            var isExisting = await _blogDBContext.Categories
                .AnyAsync(x => x.Name == postCategory.Name || x.Slug == postCategory.Slug);

            if (isExisting)
                return -1;

            var newCategory = await categoryRepository.AddAsync(new PostCategory
            {
                Name = postCategory.Name,
                Slug = postCategory.Slug,
                CreateAt = DateTime.UtcNow
            });
            await unitOfWork.SaveAsync();
            //var newCategory = await _blogDBContext.Categories.AddAsync(new PostCategory
            //{
            //    Name = postCategory.Name,
            //    Slug = postCategory.Slug,
            //    CreateAt = DateTime.UtcNow 
            //});
            //await _blogDBContext.SaveChangesAsync();
            if (newCategory == null) return -1;

            return newCategory.Id;
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
