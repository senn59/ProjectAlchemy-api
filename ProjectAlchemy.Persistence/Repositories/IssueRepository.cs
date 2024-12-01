using Microsoft.EntityFrameworkCore;
using ProjectAlchemy.Core.Domain;
using ProjectAlchemy.Core.Interfaces;
using ProjectAlchemy.Persistence.Entities;

namespace ProjectAlchemy.Persistence.Repositories;

public class IssueRepository: IIssueRepository
{
    private AppDbContext _context;
    
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

    public async Task Create(Issue item, string projectId)
    {
        var record = IssueEntity.FromIssue(item);
        record.ProjectId = projectId;
        await _context.Issues.AddAsync(IssueEntity.FromIssue(item));
        await _context.SaveChangesAsync();
    }

    public async Task Update(Issue updated)
    {
        _context.ChangeTracker.Clear();
        _context.Update(IssueEntity.FromIssue(updated)); 
        await _context.SaveChangesAsync();
    }

    public async Task<bool> IsInProject(int issueId, string projectId)
    {
        return await _context.Issues.AnyAsync(i => i.Id == issueId && i.ProjectId == projectId);
    }

    public async Task DeleteById(int id)
    {
        var issue = await _context.Issues.FindAsync(id);
        if (issue != null)
        {
            _context.Remove(issue);
        }
    }
}