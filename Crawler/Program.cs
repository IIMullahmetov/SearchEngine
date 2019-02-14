using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Crawler
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var queue = new Queue<string>();
            var url = "https://ru.wikipedia.org/";
            queue.Enqueue(url + "/wiki/%D0%92%D0%B8%D0%BA%D0%B8");
            var sendedLinks = new List<string>();

            var counter = 0;
            var path = @"C:\Users\Ilyas\source\repos\SearchEngine\Pages\";

            while (counter < 100)
            {
                try
                {
                    counter++;
                    var requestSender = new RequestSender();
                    var li = queue.Dequeue();
                    var document = requestSender.Send(li);
                    sendedLinks.Add(li);

                    var observer = new StringObserver();
                    var links = observer.GetLinks(document);
                    foreach (var link in links)
                    {
                        queue.Enqueue(url + link);
                    }

                    var text = observer.GetOnlyText(document);
                    await WriteAsync(path + $"wiki_{counter}.txt", text);
                }
                catch { }
            }
            File.AppendAllLines(path + "urls.txt", sendedLinks);
        }

        private static async Task WriteAsync(string path, string content)
        {
            await File.WriteAllTextAsync(path, content);
        }
    }
}
