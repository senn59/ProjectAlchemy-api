using ProjectAlchemy.Core.Domain;

namespace ProjectAlchemy.Core.Interfaces;

public interface IWorkItemRepository
{
    public WorkItem GetById(int id);
    public WorkItem Create(WorkItem item);
    public List<WorkItem> GetAll();
    public WorkItem Update(WorkItem item);
}