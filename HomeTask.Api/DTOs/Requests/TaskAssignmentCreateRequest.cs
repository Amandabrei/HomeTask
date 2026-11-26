namespace HomeTask.HomeTask.Api.DTOs.Requests
{
    public class TaskAssignmentCreateRequest
    {
        public Guid TaskListId { get; set; }
        public Guid FamilyMemberId { get; set; }

    }
}
