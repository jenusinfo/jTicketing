public class ProjectService : IProjectService
{
    public Task<IEnumerable<ProjectDto>> GetAllAsync() => Task.FromResult<IEnumerable<ProjectDto>>(new List<ProjectDto>());
    public Task<ProjectDto> GetByIdAsync(int id) => Task.FromResult<ProjectDto?>(null);
    public Task<ProjectDto> CreateAsync(ProjectCreateDto dto) => Task.FromResult(new ProjectDto { Id = 1, Title = dto.Title });
}