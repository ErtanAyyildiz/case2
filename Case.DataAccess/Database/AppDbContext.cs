using Case.Models;
using Microsoft.EntityFrameworkCore;

namespace Case.DataAccess.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Person> People { get; set; }
    }
}
