using Entities;
using Microsoft.EntityFrameworkCore;

namespace WebApplicationAPI.DataAccess
{
    public class WebApplicationAPIContext : DbContext
    {
        public WebApplicationAPIContext(DbContextOptions<WebApplicationAPIContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Thing> Things { get; set; }
        public DbSet<Person> Person { get; set; }
        public DbSet<Loan> Loans { get; set; }

        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Loan>()
            //    .HasOne(l => l.Person)
            //    .WithMany(p => p.Loans)
            //    .HasForeignKey(l => l.PersonId);

            //modelBuilder.Entity<Loan>()
            //    .HasOne(l => l.Thing)
            //    .WithMany(t => t.Loans)
            //    .HasForeignKey(l => l.ThingId);

            //modelBuilder.Entity<Person>().HasData(
            //    new Person
            //    {
            //        Name = "John Doe",
            //        Email = "JohnDoe@gmail.com",
            //        Phone = "123456789"
            //    },
            //    new Person
            //    {
            //        Name = "Lucho Ragusa",
            //        Email = "LuchoRagusa@gmail.com",
            //        Phone = "123456789"
            //    },
            //    new Person
            //    {
            //        Name = "Grupo SC",
            //        Email = "GrupoSC@gmail.com",
            //        Phone = "123456789"
            //    }
            //);
        }
    }
}
