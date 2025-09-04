using Microsoft.EntityFrameworkCore;
using TicketsApi.Dto;
using TicketsApi.Models;
using TicketsApi.Repositories;

namespace TicketsApi.Services
{
    public class TicketsService
    {
        private readonly TicketsRepository repository;

        public TicketsService(TicketsRepository repository)
        {
            this.repository = repository;
        }

        public async Task<PagedResult<TicketDto>> GetAllAsync(string? usuario, string? estatus, int page, int pageSize)
        {
            var query = repository.Query().AsNoTracking();

            if (!string.IsNullOrWhiteSpace(usuario))
                query = query.Where(t => t.User.ToLower().Contains(usuario.ToLower()));

            if (!string.IsNullOrWhiteSpace(estatus) && Enum.TryParse<TicketStatus>(estatus, true, out var est))
                query = query.Where(t => t.Status == est);

            var total = await query.CountAsync();

            var items = await query
                .OrderByDescending(t => t.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(t => new TicketDto(
                    t.Id,
                    t.User,
                    t.CreatedAt,
                    t.UpdatedAt,
                    t.Status.ToString()))
                .ToListAsync();

            return new PagedResult<TicketDto>
            {
                Items = items,
                Page = page,
                PageSize = pageSize,
                TotalItems = total
            };
        }

        public async Task<TicketDto?> GetByIdAsync(int id)
        {
            var t = await repository.GetByIdAsync(id);
            return t is null ? null : new TicketDto(t.Id, t.User, t.CreatedAt, t.UpdatedAt, t.Status.ToString());
        }

        public async Task<TicketDto> CreateAsync(CreateTicketDto dto)
        {
            var newTicket = new Ticket
            {
                User = dto.User,
                Status = Enum.TryParse<TicketStatus>(dto.Status, true, out var status) ? status : TicketStatus.Abierto,
                CreatedAt = DateTime.UtcNow
            };

            await repository.AddAsync(newTicket);

            return new TicketDto(newTicket.Id, newTicket.User, newTicket.CreatedAt, newTicket.UpdatedAt, newTicket.Status.ToString());
        }

        public async Task<TicketDto?> UpdateAsync(int id, UpdateTicketDto dto)
        {
            var t = await repository.GetByIdAsync(id);
            if (t is null) return null;

            t.User = dto.User;

            if (!Enum.TryParse<TicketStatus>(dto.Status, true, out var status))
                throw new ArgumentException("Status inválido. Use 'Abierto' o 'Cerrado'.");

            t.Status = status;
            t.UpdatedAt = DateTime.UtcNow;

            await repository.UpdateAsync(t);

            return new TicketDto(t.Id, t.User, t.CreatedAt, t.UpdatedAt, t.Status.ToString());
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var t = await repository.GetByIdAsync(id);
            if (t is null) return false;

            await repository.DeleteAsync(t);
            return true;
        }
    }
}
