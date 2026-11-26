using HomeTask.HomeTask.Core.Enums;

namespace HomeTask.HomeTask.Api.DTOs.Responses
{
    public class TaskListResponse
    {
        public Guid Id { get; set; }
        public string NameTask { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime DueDate { get; set; }
        public TaskState Status { get; set; }
        public bool IsActive { get; set; }
    }
}
