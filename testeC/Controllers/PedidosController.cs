using Microsoft.AspNetCore.Mvc;
using TestC.Models;
using TestC.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace TestC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PedidosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/pedidos
       [HttpGet]
public async Task<ActionResult<IEnumerable<Pedido>>> Get()
{
    return await _context.pedidos.ToListAsync();
}

        // POST: api/Pedidos
        [HttpPost]
        public async Task<ActionResult<Pedido>> Post([FromBody] Pedido pedido)
        {
            _context.pedidos.Add(pedido); // Alteração aqui
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = pedido.Id }, pedido);
        }

        // PUT: api/Pedidos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Pedido pedidoAtualizado)
        {
            if (id != pedidoAtualizado.Id)
            {
                return BadRequest();
            }

            _context.Entry(pedidoAtualizado).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.pedidos.Any(e => e.Id == id)) // Alteração aqui
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Pedidos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var pedido = await _context.pedidos.FindAsync(id); // Alteração aqui
            if (pedido == null)
            {
                return NotFound();
            }

            _context.pedidos.Remove(pedido); // Alteração aqui
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
