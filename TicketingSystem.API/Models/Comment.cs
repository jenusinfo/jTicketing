namespace TicketingSystem.API.Models
{
    public class Comment
    {
        public int Id { get; set; }

        public string Message { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int TicketId { get; set; }

        public Ticket Ticket { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }
    }
}
