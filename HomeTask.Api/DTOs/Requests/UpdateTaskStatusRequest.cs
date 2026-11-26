using HomeTask.HomeTask.Core.Enums;

namespace HomeTask.HomeTask.Api.DTOs.Requests
{
    public class UpdateTaskStatusRequest
    {
        public TaskState Status { get; set; }
    }
}
