﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ejemplo1.Models;

namespace Ejemplo1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoicesController : ControllerBase
    {
        private readonly InvoiceContext _context;

        public InvoicesController(InvoiceContext context)
        {
            _context = context;
        }

        // GET: api/Invoices
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Invoice>>> GetInvoices()
        {
          if (_context.Invoices == null)
          {
              return NotFound();
          }
            return await _context.Invoices.ToListAsync();
        }

        // GET: api/Invoices/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Invoice>> GetInvoice(int id)
        {
          if (_context.Invoices == null)
          {
              return NotFound();
          }
            var invoice = await _context.Invoices.FindAsync(id);

            if (invoice == null)
            {
                return NotFound();
            }

            return invoice;
        }

        // PUT: api/Invoices/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInvoice(int id, Invoice invoice)
        {
            if (id != invoice.InvoiceId)
            {
                return BadRequest();
            }

            _context.Entry(invoice).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InvoiceExists(id))
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

        // POST: api/Invoices
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Invoice>> PostInvoice(Invoice invoice)
        {
          if (_context.Invoices == null)
          {
              return Problem("Entity set 'InvoiceContext.Invoices'  is null.");
          }
            _context.Invoices.Add(invoice);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInvoice", new { id = invoice.InvoiceId }, invoice);
        }

        // DELETE: api/Invoices/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvoice(int id)
        {
            if (_context.Invoices == null)
            {
                return NotFound();
            }
            var invoice = await _context.Invoices.FindAsync(id);
            if (invoice == null)
            {
                return NotFound();
            }

            _context.Invoices.Remove(invoice);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("GetByName/{name}")]
        public List<Invoice> GetInvoiceByName(string name)
        {
            var invoice = _context.Invoices
                .Include(i => i.Customer)
                .Where(i => i.Customer.FirstName.Contains(name))
                .OrderByDescending(i => i.Customer.FirstName)
                .OrderByDescending(i => i.InvoiceNumber)
                .ToList();
            return invoice;
        }

        [HttpGet("GetByInvoiceNumber/{invoiceNumber}")]
        public List<Details> GetByInvoiceNumber(string invoiceNumber)
        {
            var details = _context.Details
                .Include(i => i.Invoice)
                .Include(i => i.Invoice.Customer)
                .Where(i => i.Invoice.InvoiceNumber.Contains(invoiceNumber))
                .OrderByDescending(i => i.Invoice.Customer.FirstName)
                .OrderByDescending(i => i.Invoice.InvoiceNumber)
                .ToList();
            return details;

        }

        [HttpGet("GetByInvoiceDate/{dateInvoice}")]
        public List<Details> GetByInvoiceDate(DateTime dateInvoice)
        {
            var details = _context.Details
                .Include(i => i.Invoice)
                .Include(i => i.Product)
                .Where(i => i.Invoice.Date.Date == dateInvoice.Date)
                .OrderBy(i => i.Invoice.Date)
                .OrderBy(i => i.Product)
                .ToList();
            return details;
        }

        private bool InvoiceExists(int id)
        {
            return (_context.Invoices?.Any(e => e.InvoiceId == id)).GetValueOrDefault();
        }
    }
}
