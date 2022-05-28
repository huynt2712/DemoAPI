using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asynchronous_Synchronous
{
    public class AsynchronousExample
    {
        public async Task<string> DownloadFileAsynchronous(string path)
        {
            HttpClient client = new HttpClient();
            var fileTask = await client.GetStringAsync(path);
            await Task.Delay(9000); //Giả sử việc download tốn 9s
            Console.WriteLine("Đã hoàn thành việc tải file");
            return fileTask;
        }

        public async Task PrintLengthCompleteDownloadStatusAsync(int length)
        {
            await Task.Delay(5000);
            Console.WriteLine($"File tai co do dai {length}");
        }

        public async Task DosomethingWhenDownloadFileAsync()
        {
            await Task.Delay(5000);
            Console.WriteLine("Lam gi do khi dang tai file");
        }

        public async Task CallDownloadFileAsync()
        {
            var url = "https://github.com/huynt2712/DemoAPI/blob/master/.gitattributes";
            var downloadFileTask = DownloadFileAndPrintLengthAsync(url);
            var dosomethingTask = DosomethingWhenDownloadFileAsync();
            await Task.WhenAll(downloadFileTask, dosomethingTask);
        }

        private async Task DownloadFileAndPrintLengthAsync(string url)
        {
            var file =  await DownloadFileAsynchronous(url);
            await PrintLengthCompleteDownloadStatusAsync(file.Length);
        }

        public async Task ActionOneAsyn() //int
        {
            Console.WriteLine("Action One Start");
            await Task.Delay(5000);
            //thread A chờ 5s, function ActionOneAsyn chưa có chạy xong - vẫn đang chạy
            Console.WriteLine("Action One finished");
        }

        public async Task ActionTwoAsyn()
        {
            Console.WriteLine("Action Two Start");
            await Task.Delay(4000);
            //Thread B chờ 4s, function ActionTwoAsyn vẫn đang chạy
            Console.WriteLine("Action Two finished");
        }

        public async Task ActionThreeAsync()
        {
            Console.WriteLine("Action Three Start");
            await Task.Delay(8000);
            //Thread C chờ 8s, function ActionThreeAsync vẫn đang chạy
            Console.WriteLine("Action Three finished");
        }

        public async Task AnswerStudent1Async()
        {
            Console.WriteLine("AnswerStudent1 start");
            await Task.Delay(4000);
            Console.WriteLine("AnswerStudent1 end");
        }

        public async Task AnswerStudent2Async()
        {
            Console.WriteLine("AnswerStudent2 start");
            await Task.Delay(6000);
            Console.WriteLine("AnswerStudent2 end");
        }

        public async Task AnswerStudent3Async()
        {
            Console.WriteLine("AnswerStudent3 start");
            await Task.Delay(2000);
            Console.WriteLine("AnswerStudent3 end");
        }
        //cancelation task
        public async Task CallWhenAnyAsync()
        {
            var watch = new Stopwatch();
            watch.Start();
            var taskStudent1 = AnswerStudent1Async();
            var taskStudent2 = AnswerStudent2Async();
            var taskStudent3 = AnswerStudent3Async();
            await Task.WhenAny(taskStudent1, taskStudent2, taskStudent3);
            Console.WriteLine("Finded answer");
            watch.Stop();
            Console.WriteLine($"Execution time of Asynchronous: " +
                $"{watch.ElapsedMilliseconds} ms");
        }

        public async Task CallAsynchronousFunctionAsync()
        {
            var watch = new Stopwatch();
            watch.Start();

            var actionOne = ActionOneAsyn(); //5s
            var actionTwo = ActionTwoAsyn();//4s
            var actionThree = ActionThreeAsync();//8s

            await Task.WhenAll(actionOne, actionTwo, actionThree);
            //WhenAll giúp các task chạy đồng thời, khi mà tất cả các task chạy xong => Task.WhenAll
            //coi là chạy xong
            //Note: tasks không phụ thuộc nhau

            //---------action 2: 4s thread A
            //-----------action 1: 5s thread B
            //--------------action 3: 8s thread C


            //Shipper giao đồ nguyên liệu nấu ăn: 30 phút
            //Em nghe nhạc: 20 phút
            //Em học lập trình: 20 phút
            //Em nấu ăn sau khi có nguyên liệu

            //await Shipper giao đồ nguyên liệu nấu ăn: 5 phút
            //await Em nấu ăn sau khi có nguyên liệu => task

            //Em nghe nhạc: 20 phút
            //Em học lập trình: 20 phút

            //await Shipper giao đồ nguyên liệu nấu ăn: 5 phút
            //await Em nấu ăn sau khi có nguyên liệu => task
            //await Task.WhenAll(Em nghe nhạc: 20 phút, Em học lập trình: 20 phút)

            //Thread A await Shipper giao đồ nguyên liệu nấu ăn: 5 phút: task 1
            //Thread B await  nấu ăn sau khi có nguyên liệu: task 2
            //Task 12

            //Thread C  nghe nhạc: 20 phút //task 3
            //Thread D  học lập trình: 20 phút //task 4

            //Task 12 await task 1 await task 2
            //await Task.WhenAll(Task 12, task 3, task 4)

            Console.WriteLine("Finished");
            watch.Stop();
            Console.WriteLine($"Execution time of Asynchronous: " +
                $"{watch.ElapsedMilliseconds} ms");
        }
    }
}
