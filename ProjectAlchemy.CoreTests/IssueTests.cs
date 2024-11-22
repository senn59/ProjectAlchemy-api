using ProjectAlchemy.Core.Domain;
using ProjectAlchemy.Core.Exceptions;

namespace ProjectAlchemy.CoreTests;

public class Tests
{

    [Test]
    public async Task CreateAndStoreIssue()
    {
        var repository = new MockIssueRepository();
        var issue = new Issue(0, "issue1", IssueType.Task, "a new issue");
        await repository.Create(issue);
        var retrievedIssue = await repository.GetById(0);
        Assert.That(retrievedIssue, Is.EqualTo(issue));
    }
    
    [Test]
    public void GettingNonExistentIssue()
    {
        var repository = new MockIssueRepository();
        Assert.ThrowsAsync<NotFoundException>(() => repository.GetById(0));
    }
}