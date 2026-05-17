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
            Task<string> weather = GetWeatherAsync(client);
            Task<decimal> price = GetTonPriceAsync(client);
            Task<CurrencyResponse> kurs = GetCurrencyAsync(client);
            try{await Task.WhenAll(weather, price, kurs);}
            catch {} // тут принимает все возможные ошибки
            System.Console.WriteLine($"Погода на сегодняшний день {weather.Result}");
            System.Console.WriteLine($"Актуальный курс TON: {price.Result} usdt");
            System.Console.WriteLine($"Актуалбный курс валют (юаней, т лир):");
            System.Console.WriteLine($"-- 1 USD = {kurs.Result.Rates["CNY"]:F2} CNY");
            System.Console.WriteLine($"-- 1 USD = {kurs.Result.Rates["TRY"]:F2} TRY");
        }
        static async Task<string> GetWeatherAsync(HttpClient client)
        {
            string json = await client.GetStringAsync("https://wttr.in/Zagreb?format=3");
            return json;
        }
        static async Task<CurrencyResponse> GetCurrencyAsync(HttpClient client)
        {
            string json = await client.GetStringAsync("https://api.frankfurter.app/latest?from=USD&to=TRY,CNY");
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            CurrencyResponse kurs = JsonSerializer.Deserialize<CurrencyResponse>(json, options);
            return kurs;
        }
        static async Task<decimal> GetTonPriceAsync(HttpClient client)
        {
            string json = await client.GetStringAsync("https://api.coingecko.com/api/v3/simple/price?ids=the-open-network&vs_currencies=usd");
            var data = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, decimal>>>(json);
            decimal price = data["the-open-network"]["usd"];
            return price;
        }
    }
}