using HomeTask.HomeTask.Core.Entities;

namespace HomeTask.HomeTask.Core.Interfaces
{
    public interface IFamilyMemberRepository
    {
        Task<IEnumerable<FamilyMember>> GetAllAsync();

        Task<FamilyMember?> GetByIdAsync(Guid id);

        Task AddAsync(FamilyMember familyMember);

        Task UpdateAsync(FamilyMember familyMember);

        Task DeleteAsync(Guid id);
    }
}
