using Entities;
using Microsoft.EntityFrameworkCore;


namespace Dal
{
    public class NamesContext : DbContext
    {

        public NamesContext(DbContextOptions<NamesContext> options)
                 : base(options) { }

        public DbSet<Name> Names { get; set; }
        public DbSet<Person> Persons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Name>()
                .HasIndex(n => n.NickName)
                .IsUnique();

            modelBuilder.Entity<Name>()
                 .HasOne(n => n.Person)  // Specify the navigation property
                 .WithOne(p => p.Name)  // Specify the inverse navigation property
                 .HasForeignKey<Name>(n =>n.PersonId);

        }
        
    }
}