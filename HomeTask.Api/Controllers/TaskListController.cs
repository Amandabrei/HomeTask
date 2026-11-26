using HomeTask.HomeTask.Api.DTOs.Requests;
using HomeTask.HomeTask.Api.DTOs.Responses;
using HomeTask.HomeTask.Core.Entities;
using HomeTask.HomeTask.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace HomeTask.HomeTask.Api.Controllers
{
    /// <summary>
    /// Gerencia o cadastro, consulta, atualização e exclusão de tarefas.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class TaskListController : ControllerBase
    {
        private readonly TaskService _taskService;

        /// <summary>
        /// Inicializa o controller de tarefas.
        /// </summary>
        /// <param name="taskService">Serviço responsável pelas operações de tarefas.</param>
        public TaskListController(TaskService taskService)
        {
            _taskService = taskService;
        }

        /// <summary>
        /// Retorna todas as tarefas cadastradas.
        /// </summary>
        /// <returns>Lista de tarefas.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<TaskListResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var tasks = await _taskService.GetAllAsync();

            var response = tasks.Select(MapToResponse);

            return Ok(response);
        }

        /// <summary>
        /// Busca uma tarefa pelo identificador.
        /// </summary>
        /// <param name="id">Identificador único da tarefa.</param>
        /// <returns>Dados da tarefa encontrada.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(TaskListResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var task = await _taskService.GetByIdAsync(id);

            if (task == null)
                return NotFound();

            return Ok(MapToResponse(task));
        }

        /// <summary>
        /// Cadastra uma nova tarefa.
        /// </summary>
        /// <param name="request">Dados da tarefa que será cadastrada.</param>
        /// <returns>Dados da tarefa criada.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(TaskListResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(TaskListCreateRequest request)
        {
            var newTask = new TaskList(
                request.NameTask,
                request.Description,
                request.DueDate
            );

            await _taskService.AddAsync(newTask);

            var response = MapToResponse(newTask);

            return CreatedAtAction(
                nameof(GetById),
                new { id = newTask.Id },
                response
            );
        }

        /// <summary>
        /// Atualiza os dados de uma tarefa existente.
        /// </summary>
        /// <param name="id">Identificador da tarefa que será atualizada.</param>
        /// <param name="request">Novos dados da tarefa.</param>
        /// <returns>Resultado da atualização.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(
            Guid id,
            TaskListUpdateRequest request)
        {
            var existingTask = await _taskService.GetByIdAsync(id);

            if (existingTask == null)
                return NotFound();

            existingTask.NameTask = request.NameTask;
            existingTask.Description = request.Description;
            existingTask.DueDate = request.DueDate;

            await _taskService.UpdateAsync(existingTask);

            return NoContent();
        }

        /// <summary>
        /// Atualiza somente o status de uma tarefa.
        /// </summary>
        /// <param name="id">Identificador da tarefa.</param>
        /// <param name="request">Novo status da tarefa.</param>
        /// <returns>Resultado da atualização.</returns>
        [HttpPatch("{id}/status")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateStatus(
            Guid id,
            UpdateTaskStatusRequest request)
        {
            var updated = await _taskService.UpdateStatusAsync(
                id,
                request.Status
            );

            if (!updated)
                return NotFound();

            return NoContent();
        }

        /// <summary>
        /// Exclui uma tarefa pelo identificador.
        /// </summary>
        /// <param name="id">Identificador da tarefa que será excluída.</param>
        /// <returns>Resultado da exclusão.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var existingTask = await _taskService.GetByIdAsync(id);

            if (existingTask == null)
                return NotFound();

            await _taskService.DeleteAsync(id);

            return NoContent();
        }

        private static TaskListResponse MapToResponse(TaskList task)
        {
            return new TaskListResponse
            {
                Id = task.Id,
                NameTask = task.NameTask,
                Description = task.Description,
                CreatedAt = task.CreatedAt,
                DueDate = task.DueDate,
                Status = task.Status,
                IsActive = task.IsActive
            };
        }
    }
}
