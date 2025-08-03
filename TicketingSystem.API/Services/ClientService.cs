public class ClientService : IClientService
{
    public Task<IEnumerable<ClientDto>> GetAllAsync() => Task.FromResult<IEnumerable<ClientDto>>(new List<ClientDto>());
    public Task<ClientDto> GetByIdAsync(int id) => Task.FromResult<ClientDto?>(null);
    public Task<ClientDto> CreateAsync(ClientCreateDto dto) => Task.FromResult(new ClientDto { Id = 1, Name = dto.Name });
    public Task<ClientDto> UpdateAsync(int id, ClientUpdateDto dto) => Task.FromResult(new ClientDto { Id = id, Name = dto.Name });
    public Task<bool> DeleteAsync(int id) => Task.FromResult(true);
}