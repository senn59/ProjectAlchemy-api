using FluentAssertions;
using ProjectAlchemy.Core.Domain;
using ProjectAlchemy.Core.Exceptions;

namespace ProjectAlchemy.CoreTests;

public class Tests
{
    [Test]
    public async Task ProjectNameIsValided()
    {
        var validName = new string('*', Project.MaxNameLength);
        var tooLongName = new string('*', Project.MaxNameLength + 1);
        var emptyName = "";
        Action createProjectWithValidName = () => new Project(validName, [], [], []);
        Action createProjectWithShortName = () => new Project("1", [], [], []);
        Action createProjectWithTooLongName = () => new Project(tooLongName, [], [], []);
        Action createProjectWithEmptyName = () => new Project(emptyName, [], [], []);
        createProjectWithValidName.Should().NotThrow();
        createProjectWithShortName.Should().NotThrow();
        createProjectWithTooLongName.Should().Throw<InvalidArgument>();
        createProjectWithEmptyName.Should().Throw<InvalidArgument>();
    }
}