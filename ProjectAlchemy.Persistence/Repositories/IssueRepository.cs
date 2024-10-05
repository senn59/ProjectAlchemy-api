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
        var item = _context.Issues.First(w => w.Id == id);
        return IssueEntity.ToIssue(item);
    }

    public Issue Create(Issue item)
    {
        var createdItem = _context.Issues.Add(IssueEntity.FromIssue(item));
        _context.SaveChanges();
        return IssueEntity.ToIssue(createdItem.Entity);
    }

    public List<Issue> GetAll()
    {
        return _context.Issues.Select(IssueEntity.ToIssue).ToList();
    }

    public Issue Update(Issue updated)
    {
        var updatedItem = _context.Update(IssueEntity.FromIssue(updated));
        _context.SaveChanges();
        return IssueEntity.ToIssue(updatedItem.Entity);
    }
}