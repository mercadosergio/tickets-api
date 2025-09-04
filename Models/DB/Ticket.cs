using System;
using System.Collections.Generic;

namespace TicketsApi.Models.DB;

public partial class Ticket
{
    public int Id { get; set; }

    public string? User { get; set; }

    public string? Status { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
