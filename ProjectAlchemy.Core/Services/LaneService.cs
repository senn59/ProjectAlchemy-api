using ProjectAlchemy.Core.Domain;
using ProjectAlchemy.Core.Exceptions;
using ProjectAlchemy.Core.Interfaces;

namespace ProjectAlchemy.Core.Services;

public class LaneService
{
    private readonly ILaneRepository _laneRepository;
    private readonly AuthorizationService _authService;
    
    public LaneService(ILaneRepository laneRepository, AuthorizationService authService)
    {
        _laneRepository = laneRepository;
        _authService = authService;
    }
    
    public async Task<Lane> GetLaneById(int laneId, string projectId, string userId)
    {
        await _authService.AuthorizeProjectAccess(userId, projectId);
        return await _laneRepository.GetLaneById(laneId, projectId) ?? throw new NotFoundException();
    }
}