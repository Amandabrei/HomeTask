namespace HomeTask.HomeTask.Api.DTOs.Requests
{
    public class TaskAssignmentUpdateRequest
    {
        public Guid TaskListId { get; set; }
        public Guid FamilyMemberId { get; set; }
        public DateTime? CompletedAt { get; set; }
    }
}
