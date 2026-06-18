using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkoutPlanner.API.Data;

namespace WorkoutPlanner.API.Features.UserProfiles.Commands.DeleteUserProfile;

public class DeleteUserProfileHandler
    : IRequestHandler<DeleteUserProfileCommand, Unit>
{
    private readonly ApplicationDbContext _context;

    public DeleteUserProfileHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(
        DeleteUserProfileCommand request,
        CancellationToken cancellationToken)
    {
        var userProfile = await _context.UserProfiles
            .FirstOrDefaultAsync(x => x.IdUserProfile == request.IdUserProfile, cancellationToken);

        if (userProfile == null)
        {
            throw new KeyNotFoundException($"User profile {request.IdUserProfile} not found");
        }

        userProfile.IsActive = false;

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}