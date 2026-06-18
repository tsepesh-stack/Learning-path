using System;
namespace TaskManagerApi;
public class TaskService
{
    private List<TaskItem> _tasks = new List<TaskItem>
    {
        new TaskItem { Id = 1, Name = "Изучить Web API", Status = Status.unfulfilled },
        new TaskItem { Id = 2, Name = "Сделать курсовую", Status = Status.done}
    };
    public List<TaskItem> GetAll() {return _tasks;}
    public TaskItem? GetById(int id)
    {
        var task = _tasks.FirstOrDefault(t=>t.Id == id);
        return task;
    }
    public TaskItem Create(CreateTaskDto dto)
    {
        var task = new TaskItem
        {
          Id= _tasks.Max(t=>t.Id)+1,
          Name =dto.Name,  
          Status=dto.Status
        };
        _tasks.Add(task);
        return task;
    }
    public bool Update(int id, UpdateTaskDto dto)
    {
        var task = _tasks.FirstOrDefault(t=>t.Id == id);
        if (task == null)
        {
            return false;
        }
        else 
        {
            task.Name= dto.Name; 
            task.Status=dto.Status; 
            return true;
        }
    }
    public bool Delete(int id)
    {
        var task = _tasks.FirstOrDefault(t=>t.Id  == id);
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