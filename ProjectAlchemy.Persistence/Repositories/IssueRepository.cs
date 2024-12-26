using Microsoft.EntityFrameworkCore;
using ProjectAlchemy.Core.Dtos;
using ProjectAlchemy.Core.Interfaces;
using ProjectAlchemy.Persistence.Entities;

namespace ProjectAlchemy.Persistence.Repositories;

public class IssueRepository: IIssueRepository
{
    private readonly AppDbContext _context;
    
    public IssueRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Issue?> GetById(int id)
    {
        var issue = await _context.Issues.FindAsync(id);
        var lane = await _context.Lanes.FirstOrDefaultAsync(l => issue != null && l.Id == issue.LaneId);

        return lane == null || issue == null ? null : IssueEntity.ToIssue(issue, LaneEntity.ToLane(lane));
    }

    public async Task<Issue> Create(IssueCreate issue, string projectId)
    {
        var entity = IssueEntity.FromIssueCreate(issue);
        entity.ProjectId = projectId;
        await _context.Issues.AddAsync(entity);
        await _context.SaveChangesAsync();
        var lane = await _context.Lanes.FindAsync(entity.LaneId);
        return IssueEntity.ToIssue(entity, LaneEntity.ToLane(lane!));
    }

    public async Task<Issue> Update(Issue updated, string projectId)
    {
        var entity = IssueEntity.FromIssue(updated);
        entity.ProjectId = projectId;
        _context.ChangeTracker.Clear();
        _context.Update(entity); 
        await _context.SaveChangesAsync();
        return IssueEntity.ToIssue(entity, updated.Lane);
    }

    public async Task DeleteById(int id)
    {
        var issue = await _context.Issues.FindAsync(id);
        if (issue != null)
        {
            _context.Remove(issue);
            await _context.SaveChangesAsync();
        }
    }
}