using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

public class OrganizationService : IOrganizationService
{
    private readonly AppDbContext _context;
    private readonly ILogger<OrganizationService> _logger;

    public OrganizationService(AppDbContext context, ILogger<OrganizationService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<OrganizationDto>> GetAllAsync()
    {
        try
        {
            return await _context.Organizations
                .Select(o => new OrganizationDto { Id = o.Id, Name = o.Name })
                .ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving organizations");
            throw;
        }
    }
}
