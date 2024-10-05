using ProjectAlchemy.Core.Domain;

namespace ProjectAlchemy.Core.Interfaces;

public interface IIssueRepository
{
    public Issue GetById(int id);
    public Issue Create(Issue item);
    public List<Issue> GetAll();
    public Issue Update(Issue item);
}