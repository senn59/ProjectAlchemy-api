using ProjectAlchemy.Core.Dtos;

namespace ProjectAlchemy.Core.Interfaces;

public interface ILaneRepository
{
    Task<Lane?> GetLaneById(int laneId, string projectId);
}