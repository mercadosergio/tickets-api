
using Microsoft.EntityFrameworkCore;
using TicketsApi.Models.DB;

namespace TicketsApi.Repositories
{
    public class TicketsRepository
    {
        private readonly TicketsApiContext dbContext;

        public TicketsRepository(TicketsApiContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IQueryable<TicketsApi.Models.Ticket> Query() => dbContext.Tickets.AsQueryable();

        public async Task<TicketsApi.Models.Ticket?> GetByIdAsync(int id) =>
            await dbContext.Tickets.FirstOrDefaultAsync(t => t.Id == id);

        public async Task AddAsync(TicketsApi.Models.Ticket ticket)
        {
            dbContext.Tickets.Add(ticket);
            await dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(TicketsApi.Models.Ticket ticket)
        {
            dbContext.Tickets.Update(ticket);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(TicketsApi.Models.Ticket ticket)
        {
            dbContext.Tickets.Remove(ticket);
            await dbContext.SaveChangesAsync();
        }
    }
}
