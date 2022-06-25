using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiSDM.Models;

namespace ApiSDM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RepartidorsController : ControllerBase
    {
        private readonly u204501959_SaborDeMexicoContext _context;

        public RepartidorsController(u204501959_SaborDeMexicoContext context)
        {
            _context = context;
        }

        // GET: api/Repartidors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Repartidor>>> GetRepartidor()
        {
            return await _context.Repartidor.Where(d=>d.Activo==1).ToListAsync();
        }

        // GET: api/Repartidors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Repartidor>> GetRepartidor(int id)
        {
            var repartidor = await _context.Repartidor.FindAsync(id);

            if (repartidor == null)
            {
                return NotFound();
            }

            return repartidor;
        }

        // PUT: api/Repartidors/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRepartidor(int id, Repartidor repartidor)
        {
            if (id != repartidor.Id)
            {
                return BadRequest();
            }

            _context.Entry(repartidor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RepartidorExists(id))
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

        // POST: api/Repartidors
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Repartidor>> PostRepartidor(Repartidor repartidor)
        {
            _context.Repartidor.Add(repartidor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRepartidor", new { id = repartidor.Id }, repartidor);
        }

        // DELETE: api/Repartidors/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Repartidor>> DeleteRepartidor(int id)
        {
            var repartidor = await _context.Repartidor.FindAsync(id);
            if (repartidor == null)
            {
                return NotFound();
            }

            _context.Repartidor.Remove(repartidor);
            await _context.SaveChangesAsync();

            return repartidor;
        }

        private bool RepartidorExists(int id)
        {
            return _context.Repartidor.Any(e => e.Id == id);
        }
    }
}
