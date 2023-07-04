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
            return await db.Posts.Include(x => x.Tags).ToListAsync();
        }

        public async Task<Post?> GetAsync(Guid Id)
        {
            return await db.Posts.Include(x => x.Tags).FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<Post?> UpdateAsync(Post post)
        {
            var newPost = await db.Posts.Include(x => x.Tags).FirstOrDefaultAsync(x => x.Id == post.Id);

            if (newPost != null)
            {
                newPost.Id = post.Id;
                newPost.Heading = post.Heading;
                newPost.PageTitle = post.PageTitle;
                newPost.Content = post.Content;
                newPost.ShortDescription = post.ShortDescription;
                newPost.Author = post.Author;
                newPost.FeaturedImageUrl = post.FeaturedImageUrl;
                newPost.UrlHandle = post.UrlHandle;
                newPost.Visible = post.Visible;
                newPost.PublishedDate = post.PublishedDate;
                newPost.Tags = post.Tags;

                await db.SaveChangesAsync();

                return newPost;
            }

            return null;
        }
    }
}
