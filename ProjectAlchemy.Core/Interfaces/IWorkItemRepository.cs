using ProjectAlchemy.Core.Domain;

namespace ProjectAlchemy.Core.Interfaces;

public interface IWorkItemRepository
{
    public WorkItem GetById(int id);
    public void Create(WorkItem workItem);
    public List<WorkItem> GetAll();
}