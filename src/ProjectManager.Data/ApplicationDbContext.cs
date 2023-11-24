using Microsoft.EntityFrameworkCore;
using ProjectManager.Data.Entities;

namespace ProjectManager.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Todo> Todos { get; set; } = null!;

        public DbSet<Project> Projekts { get; set; } = null!;
        public ApplicationDbContext(DbContextOptions options) : base(options)
        { 
        }
    }
}
