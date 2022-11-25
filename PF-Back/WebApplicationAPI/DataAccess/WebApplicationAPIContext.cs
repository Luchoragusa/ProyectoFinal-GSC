using Entities;
using Microsoft.EntityFrameworkCore;

namespace WebApplicationAPI.DataAccess
{
    public class WebApplicationAPIContext : DbContext
    {
        public WebApplicationAPIContext(DbContextOptions<WebApplicationAPIContext> options) : base(options)
        {
            // Database.EnsureCreated();
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Person> Person { get; set; }
        public DbSet<Loan> Loans { get; set; }

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
 
        }
    }
}
