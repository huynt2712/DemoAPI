// See https://aka.ms/new-console-template for more information
//Lập trình đồng bộ (synchronous)
//cách lập trình mà các hoạt động của chương trình sẽ được thực hiện tuần tự.
//Hành động trước xong thì hành động sau mới được thực hiện.

using Asynchronous_Synchronous;
using System.Diagnostics;
using System.Net;

/*
////Lập trình đồng bộ 
var url = "https://github.com/namndwebdev/html-css-js-thuc-chien/blob/main/Counter%20Up/index.html";
var file = DownloadFileSynchronous(url);
Console.WriteLine("Làm gì đó khi file đang tải");
Console.WriteLine($"File có độ dài {file.Length}");
Console.WriteLine("Làm gì đó khi file tải xong");
//Lập trình bất đồng bộ
var url = "https://github.com/namndwebdev/html-css-js-thuc-chien/blob/main/Counter%20Up/index.html";
var fileTask = DownloadFileAsynchronous(url);
Console.WriteLine("Làm gì đó khi file đang tải");
var file = await fileTask;
Console.WriteLine($"File có độ dài {file.Length}");
Console.WriteLine("Làm gì đó khi file tải xong");
*/

/*
var synchronousExample = new SynchronousExample();
synchronousExample.CallSynchronousFunction(); //đồng bộ
*/

var asynchronousExample = new AsynchronousExample();
await asynchronousExample.CallWhenAnyAsync();

Console.ReadLine();



