using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkoutPlanner.API.Data;
using WorkoutPlanner.API.Features.Equipment.Queries.GetAllEquipment;

namespace WorkoutPlanner.API.Features.Equipment.Queries.GetEquipmentById;

public class GetEquipmentByIdHandler
    : IRequestHandler<GetEquipmentByIdQuery, EquipmentDto?>
{
    private readonly ApplicationDbContext _context;

    public GetEquipmentByIdHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<EquipmentDto?> Handle(
        GetEquipmentByIdQuery request,
        CancellationToken cancellationToken)
    {
        return await _context.Equipment
            .Where(x => x.IdEquipment == request.Id)
            .Select(x => new EquipmentDto
            {
                IdEquipment = x.IdEquipment,
                Name = x.Name,
                Description = x.Description,
                IsActive = x.IsActive
            })
            .FirstOrDefaultAsync(cancellationToken);
    }
}