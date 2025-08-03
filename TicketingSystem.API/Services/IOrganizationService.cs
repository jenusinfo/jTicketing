public interface IOrganizationService
{
    Task<IEnumerable<OrganizationDto>> GetAllAsync();
}