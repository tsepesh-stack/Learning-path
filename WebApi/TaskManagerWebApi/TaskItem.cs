using System;
namespace TaskManagerWebApi
{
    public enum Status{New, InProgress, Done}
    public enum Priority{Low, Medium, High}
    public class TaskItem
    {
        public int Id {get;set;}
        public string Title{get;set;}
        public string Description{get;set;}
        public Status Status{get;set;}
        public DateTime CreatedAt{get; set;}
        public Priority Priority{get; set;}
    }
}