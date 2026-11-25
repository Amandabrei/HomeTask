using HomeTask.HomeTask.Core.Entities;
using HomeTask.HomeTask.Core.Interfaces;

namespace HomeTask.HomeTask.Infra.Repositories
{
    public class FamilyMemberRepository : IFamilyMemberRepository
    {
        private readonly List<FamilyMember> _familyMembers = new();

        public Task<IEnumerable<FamilyMember>> GetAllAsync()
        {
            return Task.FromResult<IEnumerable<FamilyMember>>(_familyMembers);
        }

        public Task<FamilyMember?> GetByIdAsync(Guid id)
        {
            var familyMember = _familyMembers.FirstOrDefault(x => x.Id == id);

            return Task.FromResult(familyMember);
        }

        public Task AddAsync(FamilyMember familyMember)
        {
            _familyMembers.Add(familyMember);

            return Task.CompletedTask;
        }

        public Task UpdateAsync(FamilyMember familyMember)
        {
            var existingMember = _familyMembers.FirstOrDefault(x => x.Id == familyMember.Id);

            if (existingMember != null)
            {
                existingMember.Name = familyMember.Name;
                existingMember.Email = familyMember.Email;
                existingMember.IsActive = familyMember.IsActive;
            }

            return Task.CompletedTask;
        }

        public Task DeleteAsync(Guid id)
        {
            var familyMember = _familyMembers.FirstOrDefault(x => x.Id == id);

            if (familyMember != null)
            {
                _familyMembers.Remove(familyMember);
            }

            return Task.CompletedTask;
        }
    }
}
