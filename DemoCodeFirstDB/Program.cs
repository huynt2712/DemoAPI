// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");



using DemoCodeFirstDB.Model;
using Microsoft.EntityFrameworkCore;
using System.Text;


Console.OutputEncoding = Encoding.Unicode;
Console.InputEncoding = Encoding.Unicode;
var PostDbContext = new PostContext();

var post = PostDbContext.Categories.FirstOrDefault(s => s.Id == 1);

//if (post != null)
//{
//    var postList = post.Posts.ToList();
//    Console.WriteLine(postList.Count);
//}

//var post = PostDbContext.Posts;
//var category = PostDbContext.Categories.Where(s => s.NameCategory == "Cong nghe");
//    .Include(g => g.Posts).FirstOrDefault();


//   Console.WriteLine(category);

//var postList = PostDbContext.Categories.Where(x => x.CategoryId == 1).ToList();



var postList = post?.Posts;
foreach (var posts in postList)
{
    Console.WriteLine("{0},{1},{2}",posts.PostId, posts.Title,posts.Description);
}
//Console.WriteLine(postList.Count);
