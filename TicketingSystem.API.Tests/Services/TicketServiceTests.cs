using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using TicketingSystem.API.Data;
using TicketingSystem.API.Models;
using TicketingSystem.API.Services;
using Xunit;

namespace TicketingSystem.API.Tests.Services;

public class TicketServiceTests
{
    [Fact]
    public async Task AssignTicketAsync_ThrowsForMismatchedOrg()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        using var context = new AppDbContext(options);

        var org1 = new Organization { Id = 1, Name = "Org1" };
        var org2 = new Organization { Id = 2, Name = "Org2" };
        context.Organizations.AddRange(org1, org2);

        var creator = new User { Id = 10, Email = "creator@org1", OrganizationId = org1.Id };
        var otherUser = new User { Id = 20, Email = "user@org2", OrganizationId = org2.Id };
        context.Users.AddRange(creator, otherUser);

        var ticket = new Ticket { Id = 100, Title = "Test", OrganizationId = org1.Id, CreatedById = creator.Id };
        context.Tickets.Add(ticket);

        await context.SaveChangesAsync();

        var service = new TicketService(context);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, otherUser.Id.ToString()),
            new Claim("OrgId", otherUser.OrganizationId.ToString())
        };
        var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "Test"));

        await Assert.ThrowsAsync<UnauthorizedAccessException>(
            () => service.AssignTicketAsync(ticket.Id, user));
    }
}
