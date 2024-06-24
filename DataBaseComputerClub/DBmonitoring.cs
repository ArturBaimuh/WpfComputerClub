
using Domain.models;
using Microsoft.EntityFrameworkCore;
using СomputerСlub.Persistence.EfModels;

namespace DataBaseComputerClub
{
    public class DBmonitoring : DbContext
    {
        public DBmonitoring(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Computer> Computers { get; set; }
        public DbSet<Room> Rooms { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(
                  new EfModelPerson());
            modelBuilder.ApplyConfiguration(
                  new EfModelsComputer());
            modelBuilder.ApplyConfiguration(
                 new EfModelGameRoom());
        }
    }
}
