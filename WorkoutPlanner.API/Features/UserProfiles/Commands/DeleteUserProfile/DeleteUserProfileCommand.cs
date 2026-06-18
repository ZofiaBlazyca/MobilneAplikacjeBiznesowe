using MediatR;

namespace WorkoutPlanner.API.Features.UserProfiles.Commands.DeleteUserProfile;

public class DeleteUserProfileCommand : IRequest<Unit>
{
    public int IdUserProfile { get; set; }

    public DeleteUserProfileCommand(int idUserProfile)
    {
        IdUserProfile = idUserProfile;
    }
}