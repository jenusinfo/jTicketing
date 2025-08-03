using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

public class DepartmentService : IDepartmentService
{
    private readonly AppDbContext _context;
    private readonly ILogger<DepartmentService> _logger;

    public DepartmentService(AppDbContext context, ILogger<DepartmentService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<DepartmentDto>> GetAllAsync()
    {
        try
        {
            return await _context.Departments
                .Select(d => new DepartmentDto { Id = d.Id, Name = d.Name })
                .ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving departments");
            throw;
        }
    }
}
