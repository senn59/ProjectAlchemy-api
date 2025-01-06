using ProjectAlchemy.Core.Dtos;

namespace ProjectAlchemy.Core.Interfaces;

public interface ILaneRepository
{
    Task<Lane?> GetLaneById(Guid laneId, Guid projectId);
}