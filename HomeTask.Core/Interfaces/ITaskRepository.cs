using HomeTask.HomeTask.Core.Entities;

namespace HomeTask.HomeTask.Core.Interfaces
{
    public interface ITaskRepository
    {
        Task<IEnumerable<TaskList>> GetAllAsync();

        Task<TaskList?> GetByIdAsync(Guid id);

        Task AddAsync(TaskList task);

        Task UpdateAsync(TaskList task);

        Task DeleteAsync(Guid id);
    }
}
