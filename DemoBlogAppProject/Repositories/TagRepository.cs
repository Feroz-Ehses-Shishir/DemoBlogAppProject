using DemoBlogAppProject.DB;
using DemoBlogAppProject.Models.DomainModel;
using DemoBlogAppProject.Models.EditModel;
using Microsoft.EntityFrameworkCore;

namespace DemoBlogAppProject.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly Appdbcontext db;

        public TagRepository(Appdbcontext db)
        {
            this.db = db;
        }

        public async Task<Tag> AddAsync(Tag tag)
        {
            
            await db.Tags.AddAsync(tag);
            await db.SaveChangesAsync();

            return tag;
        }

        public async Task<Tag?> DeleteAsync(Guid Id)
        {
            var tag = await db.Tags.FindAsync(Id);

            if (tag != null)
            {
                db.Tags.Remove(tag);
                await db.SaveChangesAsync();

                return tag;
            }

            return null;
        }

        public async Task<IEnumerable<Tag>> GetAllAsync()
        {
            return await db.Tags.ToListAsync();
        }

        public async Task<Tag?> GetAsync(Guid Id)
        {
            return await db.Tags.FindAsync(Id);
        }

        public async Task<Tag?> UpdateAsync(Tag tag)
        {
            var newTag = await db.Tags.FindAsync(tag.Id);

            if (newTag != null)
            {
                newTag.Name = tag.Name;
                newTag.DisplayName = tag.DisplayName;

                await db.SaveChangesAsync();

                return newTag;
            }

            return null;
        }
    }
}
