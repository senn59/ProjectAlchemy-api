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

    public async Task<Issue?> GetByKey(int issueKey, string projectId)
    {
        var issue = await _context.Issues.FirstOrDefaultAsync(i => i.Key == issueKey && i.ProjectId == projectId);
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
        entity.Key = await _context.Issues.CountAsync(i => i.ProjectId == projectId) + 1;
        await _context.Issues.AddAsync(entity);
        await _context.SaveChangesAsync();
        var lane = await _context.Lanes.FindAsync(entity.LaneId);
        return IssueEntity.ToPartial(entity, LaneEntity.ToLane(lane!));
    }

    public async Task<Issue> Update(Issue updated, string projectId)
    {
        var entity = IssueEntity.FromIssue(updated);
        var id = _context.Issues.FirstAsync(i => i.Key == updated.Key && i.ProjectId == projectId).Result.Id;
        entity.Id = id;
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

    public async Task DeleteByKey(int key, string projectId)
    {
        var issue = await _context.Issues.FirstOrDefaultAsync(i => i.ProjectId == projectId && i.Key == key);
        if (issue != null)
        {
            issue.Deleted = true;
            await _context.SaveChangesAsync();
        }
    }
}