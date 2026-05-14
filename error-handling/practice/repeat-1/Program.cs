using System;
using System.Diagnostics;
namespace Repeat1
{
    class Program
    {
        static async Task Main()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("User-Agent","MyApp");
            client.Timeout= TimeSpan.FromSeconds(5);
            Stopwatch sw = Stopwatch.StartNew();
            int[] time = {3,5,10,4};
            List<Task<string>> tasks = new List<Task<string>>();
            foreach (var el in time)
            {
                tasks.Add(FerchUserAsync(client,el));
            }
            try{ 
            await Task.WhenAll(tasks);
            }
            catch 
            {
                // Ловит все ошибки
            }
            for (int i = 0; i < tasks.Count; i++)
            {
               if (tasks[i].IsCompletedSuccessfully)
                {
                    Console.WriteLine($"{time[i]}: {tasks[i].Result.Length} символов");
                }
                else if (tasks[i].IsFaulted)
                {
                    Console.WriteLine($"{time[i]}: ошибка - {tasks[i].Exception?.InnerException?.Message}");
                }
                else if (tasks[i].IsCanceled)
                {
                    Console.WriteLine($"{time[i]}: отменено (таймаут)");
                }
            }
            
            sw.Stop();
            System.Console.WriteLine($"Время на обработку запроса: {sw.ElapsedMilliseconds} мс");
            System.Console.WriteLine("Программа продалжает работать");
        }
        static async Task<string> FerchUserAsync(HttpClient client, int time)
        {
            System.Console.WriteLine($"Обработка запроса для теста таймаута с числом {time} при ограничении 5");
            string json = await client.GetStringAsync($"https://httpbin.org/delay/{time}");
            System.Console.WriteLine($"ОБработка запроса число {time} завершена");
            return json;
        }
    }
}