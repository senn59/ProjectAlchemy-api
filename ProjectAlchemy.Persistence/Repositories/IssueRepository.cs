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

    public Issue GetById(int id)
    {
        var issue = _context.Issues.First(i => i.Id == id);
        return IssueEntity.ToIssue(issue);
    }

    public Issue Create(Issue item)
    {
        var created = _context.Issues.Add(IssueEntity.FromIssue(item));
        _context.SaveChanges();
        return IssueEntity.ToIssue(created.Entity);
    }

    public List<Issue> GetAll()
    {
        return _context.Issues.Select(IssueEntity.ToIssue).ToList();
    }

    public Issue Update(Issue updated)
    {
        _context.ChangeTracker.Clear();
        var updatedIssue = _context.Update(IssueEntity.FromIssue(updated));
        _context.SaveChanges();
        return IssueEntity.ToIssue(updatedIssue.Entity);
    }

    public void DeleteById(int id)
    {
        var issue = _context.Issues.First(i => i.Id == id);
        _context.Remove(issue);
    }
}