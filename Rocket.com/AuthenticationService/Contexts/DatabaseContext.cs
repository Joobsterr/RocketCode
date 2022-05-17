using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using AuthenticationService.Models;
using System;

namespace AuthenticationService.Contexts
{
    public class DatabaseContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = "Server=localhost,1433;Database=RocketDB;User Id=sa;Password=SQLDatabase123;";
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.EnableDetailedErrors();

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasData(
                    new User() { Username = "Job",  Email = "Jobvanr@outlook.com", Password = "5e884898da28047151d0e56f8dc6292773603d0d6aabbdd62a11ef721d1542d8", CreationDate = DateTime.Now, LastLogin = DateTime.Now }
                ) ;
        }
    }
}