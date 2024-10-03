using Microsoft.EntityFrameworkCore;
using ProjectAlchemy.Persistence.Entities;

namespace ProjectAlchemy.Persistence;

public class AppDbContext(string connectionString) : DbContext
{
    public DbSet<WorkItemEntity> WorkItems { get; set; }
    private string _connectionString = connectionString;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<WorkItemEntity>()
            .Property(p => p.Id)
            .ValueGeneratedOnAdd();
    }
}