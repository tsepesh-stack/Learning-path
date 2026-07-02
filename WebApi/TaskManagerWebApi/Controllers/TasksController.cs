using System;
using Microsoft.AspNetCore.Mvc;
namespace TaskManagerWebApi;
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
        Console.WriteLine($"API name: {apiName}");
        return Ok(_taskService.GetAll());
    }
    [HttpGet("{id}")]
    public ActionResult<TaskItem> Get(int id)
    {
        var task = _taskService.GetById(id);
        if(task==null) return NotFound();
        return Ok(task);
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
        var success = _taskService.Update(id,dto);
        if(!success) return NotFound();
        return Ok(_taskService.GetById(id));
    }
    [HttpDelete ("{id}")]
    public ActionResult<TaskItem> Delete(int id)
    {
        var succes = _taskService.Delete(id);
        if(!succes) return NotFound();
        return NoContent();
    }


}
