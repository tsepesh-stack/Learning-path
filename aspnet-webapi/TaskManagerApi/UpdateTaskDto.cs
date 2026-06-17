using System;
using System.ComponentModel.DataAnnotations;
namespace TaskManagerApi;
public class UpdateTaskDto
{
    [Required]
    public string Name{get; set;}
    public Status Status{get; set;}
}