using MediatR;
using WorkoutPlanner.API.Features.WorkoutPlans.Queries.GetAllWorkoutPlans;

namespace WorkoutPlanner.API.Features.WorkoutPlans.Queries.GetWorkoutPlanById;

public class GetWorkoutPlanByIdQuery : IRequest<WorkoutPlanDto?>
{
    public int Id { get; set; }

    public GetWorkoutPlanByIdQuery(int id)
    {
        Id = id;
    }
}