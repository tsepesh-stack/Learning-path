using System;
namespace TaskManager
{
    enum Status{New, InProgress, Done}
    enum Priority{Low, Medium, High}
    class Task
    {
        public int Id {get;set;}
        public string Title{get;set;}
        public string Description{get;set;}
        public Status Status{get;set;}
        public DateTime CreatedAt{get; set;}
        public Priority Priority{get; set;}
        public Task(int Id, string Title,string Description,Status Status,DateTime CreatedAt,Priority Priority)
        {
            this.Id=Id;
            this.Title=Title;
            this.Description=Description;
            this.Status=Status;
            this.CreatedAt=CreatedAt;
            this.Priority=Priority;
        }
    }
}