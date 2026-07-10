using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkoutPlanner.API.Data;

namespace WorkoutPlanner.API.Features.WorkoutPlans.Queries.GetAllWorkoutPlans;

public class GetAllWorkoutPlansHandler
    : IRequestHandler<GetAllWorkoutPlansQuery, List<WorkoutPlanDto>>
{
    private readonly ApplicationDbContext _context;

    public GetAllWorkoutPlansHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<WorkoutPlanDto>> Handle(
        GetAllWorkoutPlansQuery request,
        CancellationToken cancellationToken)
    {
        return await _context.WorkoutPlans
            .Include(x => x.UserProfile)
            .Where(x => x.IsActive)
            .Select(x => new WorkoutPlanDto
            {
                IdWorkoutPlan = x.IdWorkoutPlan,
                Name = x.Name,
                Description = x.Description,
                IdUserProfile = x.IdUserProfile,
                UserName = x.UserProfile.Name,
                IsActive = x.IsActive
            })
            .ToListAsync(cancellationToken);
    }
}