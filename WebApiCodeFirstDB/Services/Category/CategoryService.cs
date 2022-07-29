using BlogWebApi.Data;
using BlogWebApi.Helper;
using BlogWebApi.Entites;
using BlogWebApi.Services.Interface;
using BlogWebApi.ViewModel;
using BlogWebApi.ViewModel.Category;
using Microsoft.EntityFrameworkCore;
using BlogWebApi.Repository;

namespace BlogWebApi.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly BlogDBContext _blogDBContext;
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(BlogDBContext blogDBContext,
            ICategoryRepository categoryRepository)
        {
            _blogDBContext = blogDBContext;
            _categoryRepository = categoryRepository;
        }

        public async Task<PagedList<CategoryViewModel>> GetsAsync(CategoryRequestModel requestModel)
        {
            var categoryEntities = _categoryRepository.GetAll();
            var postCategories = categoryEntities
                .Select(c => new CategoryViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    Slug = c.Slug,
                    CreateAt = c.CreateAt,
                    UpdateAt = c.UpdateAt
                });
            if (!string.IsNullOrWhiteSpace(requestModel.SearchText)) //logic
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
            try
            {
                var categoryEntity = await _categoryRepository.GetCategoryByIdAsync(id); //data access (xử lý data)
                var category = new CategoryViewModel //mapping - business logic
                {
                    Id = categoryEntity.Id,
                    Name = categoryEntity.Name,
                    Slug = categoryEntity.Slug,
                    CreateAt = categoryEntity.CreateAt,
                    UpdateAt = categoryEntity.UpdateAt
                };
                return category;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        //service: business logic(mapping, if else, for, swtich...)
        //+ data access(dbcontext, entity, entity framework)

        public async Task<int> AddCagtegoryAsync(AddCategoryViewModel postCategory)
        {
            var isExisting = await _categoryRepository.IsExisting(postCategory.Name, postCategory.Slug);
            if (isExisting) //logic
                return -1;

            var newCategory = await _categoryRepository.AddCagtegoryAsync(new PostCategory
            {
                Name = postCategory.Name,
                Slug = postCategory.Slug,
                CreateAt = DateTime.UtcNow
            });
            await _categoryRepository.SaveAsync();
            return newCategory.Id;
        }

        public async Task<int> DeleteCategoryAsync(int id)
        {
            //logic

            var category = await _categoryRepository.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return 0;
            }
            _categoryRepository.DeleteCategory(category);
            await _categoryRepository.SaveAsync();
            return category.Id;
        }

        public async Task<int> UpdateCategoryAsync(int id, UpdateCategoryViewModel updateCategory)
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return 0;
            }
            _categoryRepository.UpdateCategory(category, updateCategory.Name, updateCategory.Slug);
            await _categoryRepository.SaveAsync();
            return id;
        }
    }
}
