using BlogWebApi.Data;
using BlogWebApi.Entites;
using BlogWebApi.Helper;
using BlogWebApi.Repository;
using BlogWebApi.Services.Interface;
using BlogWebApi.UnitOfWork;
using BlogWebApi.ViewModel;
using BlogWebApi.ViewModel.Post;
using Microsoft.EntityFrameworkCore;

namespace BlogWebApi.Services
{
    //Logic related post
    public class PostService : IPostService
    {
        private readonly BlogDBContext _blogDBContext;
        private readonly IPostRepository postRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly IUnitOfWork unitOfWork;

        public PostService(BlogDBContext blogDBContext, IPostRepository postRepository,
            ICategoryRepository categoryRepository,
            IUnitOfWork unitOfWork)
        {
            _blogDBContext = blogDBContext;
            this.postRepository = postRepository;
            this.categoryRepository = categoryRepository;
            this.unitOfWork = unitOfWork;
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

            //Iqueryable xử lý trên database
            var queryableData = _blogDBContext.Posts.Where(p => p.Id == 1)
                .Select(p => p.Content); //10000 items

            //IEnumerable  xử lý trên bộ nhớ (RAM)
            var enumerableData = queryableData.ToList();
            //Iqueryable => IEnumerable
            var queryEnumerableData = enumerableData.Where(p => p.Contains("x"))
                .Select(x => x); //Xử lý ở bộ nhớ (RAM)



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

            var newPost = await postRepository.AddPostAsync(new Post
            {
                Content = addPostViewModel.Content,
                Title = addPostViewModel.Title,
                Description = addPostViewModel.Description,
                PostCategoryId = addPostViewModel.PostCategoryId,
                Slug = addPostViewModel.Slug,
                ImagePath = addPostViewModel.ImagePath,
                CreatedDate = DateTime.UtcNow
            });
            await postRepository.SaveAsync();
            return newPost?.Id;
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
    
        public async Task TestUnitOfWork()
        {
            //--1. Add new category.
            var category = await categoryRepository.AddCagtegoryAsync(new PostCategory
            {
                Name = "Name",
                Slug = "Slug",
                CreateAt = DateTime.UtcNow
            }); //1 insert sucessfull => entityes of dbcontext
            //--2. Add new post of category. 
            //await _blogDBContext.SaveChangesAsync();

            await postRepository.AddPostAsync(new Post
            {
                Content ="Test Content",
                Title ="Test tile",
                Description = "Test description",
                //PostCategory = category,
                PostCategoryId = 0,
                Slug = "Slug",
                ImagePath = "ImagePath",
                CreatedDate = DateTime.UtcNow
            }); //2 - error PostCategoryId = null => exception

            //await _blogDBContext.SaveChangesAsync();

            //1, 2 lúc này chưa có thêm dữ liệu database
            //await _blogDBContext.SaveChangesAsync(); //Lúc này dữ liệu thêm xuống database
            //await categoryRepository.SaveAsync();
            //await postRepository.SaveAsync();
            await unitOfWork.SaveAsync();

        }
    }
}
