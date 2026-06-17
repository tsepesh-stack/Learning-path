using Microsoft.AspNetCore.Mvc;
namespace TaskManagerApi.Controllers;
[ApiController]
[Route("[controller]")]
public class TasksController : ControllerBase
{
    private static List<TaskItem> _tasks = new List<TaskItem>
    {
        new TaskItem { Id = 1, Name = "Изучить Web API", Status = Status.unfulfilled },
        new TaskItem { Id = 2, Name = "Сделать курсовую", Status = Status.done }
    };
    [HttpGet]
    public ActionResult<List<TaskItem>> Get()
    {
        return Ok(_tasks);
    }
    [HttpPost]
    public ActionResult<TaskItem> Create(CreateTaskDto dto)
    {
        var task = new TaskItem
        {
          Id= _tasks.Max(t=>t.Id)+1,
          Name=dto.Name,
          Status=dto.Status  
        };
        _tasks.Add(task);
        return Created($"/tasks/{task.Id}", task);
    }
    [HttpPut ("{id}")]
    public ActionResult<TaskItem> Update(int id, UpdateTaskDto dto)
    {
        var existingTask = _tasks.FirstOrDefault(t => t.Id == id);
        if (existingTask == null)
        {
            return NotFound();
        }
        existingTask.Name = dto.Name;
        existingTask.Status = dto.Status;
        return Ok(existingTask);
    }
    [HttpDelete ("{id}")]
    public ActionResult Delete(int id)
    {
        var delTask = _tasks.FirstOrDefault(t=>t.Id==id);
        if (delTask == null)
        {
            return NoContent();
        }
        _tasks.Remove(delTask);
        return Ok(_tasks);
    }
    [HttpGet("{id}")]
public ActionResult<TaskItem> Get(int id)
{
    var task= _tasks.FirstOrDefault(t=>t.Id ==id);
    if (task == null)
        {
            return NotFound();
        }
        else
        {
            return Ok(task);
        }
}
} 