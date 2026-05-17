using System;
using System.Text.Json;
namespace basics1
{
    class Program
    {
        static void Main()
        {
            string json = """
            {
                "name": "Саша",
                "age": 23,
                "isStudent": true
            }
            """;
            var pipl = new JsonSerializerOptions{PropertyNameCaseInsensitive = true};
            Person p = JsonSerializer.Deserialize<Person>(json,pipl);
            string st = p.IsStudent ? "студент" : "не студент";
            Console.WriteLine($"{p.Name}: {p.Age} лет, {st}");
        }
    }
    class Person
    {
        public string Name{get; set;}
        public int Age{get; set;}
        public bool IsStudent{get; set;}
    }
    
}
