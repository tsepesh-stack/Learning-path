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
    public ActionResult<TaskItem> Create(TaskItem task)
    {
        task.Id = _tasks.Max(t => t.Id) + 1;
        _tasks.Add(task);
        return Created($"/tasks/{task.Id}", task);
    }
    [HttpPut ("{id}")]
    public ActionResult<TaskItem> Update(int id, TaskItem task)
    {
        var existingTask = _tasks.FirstOrDefault(t => t.Id == id);
        if (existingTask == null)
        {
            return NotFound();
        }
        existingTask.Name = task.Name;
        existingTask.Status = task.Status;
        return Ok(existingTask);
    }
    [HttpDelete ("{id}")]
    public IActionResult Delete(int id)
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