using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkoutPlanner.API.Data;
using WorkoutPlanner.API.Features.Goals.Queries.GetAllGoals;

namespace WorkoutPlanner.API.Features.Goals.Queries.GetGoalById;

public class GetGoalByIdHandler
    : IRequestHandler<GetGoalByIdQuery, GoalDto?>
{
    private readonly ApplicationDbContext _context;

    public GetGoalByIdHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<GoalDto?> Handle(
        GetGoalByIdQuery request,
        CancellationToken cancellationToken)
    {
        return await _context.Goals
            .Where(x => x.IdGoal == request.Id)
            .Select(x => new GoalDto
            {
                IdGoal = x.IdGoal,
                Name = x.Name,
                Description = x.Description,
                TargetDate = x.TargetDate,
                IsActive = x.IsActive
            })
            .FirstOrDefaultAsync(cancellationToken);
    }
}