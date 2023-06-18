using DemoBlogAppProject.Models.DomainModel;
using Microsoft.EntityFrameworkCore;

namespace DemoBlogAppProject.DB
{
    public class Appdbcontext : DbContext
    {
        public Appdbcontext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Tag> Tags { get; set; }
    }
}
