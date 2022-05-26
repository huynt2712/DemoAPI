// See https://aka.ms/new-console-template for more information
//Lập trình đồng bộ (synchronous)
//cách lập trình mà các hoạt động của chương trình sẽ được thực hiện tuần tự.
//Hành động trước xong thì hành động sau mới được thực hiện.

using System.Diagnostics;
using System.Net;

//var watch = new Stopwatch();
//watch.Start();

//ActionOne(); //5s
//ActionTwo(); //2s
//ActionThree(); //2s

////=> 9s

//watch.Stop();
//Console.WriteLine($"Execution time: {watch.ElapsedMilliseconds} ms");

//void ActionOne()
//{

//    //Download file 
//    Thread.Sleep(5000); //5s
//    Console.WriteLine("Action one");
//}


//void ActionTwo()
//{
//    Thread.Sleep(100000); //2s
//    Console.WriteLine("Action two");
//}

//void ActionThree()
//{
//    Thread.Sleep(2000); //2s
//    Console.WriteLine("Action three");
//}


////Lập trình đồng bộ 
//var url = "https://github.com/namndwebdev/html-css-js-thuc-chien/blob/main/Counter%20Up/index.html";
//var file = DownloadFileSynchronous(url);
//Console.WriteLine("Làm gì đó khi file đang tải");
//Console.WriteLine($"File có độ dài {file.Length}");
//Console.WriteLine("Làm gì đó khi file tải xong");

string DownloadFileSynchronous(string path)
{
    WebClient webClient = new WebClient();
    var file = webClient.DownloadString(path);
    Thread.Sleep(9000); //Giả sử việc download tốn 9s

    Console.WriteLine("Đã hoàn thành việc tải file");
    return file;
}
//Lập trình bất đồng bộ

var url = "https://github.com/namndwebdev/html-css-js-thuc-chien/blob/main/Counter%20Up/index.html";
var fileTask = DownloadFileAsynchronous(url);
Console.WriteLine("Làm gì đó khi file đang tải");
var file = await fileTask;
Console.WriteLine($"File có độ dài {file.Length}");
Console.WriteLine("Làm gì đó khi file tải xong");

//void task, task<T> 
async Task<string> DownloadFileAsynchronous(string path)
{
    HttpClient client = new HttpClient();
    var fileTask = await client.GetStringAsync(path);
    Thread.Sleep(9000); //Giả sử việc download tốn 9s
    Console.WriteLine("Đã hoàn thành việc tải file");
    return fileTask;
}

//thread
//thread x chạy DownloadFileAsynchronous
//thread y Console.WriteLine("Làm gì đó khi file đang tải");