using Microsoft.EntityFrameworkCore;
using ProjectManager.Data.Entities;

namespace ProjectManager.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Todo> Todos { get; set; } 
        public ApplicationDbContext(DbContextOptions options) : base(options)
        { 
        }
    }
}
