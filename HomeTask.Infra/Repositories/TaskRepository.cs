using HomeTask.HomeTask.Core.Entities;
using HomeTask.HomeTask.Core.Interfaces;

namespace HomeTask.HomeTask.Infra.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly List<TaskList> _tasks = new();

        public Task<IEnumerable<TaskList>> GetAllAsync()
        {
            return Task.FromResult<IEnumerable<TaskList>>(_tasks);
        }

        public Task<TaskList?> GetByIdAsync(Guid id)
        {
            var task = _tasks.FirstOrDefault(x => x.Id == id);

            return Task.FromResult(task);
        }

        public Task AddAsync(TaskList task)
        {
            _tasks.Add(task);

            return Task.CompletedTask;
        }

        public Task UpdateAsync(TaskList task)
        {
            var existingTask = _tasks.FirstOrDefault(x => x.Id == task.Id);

            if (existingTask != null)
            {
                existingTask.NameTask = task.NameTask;
                existingTask.Description = task.Description;
                existingTask.DueDate = task.DueDate;
                existingTask.Status = task.Status;
                existingTask.IsActive = task.IsActive;
            }

            return Task.CompletedTask;
        }

        public Task DeleteAsync(Guid id)
        {
            var task = _tasks.FirstOrDefault(x => x.Id == id);

            if (task != null)
            {
                _tasks.Remove(task);
            }

            return Task.CompletedTask;
        }
    }
}
