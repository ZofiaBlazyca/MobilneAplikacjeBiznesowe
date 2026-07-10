using MediatR;
using WorkoutPlanner.API.Data;
using WorkoutPlanner.API.Models;

namespace WorkoutPlanner.API.Features.WorkoutPlans.Commands.CreateWorkoutPlan;

public class CreateWorkoutPlanHandler
    : IRequestHandler<CreateWorkoutPlanCommand, int>
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<CreateWorkoutPlanHandler> _logger;

    public CreateWorkoutPlanHandler(
        ApplicationDbContext context,
        ILogger<CreateWorkoutPlanHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<int> Handle(
        CreateWorkoutPlanCommand request,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating workout plan {Name}",
            request.Name);

        var workoutPlan = new WorkoutPlan
        {
            Name = request.Name,
            Description = request.Description,
            IdUserProfile = request.IdUserProfile,
            IsActive = true
        };

        _context.WorkoutPlans.Add(workoutPlan);

        await _context.SaveChangesAsync(cancellationToken);

        return workoutPlan.IdWorkoutPlan;
    }
}