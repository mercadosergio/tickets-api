using System.ComponentModel.DataAnnotations;

namespace TicketsApi.Dto
{
    public class UpdateTicketDto
    {
        [Required]
        public string User { get; set; } = default!;

        [Required]
        public string Status { get; set; }
    }
}
