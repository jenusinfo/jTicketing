public class OrganizationService : IOrganizationService
{
    public Task<IEnumerable<OrganizationDto>> GetAllAsync()
    {
        return Task.FromResult<IEnumerable<OrganizationDto>>(new List<OrganizationDto>());
    }
}