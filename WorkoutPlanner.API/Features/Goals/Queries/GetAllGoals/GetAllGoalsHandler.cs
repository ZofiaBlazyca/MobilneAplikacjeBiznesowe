using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkoutPlanner.API.Data;

namespace WorkoutPlanner.API.Features.Goals.Queries.GetAllGoals;

public class GetAllGoalsHandler
    : IRequestHandler<GetAllGoalsQuery, List<GoalDto>>
{
    private readonly ApplicationDbContext _context;

    public GetAllGoalsHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<GoalDto>> Handle(
        GetAllGoalsQuery request,
        CancellationToken cancellationToken)
    {
        return await _context.Goals
            .Where(x => x.IsActive)
            .OrderBy(x => x.TargetDate)
            .Select(x => new GoalDto
            {
                IdGoal = x.IdGoal,
                Name = x.Name,
                Description = x.Description,
                TargetDate = x.TargetDate,
                IsActive = x.IsActive
            })
            .ToListAsync(cancellationToken);
    }
}