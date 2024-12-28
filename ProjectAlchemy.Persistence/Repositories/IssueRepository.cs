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

    public async Task<Issue?> GetById(int issueId, string projectId)
    {
        var issue = await _context.Issues.FirstOrDefaultAsync(i => i.Id == issueId && i.ProjectId == projectId);
        if (issue == null || issue.Deleted)
        {
            return null;
        }
        var lane = await _context.Lanes.FirstOrDefaultAsync(l => l.Id == issue.LaneId);
        return lane == null ? null : IssueEntity.ToIssue(issue, LaneEntity.ToLane(lane));
    }

    public async Task<IssuePartial> Create(IssueCreate issue, string projectId)
    {
        var entity = IssueEntity.FromIssueCreate(issue);
        entity.ProjectId = projectId;
        await _context.Issues.AddAsync(entity);
        await _context.SaveChangesAsync();
        var lane = await _context.Lanes.FindAsync(entity.LaneId);
        return IssueEntity.ToPartial(entity, LaneEntity.ToLane(lane!));
    }

    public async Task<Issue> Update(Issue updated, string projectId)
    {
        var entity = IssueEntity.FromIssue(updated);
        entity.ProjectId = projectId;
        _context.ChangeTracker.Clear();
        _context.Update(entity);
        if (entity.Deleted)
        {
            return updated;
        }
        var lane = await _context.Lanes.FindAsync(entity.LaneId);
        await _context.SaveChangesAsync();
        return IssueEntity.ToIssue(entity, LaneEntity.ToLane(lane!));
    }

    public async Task DeleteById(int id)
    {
        var issue = await _context.Issues.FindAsync(id);
        if (issue != null)
        {
            issue.Deleted = true;
            await _context.SaveChangesAsync();
        }
    }
}