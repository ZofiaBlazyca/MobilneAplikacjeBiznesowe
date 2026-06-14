using MediatR;
using WorkoutPlanner.API.Features.Goals.Queries.GetAllGoals;

namespace WorkoutPlanner.API.Features.Goals.Queries.GetGoalById;

public class GetGoalByIdQuery : IRequest<GoalDto?>
{
    public int Id { get; set; }

    public GetGoalByIdQuery(int id)
    {
        Id = id;
    }
}