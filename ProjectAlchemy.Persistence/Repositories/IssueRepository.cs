using Microsoft.EntityFrameworkCore;
using ProjectAlchemy.Core.Dtos;
using ProjectAlchemy.Core.Exceptions;
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

    public async Task<Issue?> GetByKey(int issueKey, Guid projectId)
    {
        var issue = await _context.Issues
            .Include(i => i.Lane)
            .FirstOrDefaultAsync(i => i.Key == issueKey && i.ProjectId == projectId);
        Console.WriteLine($"is null: {issue}");
        if (issue == null || issue.Deleted)
        {
            return null;
        }
        return IssueEntity.ToIssue(issue);
    }

    public async Task<IssuePartial> Create(IssueCreate issue, Guid projectId)
    {
        var entity = IssueEntity.FromIssueCreate(issue);
        entity.ProjectId = projectId;
        entity.Key = await _context.Issues.CountAsync(i => i.ProjectId == projectId) + 1;
        await _context.Issues.AddAsync(entity);
        await _context.SaveChangesAsync();
        var lane = await _context.Lanes.FindAsync(entity.LaneId);
        return IssueEntity.ToPartial(entity, LaneEntity.ToLane(lane!));
    }

    public async Task<Issue> Update(Issue updated, Guid projectId)
    {
        var entity = IssueEntity.FromIssue(updated);
        var id = _context.Issues
            .FirstAsync(i => i.Key == updated.Key && i.ProjectId == projectId).Result.Id;
        entity.Id = id;
        entity.ProjectId = projectId;
        _context.ChangeTracker.Clear();
        _context.Update(entity);
        if (entity.Deleted)
        {
            return updated;
        }
        var lane = await _context.Lanes.FindAsync(entity.LaneId);
        entity.Lane = lane!;
        await _context.SaveChangesAsync();
        return IssueEntity.ToIssue(entity);
    }

    public async Task DeleteByKey(int key, Guid projectId)
    {
        var issue = await _context.Issues.FirstOrDefaultAsync(i => i.ProjectId == projectId && i.Key == key);
        if (issue != null)
        {
            issue.Deleted = true;
            await _context.SaveChangesAsync();
        }
    }

    public async Task LinkIssues(int issueKey, IEnumerable<int> issueKeysToLink, Guid projectId)
    {
        var sourceIssue = await _context.Issues.FirstOrDefaultAsync(i => i.Key == issueKey && i.ProjectId == projectId);
        if (sourceIssue == null)
        {
            throw new NotFoundException();
        }

        issueKeysToLink = issueKeysToLink.Where(i => i != issueKey).Distinct();
        var issuesToLink = await _context.Issues
            .Where(i => issueKeysToLink.Contains(i.Key) && i.ProjectId == projectId)
            .ToListAsync();
        foreach (var issueEntity in issuesToLink)
        {
            issueEntity.RelatedIssues = issueEntity.RelatedIssues
                .Append(sourceIssue)
                .Distinct()
                .ToList();
        }
        sourceIssue.RelatedIssues = issuesToLink;
    }
}