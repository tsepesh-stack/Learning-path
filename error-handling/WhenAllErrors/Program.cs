using System;
using System.Diagnostics;
namespace WhenAllErrors
{
    class Program
    {
        static async Task Main()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("User-Agent","MyApp");
            client.Timeout = TimeSpan.FromSeconds(10);
            Stopwatch sw = Stopwatch.StartNew();
            List<Task<string>> tasks = new List<Task<string>>();
            string[] user = {"octocat","torvalds","dhh","gaearon"};
            foreach(var el in user)
            {
                tasks.Add(FerchUserAsync(client, el));
            }
            try{
            string[] res = await Task.WhenAll(tasks);}
            catch (HttpRequestException)
            {
                System.Console.WriteLine("Задача упала");
            }
            for (int i = 0; i < tasks.Count; i++)
            {
                if (tasks[i].IsFaulted)
                    {
                        Console.WriteLine($"{user[i]}: ошибка — {tasks[i].Exception?.InnerException?.Message}");
                    }
                    else
                    {
                        Console.WriteLine($"{user[i]}: {tasks[i].Result.Length} символов");
                    }
            }
            sw.Stop();
            System.Console.WriteLine($"Время обработки запроса {sw.ElapsedMilliseconds} мс");
            System.Console.WriteLine("Программа продолжается");
        }
        static async Task<string> FerchUserAsync(HttpClient client, string username)
        {
            System.Console.WriteLine($"Задача {username} в процессе...");
            string json = await client.GetStringAsync($"https://api.github.com/users/{username}");
            System.Console.WriteLine($"Задача {username} загружена");
            return json;
        }
    }
}