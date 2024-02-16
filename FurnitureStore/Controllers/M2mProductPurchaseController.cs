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
    [Route("api/m2mProductPurchase")]
    [ApiController]
    public class M2mProductPurchaseController : ControllerBase
    {
        private readonly FurnitureStoreContext _context;

        public M2mProductPurchaseController(FurnitureStoreContext context)
        {
            _context = context;
        }

        // GET: api/M2mProductPurchase
        [HttpGet]
        public async Task<ActionResult<IEnumerable<M2mProductPurchase>>> GetM2mProductPurchases()
        {
            return await _context.M2mProductPurchases.ToListAsync();
        }

        // GET: api/M2mProductPurchase/5
        [HttpGet("{id}")]
        public async Task<ActionResult<M2mProductPurchase>> GetM2mProductPurchase(int id)
        {
            var m2mProductPurchase = await _context.M2mProductPurchases.FindAsync(id);

            if (m2mProductPurchase == null)
            {
                return NotFound();
            }

            return m2mProductPurchase;
        }

        // PUT: api/M2mProductPurchase/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutM2mProductPurchase(int id, M2mProductPurchase m2mProductPurchase)
        {
            if (id != m2mProductPurchase.IdProduct)
            {
                return BadRequest();
            }

            _context.Entry(m2mProductPurchase).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!M2mProductPurchaseExists(id))
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

        // POST: api/M2mProductPurchase
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<M2mProductPurchase>> PostM2mProductPurchase(M2mProductPurchase m2mProductPurchase)
        {
            _context.M2mProductPurchases.Add(m2mProductPurchase);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (M2mProductPurchaseExists(m2mProductPurchase.IdProduct))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetM2mProductPurchase", new { id = m2mProductPurchase.IdProduct }, m2mProductPurchase);
        }

        // DELETE: api/M2mProductPurchase/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteM2mProductPurchase(int id)
        {
            var m2mProductPurchase = await _context.M2mProductPurchases.FindAsync(id);
            if (m2mProductPurchase == null)
            {
                return NotFound();
            }

            _context.M2mProductPurchases.Remove(m2mProductPurchase);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool M2mProductPurchaseExists(int id)
        {
            return _context.M2mProductPurchases.Any(e => e.IdProduct == id);
        }
    }
}
