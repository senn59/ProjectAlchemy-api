using ProjectAlchemy.Core.Dtos;

namespace ProjectAlchemy.Core.Interfaces;

public interface ILaneRepository
{
    Task<Lane?> GetLaneById(string laneId, string projectId);
}