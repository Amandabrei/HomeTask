using HomeTask.HomeTask.Api.DTOs.Requests;
using HomeTask.HomeTask.Api.DTOs.Responses;
using HomeTask.HomeTask.Core.Entities;
using HomeTask.HomeTask.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace HomeTask.HomeTask.Api.Controllers
{
    /// <summary>
    /// Gerencia as operações de cadastro e manutenção dos membros da família.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class FamilyMemberController : ControllerBase
    {
        private readonly FamilyMemberService _familyMemberService;

        /// <summary>
        /// Inicializa o controller de membros da família.
        /// </summary>
        /// <param name="familyMemberService">
        /// Serviço responsável pelas operações dos membros da família.
        /// </param>
        public FamilyMemberController(FamilyMemberService familyMemberService)
        {
            _familyMemberService = familyMemberService;
        }

        /// <summary>
        /// Retorna todos os membros da família cadastrados.
        /// </summary>
        /// <returns>Lista de membros da família.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<FamilyMemberResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var familyMembers = await _familyMemberService.GetAllAsync();

            var response = familyMembers.Select(member => MapToResponse(member));

            return Ok(response);
        }

        /// <summary>
        /// Busca um membro da família pelo identificador.
        /// </summary>
        /// <param name="id">Identificador único do membro.</param>
        /// <returns>Dados do membro encontrado.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(FamilyMemberResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var familyMember = await _familyMemberService.GetByIdAsync(id);

            if (familyMember == null)
                return NotFound();

            return Ok(MapToResponse(familyMember));
        }

        /// <summary>
        /// Cadastra um novo membro da família.
        /// </summary>
        /// <param name="request">Dados do membro que será cadastrado.</param>
        /// <returns>Dados do membro criado.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(FamilyMemberResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(FamilyMemberCreateRequest request)
        {
            var newFamilyMember = new FamilyMember(
                request.Name,
                request.Email
            );

            await _familyMemberService.AddAsync(newFamilyMember);

            var response = MapToResponse(newFamilyMember);

            return CreatedAtAction(
                nameof(GetById),
                new { id = newFamilyMember.Id },
                response
            );
        }

        /// <summary>
        /// Atualiza os dados de um membro da família existente.
        /// </summary>
        /// <param name="id">Identificador do membro que será atualizado.</param>
        /// <param name="request">Novos dados do membro.</param>
        /// <returns>Resultado da atualização.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(
            Guid id,
            FamilyMemberUpdateRequest request)
        {
            var existingMember = await _familyMemberService.GetByIdAsync(id);

            if (existingMember == null)
                return NotFound();

            existingMember.Name = request.Name;
            existingMember.Email = request.Email;

            await _familyMemberService.UpdateAsync(existingMember);

            return NoContent();
        }

        /// <summary>
        /// Exclui um membro da família pelo identificador.
        /// </summary>
        /// <param name="id">Identificador do membro que será excluído.</param>
        /// <returns>Resultado da exclusão.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var existingMember = await _familyMemberService.GetByIdAsync(id);

            if (existingMember == null)
                return NotFound();

            await _familyMemberService.DeleteAsync(id);

            return NoContent();
        }

        private static FamilyMemberResponse MapToResponse(FamilyMember member)
        {
            return new FamilyMemberResponse
            {
                Id = member.Id,
                Name = member.Name,
                Email = member.Email,
                CreatedAt = member.CreatedAt,
                IsActive = member.IsActive
            };
        }
    }
}
