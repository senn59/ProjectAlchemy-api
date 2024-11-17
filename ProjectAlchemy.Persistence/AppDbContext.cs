using Microsoft.EntityFrameworkCore;
using ProjectAlchemy.Persistence.Entities;

namespace ProjectAlchemy.Persistence;

public class AppDbContext(string connectionString) : DbContext
{
    public DbSet<IssueEntity> Issues { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0)));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<IssueEntity>()
            .Property(p => p.Id)
            .ValueGeneratedOnAdd();
    }
}