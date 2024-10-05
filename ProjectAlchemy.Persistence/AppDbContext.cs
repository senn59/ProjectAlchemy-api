using Microsoft.EntityFrameworkCore;
using ProjectAlchemy.Persistence.Entities;

namespace ProjectAlchemy.Persistence;

public class AppDbContext(string connectionString) : DbContext
{
    public DbSet<IssueEntity> Issues { get; set; }
    private readonly string _connectionString = connectionString;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<IssueEntity>()
            .Property(p => p.Id)
            .ValueGeneratedOnAdd();
    }
}