using Adaptteen.DataAccess.Model;
using Microsoft.EntityFrameworkCore;

namespace Adaptteen.DataAccess.Context
{
    public class ConfigDbContext : DbContext
    {
        public ConfigDbContext() { }
        public ConfigDbContext(DbContextOptions<ConfigDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Course> Course { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
