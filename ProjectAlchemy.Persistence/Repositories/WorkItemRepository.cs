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

    public void Create(WorkItem workItem)
    {
        _context.WorkItems.Add(WorkItemEntity.FromWorkitem(workItem));
        _context.SaveChanges();
    }

    public List<WorkItem> GetAll()
    {
        return _context.WorkItems.Select(WorkItemEntity.ToWorkItem).ToList();
    }
}