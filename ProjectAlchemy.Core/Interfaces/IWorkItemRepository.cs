using ProjectAlchemy.Core.Domain;
using ProjectAlchemy.Core.Dtos;

namespace ProjectAlchemy.Core.Interfaces;

public interface IWorkItemRepository
{
    public WorkItem GetById(int id);
    public void Create(WorkItem workItem);
    public List<WorkItem> GetAll();
    public void Update(int id, UpdateWorkItemRequest request);
}