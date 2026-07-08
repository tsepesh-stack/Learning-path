using System;
using System.Text.Json;
namespace basics2
{
    class Program
    {
        static void Main()
        {
            string json = """
            {
                "name": "Саша",
                "age": 23,
                "address": {
                    "country": "Хорватия",
                    "city": "Загреб"
                }
            }
            """;
            var pipl = new JsonSerializerOptions{PropertyNameCaseInsensitive = true};
            Person p = JsonSerializer.Deserialize<Person>(json,pipl);
            Console.WriteLine($"{p.Name}, {p.Age} года, живёт в городе {p.Address.City} ({p.Address.Country})");
        }
    }
    class Person
    {
        public string Name{get; set;}
        public int Age{get; set;}
        public Address Address{get; set;}
    }
    class Address
    {
        public string Country{get; set;}
        public string City{get; set;}
    }
}
