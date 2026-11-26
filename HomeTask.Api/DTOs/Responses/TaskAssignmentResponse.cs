namespace HomeTask.HomeTask.Api.DTOs.Responses
{
    public class TaskAssignmentResponse
    {
        public Guid Id { get; set; }
        public Guid TaskListId { get; set; }
        public Guid FamilyMemberId { get; set; }
        public DateTime AssignedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
    }
}
