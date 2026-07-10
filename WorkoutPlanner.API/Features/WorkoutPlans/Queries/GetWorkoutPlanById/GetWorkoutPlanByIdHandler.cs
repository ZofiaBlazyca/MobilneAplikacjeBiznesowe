using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkoutPlanner.API.Data;
using WorkoutPlanner.API.Features.WorkoutPlans.Queries.GetAllWorkoutPlans;

namespace WorkoutPlanner.API.Features.WorkoutPlans.Queries.GetWorkoutPlanById;

public class GetWorkoutPlanByIdHandler
    : IRequestHandler<GetWorkoutPlanByIdQuery, WorkoutPlanDto?>
{
    private readonly ApplicationDbContext _context;

    public GetWorkoutPlanByIdHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<WorkoutPlanDto?> Handle(
        GetWorkoutPlanByIdQuery request,
        CancellationToken cancellationToken)
    {
        return await _context.WorkoutPlans
            .Include(x => x.UserProfile)
            .Where(x => x.IdWorkoutPlan == request.Id)
            .Select(x => new WorkoutPlanDto
            {
                IdWorkoutPlan = x.IdWorkoutPlan,
                Name = x.Name,
                Description = x.Description,
                IdUserProfile = x.IdUserProfile,
                UserName = x.UserProfile.Name,
                IsActive = x.IsActive
            })
            .FirstOrDefaultAsync(cancellationToken);
    }
}