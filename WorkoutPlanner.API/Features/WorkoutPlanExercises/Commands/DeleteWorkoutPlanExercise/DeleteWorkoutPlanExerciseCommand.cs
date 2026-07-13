using MediatR;

namespace WorkoutPlanner.API.Features.WorkoutPlanExercises.Commands.DeleteWorkoutPlanExercise;

public class DeleteWorkoutPlanExerciseCommand : IRequest<Unit>
{
    public int IdWorkoutPlanExercise { get; set; }

    public DeleteWorkoutPlanExerciseCommand(int idWorkoutPlanExercise)
    {
        IdWorkoutPlanExercise = idWorkoutPlanExercise;
    }
}