using ProjectAlchemy.Core.Domain;
using ProjectAlchemy.Core.Interfaces;

namespace ProjectAlchemy.Core.Services;

public class WorkItemService(IWorkItemRepository workItemRepository)
{
    private IWorkItemRepository _workItemRepository = workItemRepository;

    public WorkItem Create(WorkItem item)
    {
        return _workItemRepository.Create(item);
    }

    public WorkItem GetById(int id)
    {
        return _workItemRepository.GetById(id);
    }

    public List<WorkItem> GetAll()
    {
        return _workItemRepository.GetAll();
    }

    public WorkItem Update(WorkItem item)
    {
        return _workItemRepository.Update(item);
    }
}