using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkoutPlanner.API.Data;

namespace WorkoutPlanner.API.Features.Equipment.Queries.GetAllEquipment;

public class GetAllEquipmentHandler
    : IRequestHandler<GetAllEquipmentQuery, List<EquipmentDto>>
{
    private readonly ApplicationDbContext _context;

    public GetAllEquipmentHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<EquipmentDto>> Handle(
        GetAllEquipmentQuery request,
        CancellationToken cancellationToken)
    {
        return await _context.Equipment
            .Where(x => x.IsActive)
            .OrderBy(x => x.Name)
            .Select(x => new EquipmentDto
            {
                IdEquipment = x.IdEquipment,
                Name = x.Name,
                Description = x.Description,
                IsActive = x.IsActive
            })
            .ToListAsync(cancellationToken);
    }
}