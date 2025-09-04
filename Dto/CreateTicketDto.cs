using System.ComponentModel.DataAnnotations;

namespace TicketsApi.Dto
{
    public class CreateTicketDto
    {
        [Required]
        public string User { get; set; } = default!;

        public string? Status { get; set; } 
    }
}
