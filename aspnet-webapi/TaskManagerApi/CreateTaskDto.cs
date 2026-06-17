using System;
using System.ComponentModel.DataAnnotations;
namespace TaskManagerApi;
public class CreateTaskDto
{
    [Required]
    [MaxLength(100)]
    public string Name{get;set;}
    public Status Status{get;set;}
}