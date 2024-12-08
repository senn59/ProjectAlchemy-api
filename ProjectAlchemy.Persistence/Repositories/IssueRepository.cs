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
        var issue = await _context.Issues.FirstOrDefaultAsync(i => i.Id == id);
        return issue == null ? null : IssueEntity.ToIssue(issue);
    }

    public async Task<Issue> Create(Issue item)
    {
        var created = await _context.Issues.AddAsync(IssueEntity.FromIssue(item));
        await _context.SaveChangesAsync();
        return IssueEntity.ToIssue(created.Entity);
    }

    public List<Issue> GetAll()
    {
        return _context.Issues.Select(IssueEntity.ToIssue).ToList();
    }

    public async Task<Issue> Update(Issue updated)
    {
        _context.ChangeTracker.Clear();
        var updatedIssue = _context.Update(IssueEntity.FromIssue(updated));
        await _context.SaveChangesAsync();
        return IssueEntity.ToIssue(updatedIssue.Entity);
    }

    public void DeleteById(int id)
    {
        var issue = _context.Issues.First(i => i.Id == id);
        _context.Remove(issue);
    }
}