using ProjectAlchemy.Core.Domain;
using ProjectAlchemy.Core.Dtos;
using ProjectAlchemy.Core.Interfaces;

namespace ProjectAlchemy.Core.Services;

public class WorkItemService(IWorkItemRepository workItemRepository)
{
    private IWorkItemRepository _workItemRepository = workItemRepository;

    public void Create(CreateWorkItemRequest request)
    {
        var item = new WorkItem(request.Name, "");
        _workItemRepository.Create(item);
    }

    public WorkItemResponse GetById(int id)
    {
        var item = _workItemRepository.GetById(id);
        return WorkItemResponse.FromWorkItem(item);
    }

    public List<WorkItemResponse> GetAll()
    {
        var items = _workItemRepository.GetAll();
        return items.Select(WorkItemResponse.FromWorkItem).ToList();
    }

    public void Update(int id, UpdateWorkItemRequest request)
    {
        _workItemRepository.Update(id, request);
    }
}