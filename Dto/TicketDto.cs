namespace TicketsApi.Dto
{
    public record TicketDto(
       int Id,
       string User,
       DateTime? CreatedAt,
       DateTime? UpdatedAt,
       string Status
   );
}
