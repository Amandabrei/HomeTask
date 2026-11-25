using System.Collections.Generic;
using System.Text;
using HomeTask.HomeTask.Core.Enums;

namespace HomeTask.HomeTask.Core.Entities
{
    public class TaskList
    {
        public Guid Id { get; set; }
        public string NameTask{ get; set; } = string.Empty;
        public string? Description {  get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime DueDate { get; set; }
        public TaskState Status { get; set; }
        public bool IsActive { get; set; }

        public TaskList()
        { }

        public TaskList(string nameTask, string? description, DateTime duoDate)
        {
            Id = Guid.NewGuid();
            NameTask = nameTask;
            Description = description;
            CreatedAt = DateTime.UtcNow;
            DueDate = duoDate;
            Status = TaskState.Pending;
            IsActive = true;
        }
    }
}
