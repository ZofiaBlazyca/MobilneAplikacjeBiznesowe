using MediatR;

namespace WorkoutPlanner.API.Features.WorkoutPlanExercises.Commands.CreateWorkoutPlanExercise;

public class CreateWorkoutPlanExerciseCommand : IRequest<int>
{
    public int IdWorkoutPlan { get; set; }

    public int IdExercise { get; set; }

    public int Sets { get; set; }

    public int Reps { get; set; }

    public int? DurationSeconds { get; set; }

    public int OrderNumber { get; set; }
}