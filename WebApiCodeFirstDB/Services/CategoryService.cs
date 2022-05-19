using WebApiCodeFirstDB.Data;
using WebApiCodeFirstDB.Models;
using WebApiCodeFirstDB.Services.Interface;

namespace WebApiCodeFirstDB.Services
{
    //Logic related category
    public class CategoryService : ICategoryService
    {
        //logic
        public List<PostCategory> GetAllCategory()
        {
            var postCategories = new List<PostCategory>();
            using (var context = new BlogDBContext())
            {
                postCategories = context.Categories.ToList();
            }

            return postCategories;
        }

        public PostCategory? GetCategoryById(int id)
        {
            var category = new PostCategory();
            using (var context = new BlogDBContext())
            {
                category = context.Categories.FirstOrDefault(c => c.Id == id);
            }

            return category;
        }

        public int AddCagtegory(PostCategory postCategory)
        {
            using (var context = new BlogDBContext())
            {
                context.Categories.Add(postCategory);
                context.SaveChanges();
            }

            return postCategory.Id;
        }

        public int DeleteCategory(int id)
        {
            using (var context = new BlogDBContext())
            {
                var category = context.Categories.FirstOrDefault(c => c.Id == id);
                if (category == null)
                {
                    return 0;
                }
                context.Remove(category);
                context.SaveChanges();
                return category.Id;
            }
        }

        public int UpdateCategory(int id, PostCategory updateCategory)
        {
            using (var context = new BlogDBContext())
            {
                var category = context.Categories.FirstOrDefault(c => c.Id == id);
                if (category == null)
                {
                    return 0;
                }
                category.Name = updateCategory.Name;
                category.Slug = updateCategory.Slug;
                category.UpdateAt = DateTime.UtcNow;
                context.SaveChanges();
                return category.Id;
            }
        }
    }
}
