using System;
namespace TaskManagerApi;
public enum Status {done, unfulfilled}
public class TaskItem
{
    public int Id {get; set;}
    public string Name {get;set;}
    public Status Status{get; set;}
}