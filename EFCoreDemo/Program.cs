// See https://aka.ms/new-console-template for more information
using EFCoreDemo.Model;
using System.Text;

//Console.WriteLine("Hello, World!");
Console.OutputEncoding = Encoding.Unicode;
Console.InputEncoding = Encoding.Unicode;


static void PrintAllPost()
{
    var context = new ApiDBContext();
    var postList = context.Posts.Select(p => p);
    foreach (var post in postList)
    {
        Console.WriteLine("Post Id: {0}, Name: {1}, Description: {2}", post.Id, post.Name, post.Description);
    }
}

PrintAllPost();

//static void PrintPostId(int id)
//{
//    var context = new ApiDBContext();
//    var idList = context.Posts.FirstOrDefault(p => p.Id == id);
//    if(idList != null)
//    {
//        Console.WriteLine("Post Id: {0}, Name: {1}, Description: {2}", idList.Id, idList.Name, idList.Description);
//    }
//}

//PrintPostId(2);


//var AddPostModel = new Post()
//{
//    Name = "Call of duty",
//    Description = "Game of the year"
//};

//static void AddPost(Post addpost)
//{
//    var context = new ApiDBContext();
//    context.Posts.Add(addpost);
//    context.SaveChanges();
//}

//AddPost(AddPostModel);


//static void UpdatePost(int id, string Name, string Description)
//{
//    var context = new ApiDBContext();
//    var updatePost = context.Posts.FirstOrDefault(p => p.Id == id);
//    if (updatePost != null)
//    {
//        updatePost.Name = Name;
//        updatePost.Description = Description;
//        context.SaveChanges();
//    }
//}

//UpdatePost(5, "Sửa lại post", "Đã hoàn tất");


//static void RemovePost(int id)
//{
//    var context = new ApiDBContext();
//    var removePost = context.Posts.FirstOrDefault(p => p.Id == id);
//    if (removePost != null)
//    {
//        context.Posts.Remove(removePost);
//        context.SaveChanges();
//    }
//}

//RemovePost(6);

//Add Database
//var newPost = new Post()
//{
//    Name = "Post 3",
//    Description = "Xe BMW 1000RR"
//};

//context.Posts.Add(newPost);
//context.SaveChanges();


//Update database
//var updatePost = context.Posts.FirstOrDefault(p => p.Name == "Post 3");
//if(updatePost != null)
//{
//    updatePost.Description = "Xe BMW 1000RR cá voi sát thủ";
//    context.SaveChanges();
//}



//Remove database
//var removePost = context.Posts.Where(p => p.Id == 4 && p.Name=="Post 3").FirstOrDefault();
//if(removePost != null)
//{
//    context.Posts.Remove(removePost);
//    context.SaveChanges();
//}


Console.ReadLine();