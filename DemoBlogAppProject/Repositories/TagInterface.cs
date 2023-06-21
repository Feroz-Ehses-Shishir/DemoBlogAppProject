using DemoBlogAppProject.Models.DomainModel;

namespace DemoBlogAppProject.Repositories
{
    public interface TagInterface
    {
        Task<IEnumerable<Tag>> GetAllAsync();
        Task<Tag?> GetAsync(Guid Id);
        Task<Tag> AddAsync(Tag tag);
        Task<Tag?> UpdateAsync(Tag tag);
        Task<Tag?> DeleteAsync(Guid Id);

    }
}
