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
                System.Console.WriteLine("Список действий в TaskManager");
                System.Console.WriteLine("1 — добавить задачу");
                System.Console.WriteLine("2 — удалить задачу");
                System.Console.WriteLine("3 — показать все задачи (с выбором)");
                System.Console.WriteLine("4 — изменить любые параметры задачи");
                System.Console.WriteLine("5 — показать сделаные задачи");
                System.Console.WriteLine("0 — выход");
                System.Console.Write("Ввод: ");
                if (!int.TryParse(Console.ReadLine(), out n))
                {
                    Console.WriteLine("Введен не верный формат!");
                    n = -1;
                }
                switch (n)
                {
                    case 1: //на добавление 
                    System.Console.WriteLine("Вы выбрали добавить задачу (нужно будет назвать задачу добавить описание и выбрать статус, а так же ее приоритет)");
                    System.Console.Write("Введите название задачи: ");
                    Title = Console.ReadLine();
                    System.Console.Write("Введите описание задачи: ");
                    Description=Console.ReadLine();
                    newId = ts.Count == 0 ? 1 : ts.Max(x => x.Id) + 1;
                    System.Console.WriteLine("Выберите статус задачи (процесс выполнения)");
                    System.Console.WriteLine("1 — Новая задача");
                    System.Console.WriteLine("2 — Задача в процессе");
                    System.Console.Write("Ввод: ");
                    if (!int.TryParse(Console.ReadLine(), out m))
                    {
                        Console.WriteLine("Не тот формат!");
                        break;
                    }
                    System.Console.WriteLine("Выберите приоритет задачи (необходимость перед остальными задачами)");
                    System.Console.WriteLine("1 — Высокая");
                    System.Console.WriteLine("2 — Средняя");
                    System.Console.WriteLine("3 — Низкая");
                    System.Console.Write("Ввод: ");
                    if (!int.TryParse(Console.ReadLine(), out k))
                    {
                        Console.WriteLine("Не тот формат!");
                        break;
                    }
                        Status status = m == 1 ? Status.New : Status.InProgress;
                        Priority priority = k == 1 ? Priority.High : k == 2 ? Priority.Medium : Priority.Low;
                        ts.Add(new TaskItem(newId, Title, Description, status, DateTime.Now, priority));
                        File.WriteAllText("tasks.json", JsonSerializer.Serialize(ts));
                        Console.WriteLine("Задача добавлена");
                    break;
                    case 2: // Удаление задачи
                    System.Console.WriteLine("Вы выбрали удалить задачу");
                    System.Console.WriteLine("Список всех задач");
                    PrintTasks(ts);
                        System.Console.Write("Выберите задачу по айди или названию: ");
                        s=Console.ReadLine();
                        var task = FindTask(ts, s);
                        if (task == null)
                        {
                            Console.WriteLine("Не найдено!");
                            break;
                        }
                        ts.Remove(task);
                        File.WriteAllText("tasks.json", JsonSerializer.Serialize(ts));
                        Console.WriteLine("Задача удалена");
                    break;
                    case 3: // Показ всех задач с выбором
                    System.Console.WriteLine("Выберете способ вывести задачи");
                    System.Console.WriteLine("1 — Все задачи без перебоя");
                    System.Console.WriteLine("2 — По статусу");
                    System.Console.WriteLine("3 — По приоритету");
                    System.Console.Write("Ввод: ");
                        if (!int.TryParse(Console.ReadLine(), out m))
                        {
                            System.Console.WriteLine("Введен не верный формат!");
                            m=-1;
                        }
                        switch (m)
                        {
                            case 1: //Выводит задаче без фильтра
                            foreach(var el in ts)
                                {
                                    System.Console.WriteLine($"Айди: {el.Id}, Название: {el.Title}, Описание: {el.Description}, Статус: {el.Status}, Приоритет: {el.Priority}, Время создания: {el.CreatedAt}");
                                }
                            break;
                            case 2: //Выводит задачи с фильтром статус
                            var newts = ts.GroupBy(x => x.Status);
                            foreach(var group in newts)
                            {
                                Console.WriteLine($"Задачи по статусу: {group.Key}");
                                foreach(var task1 in group)
                                {
                                    Console.WriteLine($"-----{task1.Id} - {task1.Title}");
                                }
                            }
                            break;
                            case 3: //Выводит задачи с фильтром приоритет
                            var newts1 = ts.GroupBy(x => x.Priority);
                            foreach(var group in newts1)
                            {
                                Console.WriteLine($"Задачи по приоритету: {group.Key}");
                                foreach(var task1 in group)
                                {
                                    Console.WriteLine($"-----{task1.Id} - {task1.Title}");
                                }
                            }
                            break;
                        }
                    break;
                    case 4: // Меняет приоритеты

                    break;
                    case 5: 
                    System.Console.WriteLine("Список Выполненых заданий:");
                    foreach(var el in tsd)
                        {
                            System.Console.WriteLine($"Айди: {el.Id}, Название: {el.Title}, Описание: {el.Description}, Статус: {el.Status}, Приоритет: {el.Priority}, Время создания: {el.CreatedAt}");
                        }
                    break;
                }
            } while(n!=0);
            static TaskItem FindTask(List<TaskItem> ts, string s)
            {
                if (ts.Any(x => x.Title == s))
                    return ts.FirstOrDefault(x => x.Title == s);
                
                if (int.TryParse(s, out int id) && ts.Any(x => x.Id == id))
                    return ts.FirstOrDefault(x => x.Id == id); 
                return null;
            }
            static void PrintTasks(List<TaskItem> ts)
            {
                Console.WriteLine("Список задач:");
                foreach (var el in ts)
                    Console.WriteLine($"Задача {el.Id} - {el.Title}");
            } 
        }
    }
}
