using Microsoft.EntityFrameworkCore;
using ProjectAlchemy.Persistence.Entities;

namespace ProjectAlchemy.Persistence;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<IssueEntity> Issues { get; init; }
    public DbSet<ProjectEntity> Projects { get; init; }
    public DbSet<LaneEntity> Lanes { get; init; }
    public DbSet<MemberEntity> Members { get; init; }
    public DbSet<InvitationEntity> Invitations { get; init; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProjectEntity>()
            .HasKey(p => p.Id);
        modelBuilder.Entity<IssueEntity>()
            .Property(p => p.Id)
            .ValueGeneratedOnAdd();
        modelBuilder.Entity<IssueEntity>()
            .Property(p => p.Deleted)
            .HasDefaultValue(false);
        modelBuilder.Entity<LaneEntity>()
            .Property(p => p.Id)
            .HasDefaultValue(Guid.NewGuid().ToString());
        modelBuilder.Entity<MemberEntity>()
            .Property(p => p.Id)
            .ValueGeneratedOnAdd();
        modelBuilder.Entity<InvitationEntity>()
            .Property(p => p.Id)
            .HasDefaultValue(Guid.NewGuid().ToString());

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