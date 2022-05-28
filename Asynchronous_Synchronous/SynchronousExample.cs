using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Asynchronous_Synchronous
{
    public class SynchronousExample
    {
        public string DownloadFileSynchronous(string path)
        {
            WebClient webClient = new WebClient();
            var file = webClient.DownloadString(path);
            Thread.Sleep(9000); //Giả sử việc download tốn 9s

            Console.WriteLine("Đã hoàn thành việc tải file");
            return file;
        }

        public void ActionOne()
        {

            //Download file 
            Thread.Sleep(5000);
            Console.WriteLine("Action one");
        }

        public void ActionTwo()
        {
            Thread.Sleep(4000);
            Console.WriteLine("Action two");
        }

        public void ActionThree()
        {
            Thread.Sleep(8000);
            Console.WriteLine("Action three");
        }

        public void CallSynchronousFunction()
        {

            var watch = new Stopwatch();
            watch.Start();

            ActionOne(); //5s
            ActionTwo(); //4s
            ActionThree(); //8s

            watch.Stop();
            Console.WriteLine($"Execution time of Synchronous: " +
                $"{watch.ElapsedMilliseconds} ms");
        }
    }
}
