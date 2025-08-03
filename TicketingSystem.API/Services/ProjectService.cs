using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

public class ProjectService : IProjectService
{
    private readonly AppDbContext _context;
    private readonly ILogger<ProjectService> _logger;

    public ProjectService(AppDbContext context, ILogger<ProjectService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<ProjectDto>> GetAllAsync()
    {
        try
        {
            return await _context.Projects
                .Select(p => new ProjectDto { Id = p.Id, Title = p.Name })
                .ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving projects");
            throw;
        }
    }

    public async Task<ProjectDto> GetByIdAsync(int id)
    {
        try
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null) return null;

            return new ProjectDto { Id = project.Id, Title = project.Name };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving project by id");
            throw;
        }
    }

    public async Task<ProjectDto> CreateAsync(ProjectCreateDto dto)
    {
        try
        {
            var project = new Project { Name = dto.Title };
            _context.Projects.Add(project);
            await _context.SaveChangesAsync();

            return new ProjectDto { Id = project.Id, Title = project.Name };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating project");
            throw;
        }
    }
}
