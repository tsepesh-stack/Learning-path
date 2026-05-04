using System;
using System.Text.Json;
namespace TaskManager
{
    class Program
    {
        static void Main()
        {
            List<TaskItem> ts = new List<TaskItem>();
            List<TaskItem> tsd = new List<TaskItem>();
            int n;
            int m;
            int k;
            int newId;
            string Title;
            string Description;
            DateTime dateTime;
            string s;
            if (File.Exists("tasks.json"))
            {
                string json = File.ReadAllText("tasks.json");
                ts = JsonSerializer.Deserialize<List<TaskItem>>(json);
            }
            if (File.Exists("tasksdone.json"))
            {
                string json = File.ReadAllText("tasksdone.json");
                tsd = JsonSerializer.Deserialize<List<TaskItem>>(json);
            }
            do
            {
                System.Console.WriteLine("1 — добавить задачу");
                System.Console.WriteLine("2 — удалить задачу");
                System.Console.WriteLine("3 — показать все задачи");
                System.Console.WriteLine("4 — показать задачи по статусу");
                System.Console.WriteLine("5 — изменить статус задачи");
                System.Console.WriteLine("6 — показать задачи по приоритету");
                System.Console.WriteLine("7 — показать сделаные задачи");
                System.Console.WriteLine("0 — выход");
                try
                {
                    n=int.Parse(Console.ReadLine());
                }
                catch (FormatException)
                {
                    System.Console.WriteLine("Введен не верный формат!");
                    n=-1;
                }
                switch (n)
                {
                    case 1: 
                    System.Console.WriteLine("Вы выбрали добавить задачу (нужно будет назвать задачу добавить описание и выбрать статус, а так же ее приоритет)");
                    System.Console.Write("Введите название задачи: ");
                    Title = Console.ReadLine();
                    System.Console.Write("Введите описание задачи: ");
                    Description=Console.ReadLine();
                    if (ts.Count == 0)
                    {
                        newId = 1;
                    }
                    else
                    {
                        newId = ts.Max(x => x.Id) + 1;
                    }
                    System.Console.WriteLine("Выберите статус задачи (процесс выполнения)");
                    System.Console.WriteLine("1 — Новая задача");
                    System.Console.WriteLine("2 — Задача в процессе");
                    m=int.Parse(Console.ReadLine());
                    System.Console.WriteLine("Выберите приоритет задачи (необходимость перед остальными задачами)");
                    System.Console.WriteLine("1 — Низкая");
                    System.Console.WriteLine("2 — Средняя");
                    System.Console.WriteLine("3 — Высокая");
                    k=int.Parse(Console.ReadLine());
                        if (m == 1)
                        {
                            switch (k)
                            {
                                case 1:
                                ts.Add(new TaskItem(newId,Title,Description, Status.New,DateTime.Now, Priority.Low));
                                string json = JsonSerializer.Serialize(ts);
                                File.WriteAllText("tasksdone.json",json);
                                break;
                                case 2: 
                                ts.Add(new TaskItem(newId,Title,Description, Status.New,DateTime.Now, Priority.Medium));
                                string json1 = JsonSerializer.Serialize(ts);
                                File.WriteAllText("tasksdone.json",json1);
                                break;
                                case 3: 
                                ts.Add(new TaskItem(newId,Title,Description, Status.New,DateTime.Now, Priority.High));
                                string json2 = JsonSerializer.Serialize(ts);
                                File.WriteAllText("tasksdone.json",json2);
                                break;
                            }
                        } else if (m == 2)
                        {
                            switch (k)
                            {
                                case 1:
                                ts.Add(new TaskItem(newId,Title,Description, Status.InProgress,DateTime.Now, Priority.Low));
                                string json = JsonSerializer.Serialize(ts);
                                File.WriteAllText("tasksdone.json",json);
                                break;
                                case 2: 
                                ts.Add(new TaskItem(newId,Title,Description, Status.InProgress,DateTime.Now, Priority.Medium));
                                string json1 = JsonSerializer.Serialize(ts);
                                File.WriteAllText("tasksdone.json",json1);
                                break;
                                case 3: 
                                ts.Add(new TaskItem(newId,Title,Description, Status.InProgress,DateTime.Now, Priority.High));
                                string json2 = JsonSerializer.Serialize(ts);
                                File.WriteAllText("tasksdone.json",json2);
                                break;
                            }
                        }
                    break;
                    case 2: 
                    System.Console.WriteLine("Вы выбрали удалить задачу");
                    System.Console.WriteLine("Список всех задач");
                    foreach(var el in ts)
                        {
                            System.Console.WriteLine($"Задача {el.Id} - {el.Title}");
                        }
                        System.Console.WriteLine("Выберите задачу по ее айди или названию");
                        System.Console.WriteLine("Введи айди или название");
                        s=Console.ReadLine();
                        if (ts.Any(x => x.Title == s))
                        {
                            ts.RemoveAll(x => x.Title == s);
                            Console.WriteLine("Задача удалена");
                        }
                        else if (int.TryParse(s, out int id) && ts.Any(x => x.Id == id))
                        {
                            ts.RemoveAll(x => x.Id == id);
                            Console.WriteLine("Задача удалена");
                        }
                        else
                        {
                            Console.WriteLine("Не найдено!");
                        }
                    break;
                    case 3: break;
                    case 4: break;
                    case 5: break;
                    case 6: break;
                }
            } while(n!=0);
        }
    }
}
