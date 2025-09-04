namespace TicketsApi.Models
{
    public enum TicketStatus
    {
        Abierto = 0,
        Cerrado = 1
    }

    public class Ticket
    {
        public int Id { get; set; }
        public string User { get; set; } = default!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public TicketStatus Status { get; set; } = TicketStatus.Abierto;
    }
}
