using Microsoft.EntityFrameworkCore;
using SpaceshipService.Models;

namespace SpaceshipService.Context
{
    public class DatabaseContext: DbContext
    {
        public DbSet<Spaceship> Spaceships { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = "Server=localhost;Database=SpaceshipDB;User Id=sa;Password=SQLDatabase123;";
            optionsBuilder.UseSqlServer(connectionString);
            optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.EnableDetailedErrors();

            base.OnConfiguring(optionsBuilder);
        }
    }
}
