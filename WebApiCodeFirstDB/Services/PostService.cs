﻿using BlogApi.Data;
using BlogApi.Models;
using BlogApi.Services.Interface;
using BlogApi.ViewModel;

namespace BlogApi.Services
{
    //Logic related post
    public class PostService : IPostService
    {
        public int AddPost(AddPostViewModel addPostViewModel)
        {
            using (var context = new BlogDBContext())
            {
                var newPost = context.Posts.Add(new Post
                {
                    Content = addPostViewModel.Content,
                    Title = addPostViewModel.Title,
                    Description = addPostViewModel.Description,
                    PostCategoryId = addPostViewModel.PostCategoryId,
                    Slug = addPostViewModel.Slug,
                    Image = addPostViewModel.Image,
                    CreatedDate = DateTime.UtcNow
                });
                context.SaveChanges();
                return 1;
            }
        }

        public int DeletePost(int id)
        {
            using (var context = new BlogDBContext())
            {
                var post = context.Posts.FirstOrDefault(c => c.Id == id);
                if (post == null)
                {
                    return 0;
                }
                context.Remove(post);
                context.SaveChanges();
                return post.Id;
            }
        }

        public List<PostViewModel> GetAllPost()
        {
            var post = new List<PostViewModel>();
            using(var context = new BlogDBContext())
            {
                post = context.Posts.Select(p => new PostViewModel
                {
                    Id = p.Id,
                    Content = p.Content,
                    CreatedDate = p.CreatedDate,
                    Description = p.Description,
                    Title = p.Title,
                    Image = p.Image
                }).ToList();
            }
            return post;
        }

        public PostViewModel? GetPostById(int id)
        {
            var post = new PostViewModel();
            using (var context = new BlogDBContext())
            {
                post = context.Posts.Select(p => new PostViewModel
                {
                    Id = p.Id,
                    Content = p.Content,
                    CreatedDate = p.CreatedDate,
                    Description = p.Description,
                    Title = p.Title,
                    Image = p.Image
                }).FirstOrDefault(p => p.Id == id);
                return post;
            }
        }

        public int UpdatePost(int id, UpdatePostViewModel updatePostViewModel)
        {
            using (var context = new BlogDBContext())
            {
                var post = context.Posts.FirstOrDefault(c => c.Id == id);
                if (post == null)
                {
                    return 0;
                }
                post.Title = updatePostViewModel.Title;
                post.Description = updatePostViewModel.Description;
                post.Content = updatePostViewModel.Content;
                post.PostCategoryId = updatePostViewModel.PostCategoryId;
                post.Image = updatePostViewModel.Image;
                post.UpdatedDate = DateTime.UtcNow;
                context.SaveChanges();
                return post.Id;
            }
        }
    }
}
