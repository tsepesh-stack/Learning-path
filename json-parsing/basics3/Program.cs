using System;
using System.Text.Json;
namespace basics3
{
    class Program
    {
        static void Main()
        {
            string json = """
{
    "name": "Саша",
    "hobbies": ["программирование", "тренировки", "путешествия"]
}
""";
            var op = new JsonSerializerOptions{PropertyNameCaseInsensitive = true};
            Person p = JsonSerializer.Deserialize<Person>(json,op);
            System.Console.WriteLine($"Хобби {p.Name}");
            foreach (var el in p.Hobbies)
            {
                System.Console.WriteLine($"--- {el}");
            }
        }
        class Person
        {
            public string Name{get;set;}
            public List<string> Hobbies{get; set;}
        }
    }
}