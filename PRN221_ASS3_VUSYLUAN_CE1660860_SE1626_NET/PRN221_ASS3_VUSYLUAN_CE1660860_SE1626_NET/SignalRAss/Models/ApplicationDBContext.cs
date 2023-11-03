using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace SignalRAss.Models
{
    public partial class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext() { }

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }

        public virtual DbSet<AppUser> AppUsers { get; set; } 
        public virtual DbSet<PostCategory> PostCategories { get; set; } 
        public virtual DbSet<Post> Posts { get; set; } 
    }
}
