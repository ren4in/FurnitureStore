﻿using System;
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
    [Route("api/Purchases")]
    [ApiController]
    public class PurchasesController : ControllerBase
    {
        private readonly FurnitureStoreContext _context;

        public PurchasesController(FurnitureStoreContext context)
        {
            _context = context;
        }

        // GET: api/Purchases
        [HttpGet]

        public async Task<ActionResult<IEnumerable<Purchase>>> GetPurchases()
        {
            return await _context.Purchases.ToListAsync();
        }

        [HttpGet("id_customer")]

        [Authorize(Roles = "Продавец")]

        public async Task<ActionResult<IEnumerable<Purchase>>> GetPurchase_Customer(int id_customer)
        {
            return await _context.Purchases.Include(c => c.IdCustomerNavigation).Where(p => p.IdCustomerNavigation.IdCustomer==id_customer).ToListAsync();
        }

       [HttpGet("{id_customer}/{id_Purchase}")]

        [Authorize(Roles = "Продавец")]

        public async Task<ActionResult<IEnumerable<Purchase>>> GetPurchase_Customer(int id_customer, int id_Purchase)
        {
            return await _context.Purchases.Include(c => c.IdCustomerNavigation).Where(p => p.IdCustomerNavigation.IdCustomer == id_customer && p.IdPurchase==id_Purchase).ToListAsync();

        }

            // GET: api/Purchases/5
            [HttpGet("{id}")]

        [Authorize(Roles = "Продавец")]
        public async Task<ActionResult<Purchase>> GetPurchase(int id)
        {
            var purchase = await _context.Purchases.FindAsync(id);

            if (purchase == null)
            {
                return NotFound();
            }

            return purchase;
        }

        // PUT: api/Purchases/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]

        [Authorize(Roles = "Продавец")]
        public async Task<IActionResult> PutPurchase(int id, Purchase purchase)
        {purchase.IdPurchase = id;
            if (id != purchase.IdPurchase)
            {
                return BadRequest();
            }

            _context.Entry(purchase).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PurchaseExists(id))
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

        // POST: api/Purchases
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Purchase>> PostPurchase(Purchase purchase)
        {
            _context.Purchases.Add(purchase);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPurchase", new { id = purchase.IdPurchase }, purchase);
        }

        // DELETE: api/Purchases/5
        [HttpDelete("{id}")]

        [Authorize(Roles = "Продавец")]
        public async Task<IActionResult> DeletePurchase(int id)
        {
            var purchase = await _context.Purchases.FindAsync(id);
            if (purchase == null)
            {
                return NotFound();
            }

            _context.Purchases.Remove(purchase);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PurchaseExists(int id)
        {
            return _context.Purchases.Any(e => e.IdPurchase == id);
        }
    }
}
