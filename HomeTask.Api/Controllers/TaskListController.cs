using HomeTask.HomeTask.Core.Dtos;
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var tasks = await _taskService.GetAllAsync();
            return Ok(tasks);
        }

        /// <summary>
        /// Busca uma tarefa pelo identificador.
        /// </summary>
        /// <param name="id">Identificador único da tarefa.</param>
        /// <returns>Dados da tarefa encontrada.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(TaskList), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var task = await _taskService.GetByIdAsync(id);

            if (task == null)
                return NotFound();

            return Ok(task);
        }

        /// <summary>
        /// Cadastra uma nova tarefa.
        /// </summary>
        /// <param name="task">Dados da tarefa que será cadastrada.</param>
        /// <returns>Dados da tarefa criada.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(TaskList), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(TaskList task)
        {
            var newTask = new TaskList(
                task.NameTask,
                task.Description,
                task.DueDate
            );

            await _taskService.AddAsync(newTask);

            return CreatedAtAction(
                nameof(GetById),
                new { id = newTask.Id },
                newTask
            );
        }

        /// <summary>
        /// Atualiza os dados de uma tarefa existente.
        /// </summary>
        /// <param name="id">Identificador da tarefa que será atualizada.</param>
        /// <param name="task">Novos dados da tarefa.</param>
        /// <returns>Resultado da atualização.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(
            Guid id,
            TaskList task)
        {
            if (id != task.Id)
                return BadRequest("O ID da rota é diferente do ID da tarefa.");

            var existingTask = await _taskService.GetByIdAsync(id);

            if (existingTask == null)
                return NotFound();

            await _taskService.UpdateAsync(task);

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
    }
}
