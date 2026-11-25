using HomeTask.HomeTask.Core.Entities;
using HomeTask.HomeTask.Core.Interfaces;

namespace HomeTask.HomeTask.Core.Services
{
    public class FamilyMemberService
    {
        private readonly IFamilyMemberRepository _familyMemberRepository;

        public FamilyMemberService(
            IFamilyMemberRepository familyMemberRepository)
        {
            _familyMemberRepository = familyMemberRepository;
        }

        public async Task<IEnumerable<FamilyMember>> GetAllAsync()
        {
            return await _familyMemberRepository.GetAllAsync();
        }

        public async Task<FamilyMember?> GetByIdAsync(Guid id)
        {
            return await _familyMemberRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(FamilyMember familyMember)
        {
            if (string.IsNullOrWhiteSpace(familyMember.Name))
                throw new ArgumentException("O nome é obrigatório.");

            await _familyMemberRepository.AddAsync(familyMember);
        }

        public async Task UpdateAsync(FamilyMember familyMember)
        {
            if (string.IsNullOrWhiteSpace(familyMember.Name))
                throw new ArgumentException("O nome é obrigatório.");

            await _familyMemberRepository.UpdateAsync(familyMember);
        }

        public async Task DeleteAsync(Guid id)
        {
            var familyMember = await _familyMemberRepository.GetByIdAsync(id);

            if (familyMember == null)
                throw new KeyNotFoundException("Integrante não encontrado.");

            await _familyMemberRepository.DeleteAsync(id);
        }
    }
}
