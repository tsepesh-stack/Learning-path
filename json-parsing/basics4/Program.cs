using System;
using System.Text.Json;
namespace basics4
{
    class Program
    {
        static void Main()
        {
                string json = """
                {
                    "city": "Загреб",
                    "users": [
                        { "name": "Саша", "age": 23 },
                        { "name": "Иван", "age": 30 },
                        { "name": "Мария", "age": 27 }
                    ]
                }
                """;
            var option= new JsonSerializerOptions{PropertyNameCaseInsensitive = true};
            Person p = JsonSerializer.Deserialize<Person>(json,option);
            System.Console.WriteLine($"Город: {p.City}");
            foreach(var el in p.Users)
            {
                System.Console.WriteLine($"-- {el.Name} возраст: {el.Age}");
            }
            
            
            
        }
        class Person
        {
            public string City{get; set;}
            public List<Users> Users { get; set; }
        }
        class Users
        {
            public string Name{get; set;}
            public int Age{get; set;}
        }

    }
}