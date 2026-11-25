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
        /// <param name="familyMemberService">Serviço responsável pelas operações dos membros.</param>
        public FamilyMemberController(FamilyMemberService familyMemberService)
        {
            _familyMemberService = familyMemberService;
        }

        /// <summary>
        /// Retorna todos os membros cadastrados na família.
        /// </summary>
        /// <returns>Lista de membros da família.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var familyMembers = await _familyMemberService.GetAllAsync();
            return Ok(familyMembers);
        }

        /// <summary>
        /// Busca um membro da família pelo identificador.
        /// </summary>
        /// <param name="id">Identificador único do membro.</param>
        /// <returns>Dados do membro encontrado.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(FamilyMember), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var familyMember = await _familyMemberService.GetByIdAsync(id);

            if (familyMember == null)
                return NotFound();

            return Ok(familyMember);
        }

        /// <summary>
        /// Cadastra um novo membro da família.
        /// </summary>
        /// <param name="familyMember">Dados do membro que será cadastrado.</param>
        /// <returns>Dados do membro criado.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(FamilyMember), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(FamilyMember familyMember)
        {
            var newFamilyMember = new FamilyMember(
                familyMember.Name,
                familyMember.Email
            );

            await _familyMemberService.AddAsync(newFamilyMember);

            return CreatedAtAction(
                nameof(GetById),
                new { id = newFamilyMember.Id },
                newFamilyMember
            );
        }

        /// <summary>
        /// Atualiza os dados de um membro da família.
        /// </summary>
        /// <param name="id">Identificador do membro que será atualizado.</param>
        /// <param name="familyMember">Novos dados do membro.</param>
        /// <returns>Resultado da operação de atualização.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(
            Guid id,
            FamilyMember familyMember)
        {
            if (id != familyMember.Id)
                return BadRequest("O ID da rota é diferente do ID do integrante.");

            var existingMember = await _familyMemberService.GetByIdAsync(id);

            if (existingMember == null)
                return NotFound();

            await _familyMemberService.UpdateAsync(familyMember);

            return NoContent();
        }

        /// <summary>
        /// Remove um membro da família pelo identificador.
        /// </summary>
        /// <param name="id">Identificador do membro que será removido.</param>
        /// <returns>Resultado da operação de exclusão.</returns>
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
    }
}
