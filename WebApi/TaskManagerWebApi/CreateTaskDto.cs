using System;
using System.ComponentModel.DataAnnotations;
namespace TaskManagerWebApi
{
    public class CreateTaskDto
    {
        [Required]
        [MaxLength(100)]
        public string Title{get;set;}
        [MaxLength(500)]
        public string Description{get;set;}
        public Status Status{get;set;}
        public Priority Priority{get; set;}
    }
}