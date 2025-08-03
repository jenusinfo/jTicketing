using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Xunit;
using System.Linq;
using System;
using System.Threading.Tasks;

public class TicketCommentServiceTests
{
    private static TicketCommentService CreateService(out AppDbContext context, out ClaimsPrincipal user)
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        context = new AppDbContext(options);

        var client = new Client { Id = 1, Name = "Client" };
        var project = new Project { Id = 1, Name = "Proj", Description = "Desc", ClientId = client.Id };
        var usr = new User { Id = 1, Email = "user@example.com", PasswordHash = "h", FullName = "Test User", Role = "User" };
        var ticket = new Ticket
        {
            Id = 1,
            TicketNumber = "T1",
            Title = "Test",
            Description = "Desc",
            Category = "Cat",
            Subcategory = "Sub",
            Priority = "P1",
            Impact = "High",
            Urgency = "Low",
            SLA = "SLA",
            ClientId = client.Id,
            ProjectId = project.Id,
            CreatedById = usr.Id
        };
        context.Clients.Add(client);
        context.Projects.Add(project);
        context.Users.Add(usr);
        context.Tickets.Add(ticket);
        context.SaveChanges();

        user = new ClaimsPrincipal(new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.NameIdentifier, usr.Id.ToString())
        }, "Test"));

        var logger = new LoggerFactory().CreateLogger<TicketCommentService>();
        return new TicketCommentService(context, logger);
    }

    [Fact]
    public async Task AddCommentAsync_PersistsComment()
    {
        var service = CreateService(out var context, out var user);
        var dto = new TicketCommentCreateDto { Message = "Hello" };

        var result = await service.AddCommentAsync(1, dto, user);

        Assert.Equal("Hello", result.Message);
        Assert.Single(context.TicketComments);
    }

    [Fact]
    public async Task GetByTicketIdAsync_ReturnsComments()
    {
        var service = CreateService(out var context, out var user);
        context.TicketComments.Add(new TicketComment { TicketId = 1, UserId = 1, Message = "Existing" });
        await context.SaveChangesAsync();

        var comments = await service.GetByTicketIdAsync(1);

        Assert.Single(comments);
        Assert.Equal("Existing", comments.First().Message);
    }
}
