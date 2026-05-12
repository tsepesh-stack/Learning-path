using System;
using System.Diagnostics;
namespace TrainAsync1
{
    class Program
    {
        static async Task Main()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("User-Agent","MyApp");
            Stopwatch sw = Stopwatch.StartNew();
            string[] coins = { "bitcoin", "ethereum", "solana", "cardano" }; 
                // сокращение с помощью linq  
            // List<Task<string>> tasks = coins.Select(x => FetchPriceAsync(client, x)).ToList(); 
            List<Task<string>> tasks = new List<Task<string>>();     
            foreach (var el in coins)
            {
                tasks.Add(FetchPriceAsync(client,el));   
            }
            string[] res = await Task.WhenAll(tasks);
 
 
            for (int i = 0; i < res.Length; i++)
            {
                System.Console.WriteLine($"Результат монеты {coins[i]} {res[i].Length}");
            }
            sw.Stop();
            System.Console.WriteLine($"Общее время обработки {sw.ElapsedMilliseconds} мс");
        }
       static async Task<string> FetchPriceAsync(HttpClient client, string coin)
        {
            System.Console.WriteLine($"{coin} загрузка...");
            string json = await client.GetStringAsync($"https://api.coingecko.com/api/v3/simple/price?ids={coin}&vs_currencies=usd");
            System.Console.WriteLine($"Загрузка {coin} завершена");
            return json;
        }
    }
    
}
