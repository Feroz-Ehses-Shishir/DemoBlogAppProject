using DemoBlogAppProject.DB;
using DemoBlogAppProject.Models.DomainModel;
using Microsoft.EntityFrameworkCore;

namespace DemoBlogAppProject.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly Appdbcontext db;

        public PostRepository(Appdbcontext db)
        {
            this.db = db;
        }

        public async Task<Post> AddAsync(Post post)
        {

            await db.Posts.AddAsync(post);
            await db.SaveChangesAsync();

            return post;
        }

        public async Task<Post?> DeleteAsync(Guid Id)
        {
            var post = await db.Posts.FindAsync(Id);

            if (post != null)
            {
                db.Posts.Remove(post);
                await db.SaveChangesAsync();

                return post;
            }

            return null;
        }

        public async Task<IEnumerable<Post>> GetAllAsync()
        {
            return await db.Posts.ToListAsync();
        }

        public async Task<Post?> GetAsync(Guid Id)
        {
            return await db.Posts.FindAsync(Id);
        }

        public async Task<Post?> UpdateAsync(Post post)
        {
            var newPost = await db.Posts.FindAsync(post.Id);

            if (newPost != null)
            {
                

                await db.SaveChangesAsync();

                return newPost;
            }

            return null;
        }
    }
}
