namespace HomeTask.HomeTask.Api.DTOs.Responses
{
    public class FamilyMemberResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Email { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; }
    }
}
