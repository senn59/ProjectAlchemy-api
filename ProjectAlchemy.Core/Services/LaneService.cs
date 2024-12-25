using ProjectAlchemy.Core.Domain;
using ProjectAlchemy.Core.Exceptions;
using ProjectAlchemy.Core.Interfaces;

namespace ProjectAlchemy.Core.Services;

public class LaneService(ILaneRepository laneRepository, AuthorizationService authService)
{
    public async Task<Lane> GetById(int laneId, string projectId, string userId)
    {
        await authService.AuthorizeProjectAccess(userId, projectId);
        return await laneRepository.GetLaneById(laneId, projectId) ?? throw new NotFoundException("Not a valid lane in project");
    }
}