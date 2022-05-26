using BlogWebApi.Data;
using BlogWebApi.Models;
using BlogWebApi.Services.Interface;
using BlogWebApi.ViewModel;

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

        public List<PostViewModel> GetAllPost()
        {
            var post = new List<PostViewModel>();
            post = _blogDBContext.Posts.Select(p => new PostViewModel
            {
                Id = p.Id,
                Content = p.Content,
                CreatedDate = p.CreatedDate,
                Description = p.Description,
                Title = p.Title,
                Image = p.Image
            }).ToList();
            return post;
        }

        public int AddPost(AddPostViewModel addPostViewModel)
        {
            
                var newPost = _blogDBContext.Posts.Add(new Post
                {
                    Content = addPostViewModel.Content,
                    Title = addPostViewModel.Title,
                    Description = addPostViewModel.Description,
                    PostCategoryId = addPostViewModel.PostCategoryId,
                    Slug = addPostViewModel.Slug,
                    Image = addPostViewModel.Image,
                    CreatedDate = DateTime.UtcNow
                });
                _blogDBContext.SaveChanges();
                return 1;
        }

        public int DeletePost(int id)
        {
            
                var post = _blogDBContext.Posts.FirstOrDefault(c => c.Id == id);
                if (post == null)
                {
                    return 0;
                }
                _blogDBContext.Remove(post);
                _blogDBContext.SaveChanges();
                return post.Id;
        }

        public PostViewModel? GetPostById(int id)
        {
            var post = new PostViewModel();
            
                post = _blogDBContext.Posts.Select(p => new PostViewModel
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

        public int UpdatePost(int id, UpdatePostViewModel updatePostViewModel)
        {
                var post = _blogDBContext.Posts.FirstOrDefault(c => c.Id == id);
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
                _blogDBContext.SaveChanges();
                return post.Id;
        }
    }
}
