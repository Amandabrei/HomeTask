using HomeTask.HomeTask.Core.Entities;
using HomeTask.HomeTask.Core.Interfaces;

namespace HomeTask.HomeTask.Infra.Repositories
{
    public class TaskAssignmentRepository : ITaskAssignmentRepository
    {
        private readonly List<TaskAssignment> _assignments = new();

        public Task<IEnumerable<TaskAssignment>> GetAllAsync()
        {
            return Task.FromResult<IEnumerable<TaskAssignment>>(_assignments);
        }

        public Task<TaskAssignment?> GetByIdAsync(Guid id)
        {
            var assignment = _assignments.FirstOrDefault(x => x.Id == id);

            return Task.FromResult(assignment);
        }

        public Task<IEnumerable<TaskAssignment>> GetByTaskIdAsync(Guid taskId)
        {
            var assignments = _assignments
                .Where(x => x.TaskListId == taskId);

            return Task.FromResult<IEnumerable<TaskAssignment>>(assignments);
        }

        public Task<IEnumerable<TaskAssignment>> GetByFamilyMemberIdAsync(Guid familyMemberId)
        {
            var assignments = _assignments
                .Where(x => x.FamilyMemberId == familyMemberId);

            return Task.FromResult<IEnumerable<TaskAssignment>>(assignments);
        }

        public Task AddAsync(TaskAssignment assignment)
        {
            _assignments.Add(assignment);

            return Task.CompletedTask;
        }

        public Task UpdateAsync(TaskAssignment assignment)
        {
            var existingAssignment = _assignments
                .FirstOrDefault(x => x.Id == assignment.Id);

            if (existingAssignment != null)
            {
                existingAssignment.TaskListId = assignment.TaskListId;
                existingAssignment.FamilyMemberId = assignment.FamilyMemberId;
                existingAssignment.CompletedAt = assignment.CompletedAt;
            }

            return Task.CompletedTask;
        }

        public Task DeleteAsync(Guid id)
        {
            var assignment = _assignments.FirstOrDefault(x => x.Id == id);

            if (assignment != null)
            {
                _assignments.Remove(assignment);
            }

            return Task.CompletedTask;
        }
    }
}
