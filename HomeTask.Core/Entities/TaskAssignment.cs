using System;
using System.Collections.Generic;
using System.Text;

namespace HomeTask.HomeTask.Core.Entities
{
    public class TaskAssignment
    {
        public Guid Id { get; set; }
        public Guid TaskListId {  get; set; }
        public Guid FamilyMemberId { get; set; }
        public DateTime AssignedAt { get; set; }
        public DateTime? CompletedAt { get; set; }

        public TaskAssignment()
        { }

        public TaskAssignment(Guid taskListId, Guid familyMemberId)
        {
            Id = Guid.NewGuid();
            TaskListId = taskListId;
            FamilyMemberId = familyMemberId;
            AssignedAt = DateTime.UtcNow;
            CompletedAt = null;
        }
    }
}
