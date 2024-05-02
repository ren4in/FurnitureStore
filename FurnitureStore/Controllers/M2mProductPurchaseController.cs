using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FurnitureStore.db;
using Microsoft.AspNetCore.Authorization;

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
        public async Task<ActionResult<IEnumerable<M2mProductPurchase>>> GetM2mProductPurchase()
        {
            return await _context.M2mProductPurchases.Where(p=> p.Amount>0).ToListAsync();
        }

        // GET: api/M2mProductPurchase/5
        [HttpGet("id")]
        [Authorize(Roles = "Продавец")]

        public async Task<ActionResult<IEnumerable<M2mProductPurchase>>> GetM2mProductPurchases(int id)
        {
            var m2mProductPurchase = await _context.M2mProductPurchases.Where(p => p.IdPurchase == id).Include(p=>p.IdProductNavigation).ToListAsync();

            if (m2mProductPurchase == null)
            {
                return NotFound();
            }
            return m2mProductPurchase;

        }

        // PUT: api/M2mProductPurchase/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut()]
        [Authorize(Roles = "Продавец")]

        public async Task<IActionResult> PutM2mProductPurchase(int idProduct, int idPurchase, int amount)
        {


            var m2mProductPurchase = await _context.M2mProductPurchases.FirstOrDefaultAsync(p => p.IdProduct == idProduct && p.IdPurchase == idPurchase);
            m2mProductPurchase.Amount = amount;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!M2mProductPurchaseExists(idProduct))
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
        [Authorize(Roles = "Продавец")]

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
        [HttpDelete]
        [Authorize(Roles = "Продавец")]

        public async Task<IActionResult> DeleteM2mProductPurchase(int idProduct, int idPurchase)
        {
            var m2mProductPurchase = await _context.M2mProductPurchases.FirstOrDefaultAsync(p=>p.IdProduct==idProduct && p.IdPurchase==idPurchase);
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
