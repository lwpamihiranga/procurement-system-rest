using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using procurement_system_rest_api;
using procurement_system_rest_api.DTOs;
using procurement_system_rest_api.Models;

namespace procurement_system_rest_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoicesController : ControllerBase
    {
        private readonly ProcurementDbContext _context;

        public InvoicesController(ProcurementDbContext context)
        {
            _context = context;
        }

        // GET: api/Invoices
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Invoice>>> GetInvoice()
        {
            return await _context.Invoice
                        .Include(e => e.GoodsReceipt)
                        .Include(e => e.Supplier)
                        .Include(e => e.HandledAccountant)
                        .ToListAsync();
        }

        // GET: api/Invoices/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Invoice>> GetInvoice(string id)
        {
            var invoice = await _context.Invoice.FindAsync(id);

            if (invoice == null)
            {
                return NotFound();
            }

            return invoice;
        }

        // PUT: api/Invoices/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInvoice(string id, Invoice invoice)
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
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Invoice>> PostInvoice(InvoiceDTO invoiceDTO)
        {
            GoodsReceipt goodsReceipt = _context.GoodsReceipt.FirstOrDefault(e => e.ReceiptId == invoiceDTO.GoodsReceipt);
            Supplier supplier = _context.Supplier.FirstOrDefault(e => e.SupplierCode == invoiceDTO.Supplier);
            AccountingStaff accountingStaff = _context.AccountingStaff.FirstOrDefault(e => e.StaffId == invoiceDTO.AccountingStaffId);

            Invoice invoice = new Invoice
            {
                InvoiceId = invoiceDTO.InvoiceId,
                GoodsReceipt = goodsReceipt,
                Supplier = supplier,
                HandledAccountant = accountingStaff,
                InvoiceDate = DateTime.UtcNow,
                InvoiceStatus = invoiceDTO.InvoiceStatus,
                Description = invoiceDTO.Description,
                NetAmount = invoiceDTO.NetAmount,
                GrossAmount = invoiceDTO.GrossAmount,
                TaxAmount = invoiceDTO.TaxAmount,
                CreditNote = invoiceDTO.CreditNote,
                AdvancePayment = invoiceDTO.AdvancePayment
            };

            _context.Invoice.Add(invoice);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (InvoiceExists(invoice.InvoiceId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetInvoice", new { id = invoice.InvoiceId }, invoice);
        }

        // DELETE: api/Invoices/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Invoice>> DeleteInvoice(string id)
        {
            var invoice = await _context.Invoice.FindAsync(id);
            if (invoice == null)
            {
                return NotFound();
            }

            _context.Invoice.Remove(invoice);
            await _context.SaveChangesAsync();

            return invoice;
        }

        private bool InvoiceExists(string id)
        {
            return _context.Invoice.Any(e => e.InvoiceId == id);
        }
    }
}
