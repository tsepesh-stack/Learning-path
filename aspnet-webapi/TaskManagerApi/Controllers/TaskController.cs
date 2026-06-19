using Microsoft.AspNetCore.Mvc;
namespace TaskManagerApi.Controllers;
[ApiController]
[Route("[controller]")]
public class TasksController : ControllerBase
{
    private readonly TaskService _taskService;
    private readonly IConfiguration _config;
    public TasksController(TaskService taskService, IConfiguration config)
    {
        _taskService = taskService;
        _config = config;
    }
    [HttpGet]
    public ActionResult<List<TaskItem>> Get()
    {
        var apiName = _config["AppSettings:ApiName"];
        return Ok(_taskService.GetAll());
    }
    [HttpPost]
    public ActionResult<TaskItem> Create(CreateTaskDto dto)
    {
        var task = _taskService.Create(dto);
        return Created($"/tasks/{task.Id}", task);
    }
    [HttpPut ("{id}")]
    public ActionResult<TaskItem> Update(int id, UpdateTaskDto dto)
    {
        var task = _taskService.Update(id,dto);
        if (task == false)
        {
            return NotFound();
        } else{return Ok(_taskService.GetById(id));}
    }
    [HttpDelete ("{id}")]
    public ActionResult Delete(int id)
    {
       var task = _taskService.Delete(id);
       if (task == false)
        {
            return NotFound();
        } else
        {
            return NoContent();
        }
    }
    [HttpGet("{id}")]
    public ActionResult<TaskItem> Get(int id)
    {
        var task = _taskService.GetById(id);
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