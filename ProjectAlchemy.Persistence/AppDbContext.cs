using Microsoft.EntityFrameworkCore;
using ProjectAlchemy.Core.Domain;
using ProjectAlchemy.Persistence.Entities;

namespace ProjectAlchemy.Persistence;

public class AppDbContext(string connectionString) : DbContext
{
    public DbSet<IssueEntity> Issues { get; set; }
    public DbSet<ProjectEntity> Projects { get; set; }
    public DbSet<LaneEntity> Lanes { get; set; }
    public DbSet<MemberEntity> Members { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProjectEntity>()
            .Property(p => p.Id)
            .ValueGeneratedOnAdd();
        modelBuilder.Entity<IssueEntity>()
            .Property(p => p.Id)
            .ValueGeneratedOnAdd();
        modelBuilder.Entity<LaneEntity>()
            .Property(p => p.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<IssueEntity>()
            .HasOne(i => i.Project)
            .WithMany(p => p.Issues)
            .HasForeignKey(i => i.ProjectId)
            .IsRequired();
        
        modelBuilder.Entity<LaneEntity>()
            .HasOne(i => i.Project)
            .WithMany(p => p.Lanes)
            .HasForeignKey(i => i.ProjectId)
            .IsRequired();
        
        modelBuilder.Entity<MemberEntity>()
            .HasOne(i => i.Project)
            .WithMany(p => p.Members)
            .HasForeignKey(i => i.ProjectId)
            .IsRequired();
    }
}