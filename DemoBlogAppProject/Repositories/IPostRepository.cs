using DemoBlogAppProject.Models.DomainModel;

namespace DemoBlogAppProject.Repositories
{
    public interface IPostRepository
    {
        Task<IEnumerable<Post>> GetAllAsync();
        Task<Post?> GetAsync(Guid Id);
        Task<Post> AddAsync(Post post);
        Task<Post?> UpdateAsync(Post post);
        Task<Post?> DeleteAsync(Guid Id);
    }
}
