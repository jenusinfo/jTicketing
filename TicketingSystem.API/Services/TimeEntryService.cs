public class TimeEntryService : ITimeEntryService
{
    public Task<IEnumerable<TimeEntryDto>> GetAllAsync()
    {
        return Task.FromResult<IEnumerable<TimeEntryDto>>(new List<TimeEntryDto>());
    }
}