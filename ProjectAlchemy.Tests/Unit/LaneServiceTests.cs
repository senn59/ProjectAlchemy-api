using FluentAssertions;
using ProjectAlchemy.Core.Services;

namespace ProjectAlchemy.Tests.Unit;

public class LaneServiceTests
{
    [Fact]
    public void CreatingDefaultLanesSucceedsWithUniqueItems()
    {
        var lanes = LaneService.GetDefaultLanes();
        var ids = lanes.Select(l => l.Id);
        var names = lanes.Select(l => l.Name);
        ids.Should().OnlyHaveUniqueItems();
        names.Should().OnlyHaveUniqueItems();
    }
    
    [Fact]
    public void CreatingDefaultLanesMultipleTimesAlwaysEnsuresIdsAreUnique()
    {
        var lanes = LaneService.GetDefaultLanes().ToList();
        lanes.AddRange(LaneService.GetDefaultLanes());
        lanes.AddRange(LaneService.GetDefaultLanes());
        lanes.Should().OnlyHaveUniqueItems();
    }
}