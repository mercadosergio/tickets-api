using Microsoft.AspNetCore.Mvc;
using TicketsApi.Dto;
using TicketsApi.Services;

namespace TicketsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketsController : ControllerBase
    {
        private readonly TicketsService service;

        public TicketsController(TicketsService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResult<TicketDto>>> GetAll(
              [FromQuery] string? user,
              [FromQuery] string? status,
              [FromQuery] int page = 1,
              [FromQuery] int pageSize = 20)
        {
            try
            {
                var result = await service.GetAllAsync(user, status, page, pageSize);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Ocurrió un error al obtener los tickets.", details = ex.Message });
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<TicketDto>> GetById(int id)
        {
            try
            {
                var result = await service.GetByIdAsync(id);
                return result is null ? NotFound() : Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Ocurrió un error al obtener el ticket.", details = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult<TicketDto>> Create([FromBody] CreateTicketDto dto)
        {
            try
            {
                var result = await service.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Ocurrió un error al crear el ticket.", details = ex.Message });
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<TicketDto>> Update(int id, [FromBody] UpdateTicketDto dto)
        {
            try
            {
                var result = await service.UpdateAsync(id, dto);
                return result is null ? NotFound() : Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Ocurrió un error al actualizar el ticket.", details = ex.Message });
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var success = await service.DeleteAsync(id);
                return success ? NoContent() : NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Ocurrió un error al eliminar el ticket.", details = ex.Message });
            }
        }
    }
}
