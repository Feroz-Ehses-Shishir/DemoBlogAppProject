using DemoBlogAppProject.Models.DomainModel;
using DemoBlogAppProject.Models.EditModel;

namespace DemoBlogAppProject.Repositories
{
    public interface ITagRepository
    {
        Task<IEnumerable<Tag>> GetAllAsync();
        Task<Tag?> GetAsync(Guid Id);
        Task<Tag> AddAsync(Tag tag);
        Task<Tag?> UpdateAsync(Tag tag);
        Task<Tag?> DeleteAsync(Guid Id);
    }
}
