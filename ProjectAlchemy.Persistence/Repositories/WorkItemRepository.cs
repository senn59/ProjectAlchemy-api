using ProjectAlchemy.Core.Domain;
using ProjectAlchemy.Core.Interfaces;
using ProjectAlchemy.Persistence.Entities;

namespace ProjectAlchemy.Persistence.Repositories;

public class WorkItemRepository: IWorkItemRepository
{
    private AppDbContext _context;
    
    public WorkItemRepository(AppDbContext context)
    {
        _context = context;
    }

    public WorkItem GetById(int id)
    {
        var item = _context.WorkItems.First(w => w.Id == id);
        return WorkItemEntity.ToWorkItem(item);
    }

    public WorkItem Create(WorkItem item)
    {
        var createdItem = _context.WorkItems.Add(WorkItemEntity.FromWorkitem(item));
        _context.SaveChanges();
        return WorkItemEntity.ToWorkItem(createdItem.Entity);
    }

    public List<WorkItem> GetAll()
    {
        return _context.WorkItems.Select(WorkItemEntity.ToWorkItem).ToList();
    }

    public WorkItem Update(WorkItem updated)
    {
        var updatedItem = _context.Update(WorkItemEntity.FromWorkitem(updated));
        _context.SaveChanges();
        return WorkItemEntity.ToWorkItem(updatedItem.Entity);
    }
}