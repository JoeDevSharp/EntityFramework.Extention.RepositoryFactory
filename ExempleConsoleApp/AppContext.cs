
using Microsoft.EntityFrameworkCore;

namespace ExempleConsoleApp
{
    public class AppContext : DbContext
    {
        public DbSet<Entities.User> Users { get; set; }
        public AppContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL($"server=localhost;database=EntityFramework.Extention.RepositoryFactory;user=root;password=root;");
        }
    }
}
