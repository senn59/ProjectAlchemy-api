using ProjectAlchemy.Core.Dtos;
using ProjectAlchemy.Core.Enums;
using ProjectAlchemy.Core.Exceptions;
using ProjectAlchemy.Core.Interfaces;

namespace ProjectAlchemy.Core.Services;

public class LaneService(ILaneRepository laneRepository, IAuthorizationService authService)
{
    public const int MaxNameLength = 20;
    public async Task<Lane> GetById(string laneId, string projectId, string userId)
    {
        await authService.Authorize(Permission.ReadProject, userId, projectId);
        return await laneRepository.GetLaneById(laneId, projectId) ?? throw new NotFoundException("Not a valid lane in project");
    }
}