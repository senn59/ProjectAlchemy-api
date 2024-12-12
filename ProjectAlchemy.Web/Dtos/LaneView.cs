using ProjectAlchemy.Core.Domain;

namespace ProjectAlchemy.Web.Dtos;

public class LaneView
{
    public int Id { get; init; }
    public string Name { get; init; }
    
    public static LaneView FromLane(Lane lane)
    {
        return new LaneView()
        {
            Id = lane.Id,
            Name = lane.Name
        };
    }
}