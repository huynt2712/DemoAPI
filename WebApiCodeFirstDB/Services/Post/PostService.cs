using BlogWebApi.Data;
using BlogWebApi.Models;
using BlogWebApi.Services.Interface;
using BlogWebApi.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace BlogWebApi.Services
{
    //Logic related post
    public class PostService : IPostService
    {
        private readonly BlogDBContext _blogDBContext;

        public PostService(BlogDBContext blogDBContext)
        {
            _blogDBContext = blogDBContext;
        }

        public async Task <List<PostViewModel>> GetAllPostAsync()
        {
            var post = new List<PostViewModel>();
            post = await _blogDBContext.Posts.Select(p => new PostViewModel
            {
                Id = p.Id,
                Content = p.Content,
                CreatedDate = p.CreatedDate,
                Description = p.Description,
                Title = p.Title,
                Image = p.Image
            }).ToListAsync();
            return post;
        }

        public async Task <int?> AddPostAsync(AddPostViewModel addPostViewModel)
        {
            //Validate parameter of function
            var category = await _blogDBContext.Categories
                .FirstOrDefaultAsync(c => c.Id == addPostViewModel.PostCategoryId);
            if (category == null)
                return -1;

            var newPost = await _blogDBContext.Posts.AddAsync(new Post
            {
                Content = addPostViewModel.Content,
                Title = addPostViewModel.Title,
                Description = addPostViewModel.Description,
                PostCategoryId = addPostViewModel.PostCategoryId,
                Slug = addPostViewModel.Slug,
                Image = addPostViewModel.Image,
                CreatedDate = DateTime.UtcNow
            });
            await _blogDBContext.SaveChangesAsync();
            return newPost?.Entity?.Id;
        }

        public async Task <int> DeletePostAsync(int id)
        {

            var post = await _blogDBContext.Posts.FirstOrDefaultAsync(c => c.Id == id);
            if (post == null)
            {
                return 0;
            }
            _blogDBContext.Remove(post);
            await _blogDBContext.SaveChangesAsync();
            return post.Id;
        }

        public async Task <PostViewModel?> GetPostByIdAsync(int id)
        {
            var post = new PostViewModel();

            post = await _blogDBContext.Posts.Select(p => new PostViewModel
            {
                Id = p.Id,
                Content = p.Content,
                CreatedDate = p.CreatedDate,
                Description = p.Description,
                Title = p.Title,
                Image = p.Image
            }).FirstOrDefaultAsync(p => p.Id == id);
            return post;
        }

        public async Task <int> UpdatePostAsync(int id, UpdatePostViewModel updatePostViewModel)
        {
            var post = await _blogDBContext.Posts.FirstOrDefaultAsync(c => c.Id == id);
            if (post == null)
            {
                return -1;
            }
            //Validate parameter of function
            var category = await _blogDBContext.Categories
                .FirstOrDefaultAsync(c => c.Id == updatePostViewModel.PostCategoryId);
            if (category == null)
                return -1;

            post.Title = updatePostViewModel.Title;
            post.Description = updatePostViewModel.Description;
            post.Content = updatePostViewModel.Content;
            post.PostCategoryId = updatePostViewModel.PostCategoryId;
            post.Image = updatePostViewModel.Image;
            post.UpdatedDate = DateTime.UtcNow;
            await _blogDBContext.SaveChangesAsync();
            return post.Id;
        }
    }
}
