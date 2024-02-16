using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FurnitureStore.db;

namespace FurnitureStore.Controllers
{
    [Route("api/m2mProductSupply")]
    [ApiController]
    public class M2mProductSupplyController : ControllerBase
    {
        private readonly FurnitureStoreContext _context;

        public M2mProductSupplyController(FurnitureStoreContext context)
        {
            _context = context;
        }

        // GET: api/M2mProductSupply
        [HttpGet]
        public async Task<ActionResult<IEnumerable<M2mProductSupply>>> GetM2mProductSupplies()
        {
            return await _context.M2mProductSupplies.ToListAsync();
        }

        // GET: api/M2mProductSupply/5
        [HttpGet("{id}")]
        public async Task<ActionResult<M2mProductSupply>> GetM2mProductSupply(int id)
        {
            var m2mProductSupply = await _context.M2mProductSupplies.FindAsync(id);

            if (m2mProductSupply == null)
            {
                return NotFound();
            }

            return m2mProductSupply;
        }

        // PUT: api/M2mProductSupply/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutM2mProductSupply(int id, M2mProductSupply m2mProductSupply)
        {
            if (id != m2mProductSupply.IdProduct)
            {
                return BadRequest();
            }

            _context.Entry(m2mProductSupply).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!M2mProductSupplyExists(id))
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

        // POST: api/M2mProductSupply
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<M2mProductSupply>> PostM2mProductSupply(M2mProductSupply m2mProductSupply)
        {
            _context.M2mProductSupplies.Add(m2mProductSupply);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (M2mProductSupplyExists(m2mProductSupply.IdProduct))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetM2mProductSupply", new { id = m2mProductSupply.IdProduct }, m2mProductSupply);
        }

        // DELETE: api/M2mProductSupply/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteM2mProductSupply(int id)
        {
            var m2mProductSupply = await _context.M2mProductSupplies.FindAsync(id);
            if (m2mProductSupply == null)
            {
                return NotFound();
            }

            _context.M2mProductSupplies.Remove(m2mProductSupply);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool M2mProductSupplyExists(int id)
        {
            return _context.M2mProductSupplies.Any(e => e.IdProduct == id);
        }
    }
}
