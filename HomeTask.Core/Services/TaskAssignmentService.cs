using HomeTask.HomeTask.Core.Entities;
using HomeTask.HomeTask.Core.Interfaces;

namespace HomeTask.HomeTask.Core.Services
{
    public class TaskAssignmentService
    {
        private readonly ITaskAssignmentRepository _assignmentRepository;

        public TaskAssignmentService(
            ITaskAssignmentRepository assignmentRepository)
        {
            _assignmentRepository = assignmentRepository;
        }

        public async Task<IEnumerable<TaskAssignment>> GetAllAsync()
        {
            return await _assignmentRepository.GetAllAsync();
        }

        public async Task<TaskAssignment?> GetByIdAsync(Guid id)
        {
            return await _assignmentRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<TaskAssignment>> GetByTaskIdAsync(Guid taskListId)
        {
            return await _assignmentRepository.GetByTaskIdAsync(taskListId);
        }

        public async Task<IEnumerable<TaskAssignment>> GetByFamilyMemberIdAsync(Guid familyMemberId)
        {
            return await _assignmentRepository.GetByFamilyMemberIdAsync(familyMemberId);
        }

        public async Task AddAsync(TaskAssignment assignment)
        {
            if (assignment.TaskListId == Guid.Empty)
                throw new ArgumentException("A tarefa é obrigatória.");

            if (assignment.FamilyMemberId == Guid.Empty)
                throw new ArgumentException("O integrante da família é obrigatório.");

            await _assignmentRepository.AddAsync(assignment);
        }

        public async Task UpdateAsync(TaskAssignment assignment)
        {
            await _assignmentRepository.UpdateAsync(assignment);
        }

        public async Task DeleteAsync(Guid id)
        {
            var assignment = await _assignmentRepository.GetByIdAsync(id);

            if (assignment == null)
                throw new KeyNotFoundException("Atribuição não encontrada.");

            await _assignmentRepository.DeleteAsync(id);
        }
    }
}
