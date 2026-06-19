using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkoutPlanner.API.Data;

namespace WorkoutPlanner.API.Features.UserProfiles.Commands.UpdateUserProfile;

public class UpdateUserProfileHandler
    : IRequestHandler<UpdateUserProfileCommand, Unit>
{
    private readonly ApplicationDbContext _context;

    public UpdateUserProfileHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(
        UpdateUserProfileCommand request,
        CancellationToken cancellationToken)
    {
        var userProfile = await _context.UserProfiles
            .FirstOrDefaultAsync(x => x.IdUserProfile == request.IdUserProfile, cancellationToken);

        if (userProfile == null)
        {
            throw new KeyNotFoundException($"User profile {request.IdUserProfile} not found");
        }

        userProfile.Name = request.Name;
        userProfile.Email = request.Email;
        userProfile.IsActive = request.IsActive;

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}