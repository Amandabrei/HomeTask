using HomeTask.HomeTask.Core.Enums;

namespace HomeTask.HomeTask.Core.Dtos
{
    public class UpdateTaskStatusRequest
    {
        public TaskState Status { get; set; }
    }
}
