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
    [Route("api/Products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly FurnitureStoreContext _context;

        public ProductsController(FurnitureStoreContext context)
        {
            _context = context;
        }

        // GET: api/Products
      [HttpGet("api/GetProducts")]
        [Authorize(Roles = "Продавец, Администратор")]

        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            //     return await _context.Products.Where(p => p.IdProduct==1).ToListAsync();
                  return await _context.Products.Include(p=>p.IdProductCategoryNavigation).ToListAsync();

        }
        [HttpGet("api/GetProductsNoPhoto")]
        [Authorize(Roles = "Продавец, Администратор")]

        public async Task<ActionResult<IEnumerable<Product>>> GetProductsNoPhoto()
        {
            //     return await _context.Products.Where(p => p.IdProduct==1).ToListAsync();
            return   await _context.Products
        .Include(p => p.IdProductCategoryNavigation)
        .Where(p=>p.Amount>0).Select(p => new Product
        {
            IdProduct = p.IdProduct,
            ProductName = p.ProductName,
            Amount = p.Amount,
            // Остальные столбцы будут проигнорированы
        })
        .ToListAsync();

        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {product.IdProduct = id;
            if (id != product.IdProduct)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
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

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = product.IdProduct }, product);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.IdProduct == id);
        }
    }
}
