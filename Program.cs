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
                System.Console.WriteLine("3 — показать все задачи (с выбором)");
                System.Console.WriteLine("4 — изменить любые параметры задачи");
                System.Console.WriteLine("5 — показать сделаные задачи");
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
                    case 1: //на добавление 1
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
                    try
                        {
                            m=int.Parse(Console.ReadLine());
                        }
                        catch (FormatException)
                        {
                            System.Console.WriteLine("Введен не тот формат!");
                            m=-1;
                        }
                    System.Console.WriteLine("Выберите приоритет задачи (необходимость перед остальными задачами)");
                    System.Console.WriteLine("1 — Высокая");
                    System.Console.WriteLine("2 — Средняя");
                    System.Console.WriteLine("3 — Низкая");
                    try
                        {
                            k=int.Parse(Console.ReadLine());
                        }
                        catch (FormatException)
                        {
                            System.Console.WriteLine("Введен не тот формат!");
                            k=-1;
                        }
                        if (m == 1)
                        {
                            switch (k)
                            {
                                case 1: // выбор приоритета(низкий) с статусом Новый
                                ts.Add(new TaskItem(newId,Title,Description, Status.New,DateTime.Now, Priority.Low));
                                string json = JsonSerializer.Serialize(ts);
                                File.WriteAllText("tasksdone.json",json);
                                break;
                                case 2: //выбор приоритета(средний) с статусом Новый
                                ts.Add(new TaskItem(newId,Title,Description, Status.New,DateTime.Now, Priority.Medium));
                                string json1 = JsonSerializer.Serialize(ts);
                                File.WriteAllText("tasksdone.json",json1);
                                break;
                                case 3: //выбор приоритета(высокий) с статусом Новый
                                ts.Add(new TaskItem(newId,Title,Description, Status.New,DateTime.Now, Priority.High));
                                string json2 = JsonSerializer.Serialize(ts);
                                File.WriteAllText("tasksdone.json",json2);
                                break;
                            }
                        } else if (m == 2)
                        {
                            switch (k)
                            {
                                case 1: // выбор приоритета(высокий) с статусом В процессе
                                ts.Add(new TaskItem(newId,Title,Description, Status.InProgress,DateTime.Now, Priority.High));
                                string json = JsonSerializer.Serialize(ts);
                                File.WriteAllText("tasksdone.json",json);
                                break;
                                case 2: // выбор приоритета(средний) с статусом В процессе
                                ts.Add(new TaskItem(newId,Title,Description, Status.InProgress,DateTime.Now, Priority.Medium));
                                string json1 = JsonSerializer.Serialize(ts);
                                File.WriteAllText("tasksdone.json",json1);
                                break;
                                case 3: // выбор приоритета(низкий) с статусом В процессе
                                ts.Add(new TaskItem(newId,Title,Description, Status.InProgress,DateTime.Now, Priority.Low));
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
                        System.Console.Write("Выберите задачу по айди или названию: ");
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
                    case 3: 
                    System.Console.WriteLine("Выберете способ вывести задачи");
                    System.Console.WriteLine("1 — Все задачи без перебоя");
                    System.Console.WriteLine("2 — По статусу");
                    System.Console.WriteLine("3 — По приоритету");
                        try
                        {
                            m=int.Parse(Console.ReadLine());
                        }
                        catch (FormatException)
                        {
                            System.Console.WriteLine("Введен не тот формат!");
                            m=-1;
                        }
                        switch (m)
                        {
                            case 1: //Выводит задаче без фильтра
                            foreach(var el in ts)
                                {
                                    System.Console.WriteLine($"Айди:{el.Id}, Название:{el.Title}, Описание:{el.Description}, Статус:{el.Status}, Приоритет:{el.Priority}, Время создания:{el.CreatedAt}");
                                }
                            break;
                            case 2: //Выводит задачи с фильтром статус
                            var newts = ts.GroupBy(x => x.Status);
                            foreach(var group in newts)
                            {
                                Console.WriteLine($"Задачи по статусу: {group.Key}");
                                foreach(var task in group)
                                {
                                    Console.WriteLine($"-----{task.Id} - {task.Title}");
                                }
                            }
                            break;
                            case 3: //Выводит задачи с фильтром приоритет
                            var newts1 = ts.GroupBy(x => x.Priority);
                            foreach(var group in newts1)
                            {
                                Console.WriteLine($"Задачи по статусу: {group.Key}");
                                foreach(var task in group)
                                {
                                    Console.WriteLine($"-----{task.Id} - {task.Title}");
                                }
                            }
                            break;
                        }
                    break;
                    case 4: 
                    System.Console.WriteLine("Изменения параметров задачи");
                    System.Console.WriteLine("1 — Отметить задачу выполненой");
                    System.Console.WriteLine("2 — Другое действие");
                    try
                        {
                            m=int.Parse(Console.ReadLine());
                        }
                        catch (FormatException)
                        {
                            System.Console.WriteLine("Введен не тот формат!");
                            m=-1;
                        }
                        switch (m)
                        {
                            case 1: //Меняет статус на выполнено и архивирует задачу
                            System.Console.WriteLine("Список задач");
                            foreach(var el in ts)
                                {
                                    System.Console.WriteLine($"Задача {el.Id} - {el.Title}");
                                }
                                System.Console.Write("Выберите задачу по айди или названию: ");
                                s=Console.ReadLine();
                        if (ts.Any(x => x.Title == s))
                        {
                            var task = ts.FirstOrDefault(x => x.Title == s);
                            if (task != null)
                            {
                                task.Status = Status.Done;
                                tsd.Add(task);              
                                ts.Remove(task);            
                                File.WriteAllText("tasks.json", JsonSerializer.Serialize(ts));
                                File.WriteAllText("tasksdone.json", JsonSerializer.Serialize(tsd));
                            }
                            Console.WriteLine("Задача выполнена");
                        }
                        else if (int.TryParse(s, out int id) && ts.Any(x => x.Id == id))
                        {
                            var task = ts.FirstOrDefault(x => x.Id == id);
                            if (task != null)
                            {
                                task.Status = Status.Done;
                                tsd.Add(task);              
                                ts.Remove(task);            
                                File.WriteAllText("tasks.json", JsonSerializer.Serialize(ts));
                                File.WriteAllText("tasksdone.json", JsonSerializer.Serialize(tsd));
                            }
                            Console.WriteLine("Задача выполнена");
                        }
                        else
                        {
                            Console.WriteLine("Не найдено!");
                        }
                            break;
                            case 2: // Дает другую возможность редактирования
                            System.Console.WriteLine("Что вы хотите изменить?");
                            System.Console.WriteLine("1 — Название");
                            System.Console.WriteLine("2 — Описание");
                            System.Console.WriteLine("3 — Статус");
                            System.Console.WriteLine("4 — Приоритет");
                            try
                                {
                                    m=int.Parse(Console.ReadLine());
                                }
                                catch (FormatException)
                                {
                                    System.Console.WriteLine("Введен не тот формат!");
                                    m=-1;
                                }
                                switch (m)
                                {
                                    case 1:  // меняет название
                                    System.Console.WriteLine("Список задач");
                                    foreach(var el in ts)
                                    {
                                    System.Console.WriteLine($"Задача {el.Id} - {el.Title}");
                                    }
                                    System.Console.Write("Выберите задачу по айди или названию: ");
                                    s=Console.ReadLine();
                                    if (ts.Any(x => x.Title == s))
                                    {
                                    var task = ts.FirstOrDefault(x => x.Title == s);
                                    if (task != null)
                                    {
                                        System.Console.Write("Введите новое название для задачи: ");
                                        task.Title = Console.ReadLine();
                                        ts.Add(task);                         
                                        File.WriteAllText("tasks.json", JsonSerializer.Serialize(ts));
                                    }
                                    Console.WriteLine("Задача переименована");
                                    }
                                    else if (int.TryParse(s, out int id) && ts.Any(x => x.Id == id))
                                    {
                                    var task = ts.FirstOrDefault(x => x.Id == id);
                                    if (task != null)
                                    {
                                        System.Console.Write("Введите новое название для задачи: ");
                                        task.Title = Console.ReadLine();
                                        ts.Add(task);                         
                                        File.WriteAllText("tasks.json", JsonSerializer.Serialize(ts));
                                    }
                                    Console.WriteLine("Задача переименована");
                                    }
                                    else
                                    {
                                    Console.WriteLine("Не найдено!");
                                    }
                                    break;
                                    case 2: // Меняет описание
                                    System.Console.WriteLine("Список задач");
                                    foreach(var el in ts)
                                    {
                                    System.Console.WriteLine($"Задача {el.Id} - {el.Title}");
                                    }
                                    System.Console.Write("Выберите задачу по айди или названию: ");
                                    s=Console.ReadLine();
                                    if (ts.Any(x => x.Title == s))
                                    {
                                    var task = ts.FirstOrDefault(x => x.Title == s);
                                    if (task != null)
                                    {
                                        System.Console.Write("Введите новое описание для задачи: ");
                                        task.Description = Console.ReadLine();                        
                                        File.WriteAllText("tasks.json", JsonSerializer.Serialize(ts));
                                    }
                                    Console.WriteLine("Задача переименована");
                                    }
                                    else if (int.TryParse(s, out int id) && ts.Any(x => x.Id == id))
                                    {
                                    var task = ts.FirstOrDefault(x => x.Id == id);
                                    if (task != null)
                                    {
                                        System.Console.Write("Введите новое название для задачи: ");
                                        task.Description = Console.ReadLine();                        
                                        File.WriteAllText("tasks.json", JsonSerializer.Serialize(ts));
                                    }
                                    Console.WriteLine("Задача переименована");
                                    }
                                    else
                                    {
                                    Console.WriteLine("Не найдено!");
                                    }
                                    break;
                                    case 3: // Меняет Статус
                                    System.Console.WriteLine("Список задач");
                                    foreach(var el in ts)
                                    {
                                    System.Console.WriteLine($"Задача {el.Id} - {el.Title}");
                                    }
                                    System.Console.Write("Выберите задачу по айди или названию: ");
                                    s=Console.ReadLine();
                                    if (ts.Any(x => x.Title == s))
                                    {
                                    var task = ts.FirstOrDefault(x => x.Title == s);
                                    if (task != null)
                                    {
                                                if (task.Status == Status.New)
                                                {
                                                    task.Status = Status.InProgress;
                                                    File.WriteAllText("tasks.json", JsonSerializer.Serialize(ts));
                                                    System.Console.WriteLine("Задача поменяла статус с New в InProgress");
                                                } else if(task.Status == Status.InProgress)
                                                {
                                                    task.Status = Status.New;
                                                    File.WriteAllText("tasks.json", JsonSerializer.Serialize(ts));
                                                    System.Console.WriteLine("Задача поменяла статус с InProgress в New");
                                                }
                                    }
                                    }
                                    else if (int.TryParse(s, out int id) && ts.Any(x => x.Id == id))
                                    {
                                    var task = ts.FirstOrDefault(x => x.Id == id);
                                    if (task != null)
                                    {
                                        if (task.Status == Status.New)
                                                {
                                                    task.Status = Status.InProgress;
                                                    File.WriteAllText("tasks.json", JsonSerializer.Serialize(ts));
                                                    System.Console.WriteLine("Задача поменяла статус с Новой(New) в Процессе(InProgress)");
                                                } else if(task.Status == Status.InProgress)
                                                {
                                                    task.Status = Status.New;
                                                    File.WriteAllText("tasks.json", JsonSerializer.Serialize(ts));
                                                    System.Console.WriteLine("Задача поменяла статус в Процессе(InProgress) на Новую(New)");
                                                }
                                    }
                                    }
                                    else
                                    {
                                    Console.WriteLine("Не найдено!");
                                    }
                                    break;
                                    case 4: // Меняет приоритеты
                                    System.Console.WriteLine("Список задач");
                                    foreach(var el in ts)
                                    {
                                    System.Console.WriteLine($"Задача {el.Id} - {el.Title}");
                                    }
                                    System.Console.Write("Выберите задачу по айди или названию: ");
                                    s=Console.ReadLine();
                                    if (ts.Any(x => x.Title == s))
                                    {
                                    var task = ts.FirstOrDefault(x => x.Title == s);
                                    if (task != null)
                                    {
                                                if (task.Priority == Priority.Low)
                                                {
                                                    System.Console.WriteLine("Выберете новый приоритет");
                                                    System.Console.WriteLine("1 — Средний");
                                                    System.Console.WriteLine("2 — Высокий");
                                                    System.Console.Write("Введите значение: ");
                                                    try{
                                                    k = int.Parse(Console.ReadLine());
                                                    }
                                                    catch (FormatException)
                                                    {
                                                        System.Console.WriteLine("Не тот формат!");
                                                        k=-1;
                                                    }
                                                    switch (k)
                                                    {
                                                        case 1: // Изменение приоритета с Низкого на Средний
                                                        task.Priority = Priority.Medium;
                                                        File.WriteAllText("tasks.json", JsonSerializer.Serialize(ts));
                                                        System.Console.WriteLine("Задача поменяла приоритет с Низкого(Low) на Средний(Medium)");
                                                        break;
                                                        case 2: // Изменение приоритета с Низкого на Высокий
                                                        task.Priority = Priority.High;
                                                        File.WriteAllText("tasks.json", JsonSerializer.Serialize(ts));
                                                        System.Console.WriteLine("Задача поменяла приоритет с Низкого(Low) на Высокий(High)");
                                                        break;
                                                    }
                                                }
                                                } else if(task.Priority == Priority.Medium)
                                                {
                                                    System.Console.WriteLine("Выберете новый приоритет");
                                                    System.Console.WriteLine("1 — Низкий");
                                                    System.Console.WriteLine("2 — Высокий");
                                                    System.Console.Write("Введите значение: ");
                                                    try{
                                                    k = int.Parse(Console.ReadLine());
                                                    }
                                                    catch (FormatException)
                                                    {
                                                        System.Console.WriteLine("Не тот формат!");
                                                        k=-1;
                                                    }
                                                    switch (k)
                                                    {
                                                        case 1: // Изменение приоритета с Среднего на Низкий
                                                        task.Priority = Priority.Low;
                                                        File.WriteAllText("tasks.json", JsonSerializer.Serialize(ts));
                                                        System.Console.WriteLine("Задача поменяла приоритет с Среднего(Medium) на Низкий(Low)");
                                                        break;
                                                        case 2: // Изменение приоритета с Среднего на Высокий
                                                        task.Priority = Priority.High;
                                                        File.WriteAllText("tasks.json", JsonSerializer.Serialize(ts));
                                                        System.Console.WriteLine("Задача поменяла приоритет с Среднего(Medium) на Высокий(High)");
                                                        break;
                                                    }
                                                } else if (task.Priority == Priority.High)
                                                {
                                                    System.Console.WriteLine("Выберете новый приоритет");
                                                    System.Console.WriteLine("1 — Низкий");
                                                    System.Console.WriteLine("2 — Средний");
                                                    System.Console.Write("Введите значение: ");
                                                    try{
                                                    k = int.Parse(Console.ReadLine());
                                                    }
                                                    catch (FormatException)
                                                    {
                                                        System.Console.WriteLine("Не тот формат!");
                                                        k=-1;
                                                    }
                                                    switch (k)
                                                    {
                                                        case 1: // Изменение приоритета с Высокого на Низкий
                                                        task.Priority = Priority.Low;
                                                        File.WriteAllText("tasks.json", JsonSerializer.Serialize(ts));
                                                        System.Console.WriteLine("Задача поменяла приоритет с Высокого(High) на Низкий(Low)");
                                                        break;
                                                        case 2: // Изменение приоритета с Высокого на Средний
                                                        task.Priority = Priority.Medium;
                                                        File.WriteAllText("tasks.json", JsonSerializer.Serialize(ts));
                                                        System.Console.WriteLine("Задача поменяла приоритет с Высокого(High) на Средний(Medium)");
                                                        break;
                                                    }
                                                }
                                    }
                                    else if (int.TryParse(s, out int id) && ts.Any(x => x.Id == id))
                                    {
                                    var task = ts.FirstOrDefault(x => x.Id == id);
                                    if (task != null)
                                    {
                                    if (task.Priority == Priority.Low)
                                    {
                                        System.Console.WriteLine("Выберете новый приоритет");
                                        System.Console.WriteLine("1 — Средний");
                                        System.Console.WriteLine("2 — Высокий");
                                        System.Console.Write("Введите значение: ");
                                        try{
                                        k = int.Parse(Console.ReadLine());
                                        }
                                        catch (FormatException)
                                        {
                                            System.Console.WriteLine("Не тот формат!");
                                            k=-1;
                                        }
                                        switch (k)
                                        {
                                            case 1: // Изменение приоритета с Низкого на Средний
                                            task.Priority = Priority.Medium;
                                            File.WriteAllText("tasks.json", JsonSerializer.Serialize(ts));
                                            System.Console.WriteLine("Задача поменяла приоритет с Низкого(Low) на Средний(Medium)");
                                            break;
                                            case 2: // Изменение приоритета с Низкого на Высокий
                                            task.Priority = Priority.High;
                                            File.WriteAllText("tasks.json", JsonSerializer.Serialize(ts));
                                            System.Console.WriteLine("Задача поменяла приоритет с Низкого(Low) на Высокий(High)");
                                            break;
                                        }
                                    }
                                    } else if(task.Priority == Priority.Medium)
                                    {
                                        System.Console.WriteLine("Выберете новый приоритет");
                                        System.Console.WriteLine("1 — Низкий");
                                        System.Console.WriteLine("2 — Высокий");
                                        System.Console.Write("Введите значение: ");
                                        try{
                                        k = int.Parse(Console.ReadLine());
                                        }
                                        catch (FormatException)
                                        {
                                            System.Console.WriteLine("Не тот формат!");
                                            k=-1;
                                        }
                                        switch (k)
                                        {
                                            case 1: // Изменение приоритета с Среднего на Низкий
                                            task.Priority = Priority.Low;
                                            File.WriteAllText("tasks.json", JsonSerializer.Serialize(ts));
                                            System.Console.WriteLine("Задача поменяла приоритет с Среднего(Medium) на Низкий(Low)");
                                            break;
                                            case 2: // Изменение приоритета с Среднего на Высокий
                                            task.Priority = Priority.High;
                                            File.WriteAllText("tasks.json", JsonSerializer.Serialize(ts));
                                            System.Console.WriteLine("Задача поменяла приоритет с Среднего(Medium) на Высокий(High)");
                                            break;
                                        }
                                    } else if (task.Priority == Priority.High)
                                    {
                                        System.Console.WriteLine("Выберете новый приоритет");
                                        System.Console.WriteLine("1 — Низкий");
                                        System.Console.WriteLine("2 — Средний");
                                        System.Console.Write("Введите значение: ");
                                        try{
                                        k = int.Parse(Console.ReadLine());
                                        }
                                        catch (FormatException)
                                        {
                                            System.Console.WriteLine("Не тот формат!");
                                            k=-1;
                                        }
                                        switch (k)
                                        {
                                            case 1: // Изменение приоритета с Высокого на Низкий
                                            task.Priority = Priority.Low;
                                            File.WriteAllText("tasks.json", JsonSerializer.Serialize(ts));
                                            System.Console.WriteLine("Задача поменяла приоритет с Высокого(High) на Низкий(Low)");
                                            break;
                                            case 2: // Изменение приоритета с Высокого на Средний
                                            task.Priority = Priority.Medium;
                                            File.WriteAllText("tasks.json", JsonSerializer.Serialize(ts));
                                            System.Console.WriteLine("Задача поменяла приоритет с Высокого(High) на Средний(Medium)");
                                            break;
                                        }
                                    }
                                    }
                                    else
                                    {
                                    Console.WriteLine("Не найдено!");
                                    }
                                    break;
                                }
                            break;
                        }
                    break;
                    case 5: 
                    System.Console.WriteLine("Список Выполненых заданий:");
                    foreach(var el in tsd)
                        {
                            System.Console.WriteLine($"Айди:{el.Id}, Название:{el.Title}, Описание:{el.Description}, Статус:{el.Status}, Приоритет:{el.Priority}, Время создания:{el.CreatedAt}");
                        }
                    break;
                }
            } while(n!=0);
        }
    }
}
