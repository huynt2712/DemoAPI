using BlogWebApi.Data;
using BlogWebApi.Entites;
using BlogWebApi.Helper;
using BlogWebApi.Services.Interface;
using BlogWebApi.ViewModel;
using BlogWebApi.ViewModel.Post;
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

        public async Task <PagedList<PostViewModel>> GetAllPostAsync(PostRequestModel postRequestModel)
        {
            var post = _blogDBContext.Posts.Select(p => new PostViewModel
            {
                Id = p.Id,
                Content = p.Content,
                CreatedDate = p.CreatedDate,
                UpdateDate = p.UpdatedDate,
                Description = p.Description,
                Title = p.Title,
                Slug = p.Slug,
                ImagePath = p.ImagePath,
                PostCategoryId = p.PostCategoryId
            });

            if (!string.IsNullOrWhiteSpace(postRequestModel.SearchText))
            {
                var searchText = postRequestModel.SearchText.ToLower();
                post = post.Where(p => (p.Title != null
                && p.Title.ToLower().Contains(searchText))
                || (p.Description != null && p.Description.ToLower().Contains(searchText))
                || (p.Content != null && p.Content.ToLower().Contains(searchText))
                || (p.Slug != null && p.Slug.ToLower().Contains(searchText)));
            }

            post = post.OrderBy(p => p.Title); //created date/update date
            return await PagedList<PostViewModel>.ToPagedListAsync(post,
                postRequestModel.PageNumber, postRequestModel.PageSize);
        }

        public async Task <int?> AddPostAsync(AddPostViewModel addPostViewModel)
        {
            //Validate parameter of function
            var category = await _blogDBContext.Categories
                .FirstOrDefaultAsync(c => c.Id == addPostViewModel.PostCategoryId);
            if (category == null)
                return -2;

            var isExisting = await _blogDBContext.Posts
               .AnyAsync(p => p.Title == addPostViewModel.Title || p.Description == addPostViewModel.Description
               || p.Content == addPostViewModel.Content || p.Slug == addPostViewModel.Slug);
            if (isExisting)
                return -1;

            var newPost = await _blogDBContext.Posts.AddAsync(new Post
            {
                Content = addPostViewModel.Content,
                Title = addPostViewModel.Title,
                Description = addPostViewModel.Description,
                PostCategoryId = addPostViewModel.PostCategoryId,
                Slug = addPostViewModel.Slug,
                ImagePath = addPostViewModel.ImagePath,
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
                Slug = p.Slug,
                Description = p.Description,
                Title = p.Title,
                ImagePath = p.ImagePath
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
            post.Slug = updatePostViewModel.Slug;
            post.PostCategoryId = updatePostViewModel.PostCategoryId;
            post.ImagePath = updatePostViewModel.ImagePath;
            post.UpdatedDate = DateTime.UtcNow;
            await _blogDBContext.SaveChangesAsync();
            return post.Id;
        }
    }
}
