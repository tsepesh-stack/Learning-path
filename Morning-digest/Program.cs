using System;
using System.Data;
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
            DateTime nw = DateTime.Now;
            DayOfWeek dy = nw.DayOfWeek;
            System.Console.WriteLine($"Сегодня {GetWorkout(dy)}");
            Task<Weather> weather = GetWeatherAsync(client);
            Task<decimal> price = GetTonPriceAsync(client);
            Task<CurrencyResponse> kurs = GetCurrencyAsync(client);
            try{await Task.WhenAll(weather, price, kurs);}
            catch(HttpRequestException ex)
            {
                System.Console.WriteLine($"Сеть не отвечает: {ex.Message}");
                return;
            }
            catch (Exception ex)
            {
                System.Console.WriteLine($"Ошибка {ex.Message}");
                return;
            }
            System.Console.WriteLine($"Погода в Москве: {GetWeatherDescription(weather.Result.Current.WeatherCode)}, {weather.Result.Current.Temperature2m} C");
            System.Console.WriteLine($"Актуальный курс TON: {price.Result} usdt");
            System.Console.WriteLine($"Актуалбный курс валют (юаней, т лир):");
            System.Console.WriteLine($"-- 1 USD = {kurs.Result.Rates["CNY"]:F2} CNY");
            System.Console.WriteLine($"-- 1 USD = {kurs.Result.Rates["TRY"]:F2} TRY");
        }
        static string GetWeatherDescription (int code) => code switch
        {
                0 => "ясно",
                1 => "преимущественно ясно",
                2 => "переменная облачность",
                3 => "пасмурно",
                45 or 48 => "туман",
                51 or 53 or 55 => "морось",
                61 or 63 or 65 => "дождь",
                71 or 73 or 75 => "снег",
                80 or 81 or 82 => "ливни", 
                95 or 96 or 99 => "гроза",
                _ => ""
        };
        static string GetWorkout(DayOfWeek day)
        {
            switch (day)
            {
                case DayOfWeek.Monday:
                    return "понедельник: спина + бицепс";
                case DayOfWeek.Wednesday:
                    return "среда: грудь + трицепс";
                case DayOfWeek.Friday:
                    return "пятница: спина";
                case DayOfWeek.Sunday:
                    return "воскресенье: грудь + трицепс";
                case DayOfWeek.Tuesday: 
                    return "вторник: отдых"; 
                case DayOfWeek.Thursday: 
                    return "четверг: отдых";
                case DayOfWeek.Saturday:  
                    return "суббота: отдых";
                default: return "";
            }
        }
        static async Task<Weather> GetWeatherAsync(HttpClient client)
        {
            string json = await client.GetStringAsync("https://api.open-meteo.com/v1/forecast?latitude=55.75&longitude=37.62&current=temperature_2m,weather_code");
            var options = new JsonSerializerOptions {PropertyNameCaseInsensitive = true};
            Weather wear = JsonSerializer.Deserialize<Weather>(json, options);
            return wear;
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