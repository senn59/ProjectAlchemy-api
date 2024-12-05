using Microsoft.EntityFrameworkCore;
using ProjectAlchemy.Core.Domain;
using ProjectAlchemy.Persistence.Entities;

namespace ProjectAlchemy.Persistence;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<IssueEntity> Issues { get; set; }
    public DbSet<ProjectEntity> Projects { get; set; }
    public DbSet<LaneEntity> Lanes { get; set; }
    public DbSet<MemberEntity> Members { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProjectEntity>()
            .HasKey(p => p.Id);
        modelBuilder.Entity<IssueEntity>()
            .Property(p => p.Id)
            .ValueGeneratedOnAdd();
        modelBuilder.Entity<LaneEntity>()
            .Property(p => p.Id)
            .ValueGeneratedOnAdd();
        modelBuilder.Entity<MemberEntity>()
            .Property(p => p.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<ProjectEntity>()
            .HasMany(p => p.Issues)
            .WithOne(i => i.Project)
            .HasForeignKey(i => i.ProjectId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
        
        modelBuilder.Entity<ProjectEntity>()
            .HasMany(p => p.Lanes)
            .WithOne(i => i.Project)
            .HasForeignKey(i => i.ProjectId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
        
        modelBuilder.Entity<ProjectEntity>()
            .HasMany(p => p.Members)
            .WithOne(i => i.Project)
            .HasForeignKey(m => m.ProjectId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
    }
}