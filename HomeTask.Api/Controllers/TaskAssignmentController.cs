using HomeTask.HomeTask.Api.DTOs.Requests;
using HomeTask.HomeTask.Api.DTOs.Responses;
using HomeTask.HomeTask.Core.Entities;
using HomeTask.HomeTask.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace HomeTask.HomeTask.Api.Controllers
{
    /// <summary>
    /// Gerencia as atribuições de tarefas aos membros da família.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class TaskAssignmentController : ControllerBase
    {
        private readonly TaskAssignmentService _assignmentService;

        /// <summary>
        /// Inicializa o controller de atribuições.
        /// </summary>
        /// <param name="assignmentService">Serviço responsável pelas atribuições.</param>
        public TaskAssignmentController(TaskAssignmentService assignmentService)
        {
            _assignmentService = assignmentService;
        }

        /// <summary>
        /// Retorna todas as atribuições cadastradas.
        /// </summary>
        /// <returns>Lista de atribuições.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<TaskAssignmentResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var assignments = await _assignmentService.GetAllAsync();

            var response = assignments.Select(MapToResponse);

            return Ok(response);
        }

        /// <summary>
        /// Busca uma atribuição pelo identificador.
        /// </summary>
        /// <param name="id">Identificador único da atribuição.</param>
        /// <returns>Dados da atribuição encontrada.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(TaskAssignmentResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var assignment = await _assignmentService.GetByIdAsync(id);

            if (assignment == null)
                return NotFound();

            return Ok(MapToResponse(assignment));
        }

        /// <summary>
        /// Retorna as atribuições vinculadas a uma tarefa.
        /// </summary>
        /// <param name="taskId">Identificador da tarefa.</param>
        /// <returns>Lista de atribuições da tarefa.</returns>
        [HttpGet("task/{taskId}")]
        [ProducesResponseType(typeof(IEnumerable<TaskAssignmentResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByTaskId(Guid taskId)
        {
            var assignments = await _assignmentService.GetByTaskIdAsync(taskId);

            var response = assignments.Select(MapToResponse);

            return Ok(response);
        }

        /// <summary>
        /// Retorna as atribuições vinculadas a um membro da família.
        /// </summary>
        /// <param name="familyMemberId">Identificador do membro da família.</param>
        /// <returns>Lista de atribuições do membro.</returns>
        [HttpGet("member/{familyMemberId}")]
        [ProducesResponseType(typeof(IEnumerable<TaskAssignmentResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByFamilyMemberId(Guid familyMemberId)
        {
            var assignments = await _assignmentService.GetByFamilyMemberIdAsync(familyMemberId);

            var response = assignments.Select(MapToResponse);

            return Ok(response);
        }

        /// <summary>
        /// Cria uma atribuição de tarefa para um membro da família.
        /// </summary>
        /// <param name="request">Dados da atribuição.</param>
        /// <returns>Dados da atribuição criada.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(TaskAssignmentResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(TaskAssignmentCreateRequest request)
        {
            var newAssignment = new TaskAssignment(
                request.TaskListId,
                request.FamilyMemberId
            );

            await _assignmentService.AddAsync(newAssignment);

            var response = MapToResponse(newAssignment);

            return CreatedAtAction(
                nameof(GetById),
                new { id = newAssignment.Id },
                response
            );
        }

        /// <summary>
        /// Atualiza uma atribuição existente.
        /// </summary>
        /// <param name="id">Identificador da atribuição.</param>
        /// <param name="request">Novos dados da atribuição.</param>
        /// <returns>Resultado da atualização.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(
            Guid id,
            TaskAssignmentUpdateRequest request)
        {
            var existingAssignment = await _assignmentService.GetByIdAsync(id);

            if (existingAssignment == null)
                return NotFound();

            existingAssignment.TaskListId = request.TaskListId;
            existingAssignment.FamilyMemberId = request.FamilyMemberId;
            existingAssignment.CompletedAt = request.CompletedAt;

            await _assignmentService.UpdateAsync(existingAssignment);

            return NoContent();
        }

        /// <summary>
        /// Remove uma atribuição pelo identificador.
        /// </summary>
        /// <param name="id">Identificador da atribuição.</param>
        /// <returns>Resultado da exclusão.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var existingAssignment = await _assignmentService.GetByIdAsync(id);

            if (existingAssignment == null)
                return NotFound();

            await _assignmentService.DeleteAsync(id);

            return NoContent();
        }

        private static TaskAssignmentResponse MapToResponse(TaskAssignment assignment)
        {
            return new TaskAssignmentResponse
            {
                Id = assignment.Id,
                TaskListId = assignment.TaskListId,
                FamilyMemberId = assignment.FamilyMemberId,
                AssignedAt = assignment.AssignedAt,
                CompletedAt = assignment.CompletedAt
            };
        }
    }
}
