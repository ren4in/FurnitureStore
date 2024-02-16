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
    [Route("api/m2mInventoryProduct")]
    [ApiController]
    public class M2mInventoryProductController : ControllerBase
    {
        private readonly FurnitureStoreContext _context;

        public M2mInventoryProductController(FurnitureStoreContext context)
        {
            _context = context;
        }

       
        // GET: a
        // pi/M2mInventoryProduct
        [HttpGet]
        public async Task<ActionResult<IEnumerable<M2mInventoryProduct>>> GetM2mInventoryProducts()
        {
            return await _context.M2mInventoryProducts.ToListAsync();
        }

        // GET: api/M2mInventoryProduct/5
        [HttpGet("{id}")]
        public async Task<ActionResult<M2mInventoryProduct>> GetM2mInventoryProduct(int id)
        {
            var m2mInventoryProduct = await _context.M2mInventoryProducts.FindAsync(id);

            if (m2mInventoryProduct == null)
            {
                return NotFound();
            }

            return m2mInventoryProduct;
        }

        // PUT: api/M2mInventoryProduct/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutM2mInventoryProduct(int id, M2mInventoryProduct m2mInventoryProduct)
        {
            if (id != m2mInventoryProduct.IdInventory)
            {
                return BadRequest();
            }

            _context.Entry(m2mInventoryProduct).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!M2mInventoryProductExists(id))
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

        // POST: api/M2mInventoryProduct
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<M2mInventoryProduct>> PostM2mInventoryProduct(M2mInventoryProduct m2mInventoryProduct)
        {
            _context.M2mInventoryProducts.Add(m2mInventoryProduct);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (M2mInventoryProductExists(m2mInventoryProduct.IdInventory))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetM2mInventoryProduct", new { id = m2mInventoryProduct.IdInventory }, m2mInventoryProduct);
        }

        // DELETE: api/M2mInventoryProduct/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteM2mInventoryProduct(int id)
        {
            var m2mInventoryProduct = await _context.M2mInventoryProducts.FindAsync(id);
            if (m2mInventoryProduct == null)
            {
                return NotFound();
            }

            _context.M2mInventoryProducts.Remove(m2mInventoryProduct);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool M2mInventoryProductExists(int id)
        {
            return _context.M2mInventoryProducts.Any(e => e.IdInventory == id);
        }
    }
}
