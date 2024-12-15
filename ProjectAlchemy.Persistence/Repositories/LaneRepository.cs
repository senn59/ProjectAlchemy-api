using Microsoft.EntityFrameworkCore;
using ProjectAlchemy.Core.Domain;
using ProjectAlchemy.Core.Interfaces;
using ProjectAlchemy.Persistence.Entities;

namespace ProjectAlchemy.Persistence.Repositories;

public class LaneRepository: ILaneRepository
{
    private readonly AppDbContext _context;

    public LaneRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<Lane?> GetLaneById(int laneId, string projectId)
    {
        var entity = await _context.Lanes.FirstOrDefaultAsync(l => l.Id == laneId && l.ProjectId == projectId);
        return entity == null ? null : LaneEntity.ToLane(entity);
    }
}