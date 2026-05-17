using System;
using System.Diagnostics;
using System.Text.Json;
namespace morningdigest
{
    class Program
    {
        static async Task Main()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("User-Agent","MyApp");
            Task<string> we= GetWeatherAsync(client);
            string weather = await we;
            System.Console.WriteLine(weather);
        }
        static async Task<string> GetWeatherAsync(HttpClient client)
        {
            string json = await client.GetStringAsync("https://wttr.in/Zagreb?format=3");
            return json;
        }
    }
}