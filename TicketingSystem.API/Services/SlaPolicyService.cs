public class SlaPolicyService : ISlaPolicyService
{
    public Task<IEnumerable<SlaPolicyDto>> GetAllAsync()
    {
        return Task.FromResult<IEnumerable<SlaPolicyDto>>(new List<SlaPolicyDto>());
    }
}