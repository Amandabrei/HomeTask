using HomeTask.HomeTask.Core.Entities;
using HomeTask.HomeTask.Core.Enums;
using HomeTask.HomeTask.Core.Interfaces;

namespace HomeTask.HomeTask.Core.Services
{
    public class TaskService
    {
        private readonly ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<IEnumerable<TaskList>> GetAllAsync()
        {
            return await _taskRepository.GetAllAsync();
        }

        public async Task<TaskList?> GetByIdAsync(Guid id)
        {
            return await _taskRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(TaskList task)
        {
            if (string.IsNullOrWhiteSpace(task.NameTask))
                throw new ArgumentException("O nome da tarefa é obrigatório.");

            if (task.DueDate < DateTime.UtcNow)
                throw new ArgumentException("A data de vencimento não pode estar no passado.");

            await _taskRepository.AddAsync(task);
        }

        public async Task UpdateAsync(TaskList task)
        {
            if (string.IsNullOrWhiteSpace(task.NameTask))
                throw new ArgumentException("O nome da tarefa é obrigatório.");

            await _taskRepository.UpdateAsync(task);
        }


        public async Task<bool> UpdateStatusAsync(Guid id, TaskState status)
        {
            if (!Enum.IsDefined(status))
                throw new ArgumentException("Status inválido.");

            var existingTask = await _taskRepository.GetByIdAsync(id);

            if (existingTask == null)
                return false;

            existingTask.Status = status;

            await _taskRepository.UpdateAsync(existingTask);

            return true;
        }

        public async Task DeleteAsync(Guid id)
        {
            var task = await _taskRepository.GetByIdAsync(id);

            if (task == null)
                throw new KeyNotFoundException("Tarefa não encontrada.");

            await _taskRepository.DeleteAsync(id);
        }
    }
}
