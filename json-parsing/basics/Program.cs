using System;
using System.Diagnostics;
using System.Text.Json;
namespace basics
{
    class Program
    {
        static async Task Main()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("User-Agent","MyApp");
            Stopwatch sw = Stopwatch.StartNew();
            string url = "https://api.frankfurter.app/latest?from=USD&to=TRY,CNY";
            string json = await client.GetStringAsync(url);
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            CurrencyResponse data = JsonSerializer.Deserialize<CurrencyResponse>(json, options);
            Console.WriteLine($"База: {data.Base}, дата: {data.Date}");
            foreach (var pair in data.Rates)
            {
                Console.WriteLine($"USD → {pair.Key}: {pair.Value}");
            }
            sw.Stop();
            System.Console.WriteLine($"Времени на обработку процесса {sw.ElapsedMilliseconds} мс");
        }


    }
       class CurrencyResponse
    {
        public decimal Amount { get; set; }
        public string Base { get; set; }
        public string Date { get; set; }
        public Dictionary<string, decimal> Rates { get; set; }
    }
}