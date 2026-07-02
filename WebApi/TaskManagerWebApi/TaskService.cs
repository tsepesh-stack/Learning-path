using System;
namespace TaskManagerWebApi;
public class TaskService
{
    private List<TaskItem> _tasks = new List<TaskItem>
    {
        new TaskItem {
            Id = 1, 
            Title = "Стакан Воды", 
            Description="нужно выпить стакан воды от отеков", 
            Status=Status.New, Priority=Priority.Medium, CreatedAt= DateTime.Now}
    };
    public List<TaskItem> GetAll(){return _tasks;}
    public TaskItem? GetById(int Id)
    {
        var tasks = _tasks.FirstOrDefault(t=>t.Id== Id);
        return tasks;
    }
    public TaskItem Create(CreateTaskDto dto)
    {
        var task = new TaskItem
        {
          Id = _tasks.Any() ? _tasks.Max(t => t.Id) + 1 : 1,
          Title=dto.Title,
          Description=dto.Description,
          Status=dto.Status,
          CreatedAt = DateTime.Now,
          Priority=dto.Priority  
        }; _tasks.Add(task);
        return task;
    }
    public bool Update(int id, UpdateTaskDto dto)
    {
        var task= _tasks.FirstOrDefault(t=>t.Id==id);
        if (task == null)
        {
            return false;
        }
        else
        {
            task.Title=dto.Title;
            task.Description=dto.Description;
            task.Status=dto.Status;
            task.Priority=dto.Priority;
            return true;
        }
    }
    public bool Delete(int id)
    {
        var task = _tasks.FirstOrDefault(t=>t.Id==id);
        if (task == null)
        {
            return false;
        }
        else
        {
            _tasks.Remove(task);
            return true;
        }
    }

}