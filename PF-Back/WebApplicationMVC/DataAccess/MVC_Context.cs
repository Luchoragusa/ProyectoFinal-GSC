using Entities;
using Microsoft.EntityFrameworkCore;

namespace WebApplicationMVC.DataAccess
{
    public class MVC_Context : DbContext
    {
        public MVC_Context(DbContextOptions<MVC_Context> options) : base(options)
        {
            // Database.EnsureCreated();
        }

        public DbSet<Thing> Things { get; set; }
    }
}
