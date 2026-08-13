using System;
using System.Text.Json.Serialization;
namespace morningdigest
{
    class Weather
    {
        public decimal Latitude{get;set;}
        public decimal Longitude{get;set;}
        public Current Current{get;set;}
    }
    class Current
    {
        public string Time { get; set; }
        [JsonPropertyName("temperature_2m")]
        public decimal Temperature2m { get; set; }
        [JsonPropertyName("weather_code")]
        public int WeatherCode { get; set; }
    }
}