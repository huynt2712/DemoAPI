using BlogWebApi.Data;
using BlogWebApi.Models;
using BlogWebApi.Services.Interface;

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
        //logic
        public List<PostCategory> GetAllCategory()
        {
            var postCategories = new List<PostCategory>();
            postCategories = _blogDBContext.Categories.ToList();
   

            return postCategories;
        }

        public PostCategory? GetCategoryById(int id)
        {
            var category = new PostCategory();
                category = _blogDBContext.Categories.FirstOrDefault(c => c.Id == id);

            return category;
        }

        public int AddCagtegory(PostCategory postCategory)
        {
                _blogDBContext.Categories.Add(postCategory);
                _blogDBContext.SaveChanges();

            return postCategory.Id;
        }

        public int DeleteCategory(int id)
        {

                var category = _blogDBContext.Categories.FirstOrDefault(c => c.Id == id);
                if (category == null)
                {
                    return 0;
                }
                _blogDBContext.Remove(category);
                _blogDBContext.SaveChanges();
                return category.Id;
        }

        public int UpdateCategory(int id, PostCategory updateCategory)
        {
                var category = _blogDBContext.Categories.FirstOrDefault(c => c.Id == id);
                if (category == null)
                {
                    return 0;
                }
                category.Name = updateCategory.Name;
                category.Slug = updateCategory.Slug;
                category.UpdateAt = DateTime.UtcNow;
                _blogDBContext.SaveChanges();
                return category.Id;
        }
    }
}
