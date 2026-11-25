using HomeTask.HomeTask.Core.Entities;

namespace HomeTask.HomeTask.Core.Interfaces
{
    public interface ITaskAssignmentRepository
    {
        Task<IEnumerable<TaskAssignment>> GetAllAsync();

        Task<TaskAssignment?> GetByIdAsync(Guid id);

        Task<IEnumerable<TaskAssignment>> GetByTaskIdAsync(Guid taskId);

        Task<IEnumerable<TaskAssignment>> GetByFamilyMemberIdAsync(Guid familyMemberId);

        Task AddAsync(TaskAssignment assignment);

        Task UpdateAsync(TaskAssignment assignment);

        Task DeleteAsync(Guid id);
    }
}
