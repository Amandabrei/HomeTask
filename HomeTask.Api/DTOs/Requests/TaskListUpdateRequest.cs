namespace HomeTask.HomeTask.Api.DTOs.Requests
{
    public class TaskListUpdateRequest
    {
        public string NameTask { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime DueDate { get; set; }
    }
}
