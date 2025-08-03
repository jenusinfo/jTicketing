using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<TicketComment> TicketComments { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Organization> Organizations { get; set; }
    public DbSet<UserSession> UserSessions { get; set; }
    public DbSet<TicketAttachment> TicketAttachments { get; set; }
    public DbSet<TicketHistory> TicketHistories { get; set; }
    public DbSet<TimeEntry> TimeEntries { get; set; }
    public DbSet<SlaPolicy> SlaPolicies { get; set; }
    public DbSet<NotificationRule> NotificationRules { get; set; }
    public DbSet<NotificationQueue> NotificationQueues { get; set; }
    public DbSet<UserSkill> UserSkills { get; set; }
    public DbSet<TeamAssignment> TeamAssignments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Ticket>().HasIndex(t => t.CreatedById);
        modelBuilder.Entity<Ticket>().HasIndex(t => t.AssignedToUserId);
        modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();

        modelBuilder.Entity<Ticket>()
            .HasOne(t => t.CreatedBy)
            .WithMany(u => u.CreatedTickets)
            .HasForeignKey(t => t.CreatedById)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Ticket>()
            .HasOne(t => t.AssignedToUser)
            .WithMany(u => u.AssignedTickets)
            .HasForeignKey(t => t.AssignedToUserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<TicketComment>()
            .HasOne(c => c.User)
            .WithMany(u => u.Comments)
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<TicketComment>()
            .HasOne(c => c.Ticket)
            .WithMany(t => t.Comments)
            .HasForeignKey(c => c.TicketId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Project>()
            .HasOne(p => p.Client)
            .WithMany(c => c.Projects)
            .HasForeignKey(p => p.ClientId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Ticket>()
            .HasOne(t => t.Project)
            .WithMany(p => p.Tickets)
            .HasForeignKey(t => t.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Ticket>()
            .HasOne(t => t.Client)
            .WithMany(c => c.Tickets)
            .HasForeignKey(t => t.ClientId)
            .OnDelete(DeleteBehavior.Restrict);

        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            var created = entity.FindProperty("CreatedAt");
            if (created != null)
            {
                modelBuilder.Entity(entity.ClrType)
                    .Property(created.ClrType, "CreatedAt")
                    .HasDefaultValueSql("GETDATE()");
            }
            var updated = entity.FindProperty("UpdatedAt");
            if (updated != null)
            {
                modelBuilder.Entity(entity.ClrType)
                    .Property(updated.ClrType, "UpdatedAt")
                    .HasDefaultValueSql("GETDATE()");
            }
        }
    }
}
