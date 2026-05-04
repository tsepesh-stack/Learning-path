using System;
using System.Text.Json;
namespace TaskManager
{
    class Program
    {
        static void Main()
        {
            List<TaskItem> ts = new List<TaskItem>();
            int n;
            if (File.Exists("tasks.json"))
            {
                string json = File.ReadAllText("tasks.json");
                ts = JsonSerializer.Deserialize<List<TaskItem>>(json);
            }
            do
            {
                System.Console.WriteLine();
            } while(n!=0);
        }
    }
}
