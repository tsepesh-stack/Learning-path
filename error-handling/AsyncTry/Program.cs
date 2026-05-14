using System;
using System.Diagnostics;
namespace AsyncTry
{
    class Program
    {
        static async Task Main()
        {
            HttpClient client=new HttpClient();
            client.DefaultRequestHeaders.Add("User-Agent","MyApp");
            client.Timeout = TimeSpan.FromSeconds(5);
            Stopwatch sw = Stopwatch.StartNew();
            try{
                string nb = await client.GetStringAsync("https://httpbin.org/delay/10");
                Console.WriteLine($"{nb.Length}");}
            catch (TaskCanceledException)
            {
                System.Console.WriteLine("Сервер не ответил во время");
            }
            catch (HttpRequestException ex)
            {
                System.Console.WriteLine($"Ошибка url: {ex.Message}");
                Console.WriteLine($"Тип: {ex.GetType().Name}");
            } System.Console.WriteLine("программа продолжает работать");
            sw.Stop();
            System.Console.WriteLine($"Время обработки {sw.ElapsedMilliseconds} мс");
        }
    }
}
