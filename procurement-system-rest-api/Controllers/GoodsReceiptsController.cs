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
    public class GoodsReceiptsController : ControllerBase
    {
        private readonly ProcurementDbContext _context;

        public GoodsReceiptsController(ProcurementDbContext context)
        {
            _context = context;
        }

        // GET: api/GoodsReceipts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GoodsReceipt>>> GetGoodsReceipt()
        {
            return await _context.GoodsReceipt
                    .Include(e => e.PurchaseOrder)
                    .Include(e => e.Supplier)
                    .Include(e => e.Site)
                    .Include(e => e.Delivery)
                    .ToListAsync();
        }

        // GET: api/GoodsReceipts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GoodsReceipt>> GetGoodsReceipt(int id)
        {
            var goodsReceipt = await _context.GoodsReceipt.FindAsync(id);

            if (goodsReceipt == null)
            {
                return NotFound();
            }

            return goodsReceipt;
        }

        // PUT: api/GoodsReceipts/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGoodsReceipt(int id, GoodsReceipt goodsReceipt)
        {
            if (id != goodsReceipt.ReceiptId)
            {
                return BadRequest();
            }

            _context.Entry(goodsReceipt).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GoodsReceiptExists(id))
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

        // POST: api/GoodsReceipts
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<GoodsReceipt>> PostGoodsReceipt(GoodsReceiptDTO goodsReceiptDTO)
        {
            PurchaseOrder purchaseOrder = _context.PurchaseOrders.FirstOrDefault(e => e.OrderReference == goodsReceiptDTO.PurchaseOrder);
            Supplier supplier = _context.Supplier.FirstOrDefault(e => e.SupplierCode == goodsReceiptDTO.SupplierCode);
            Site site = _context.Sites.FirstOrDefault(e => e.SiteCode == goodsReceiptDTO.SiteCode);
            Delivery delivery = _context.Deliveries.FirstOrDefault(e => e.DeliveryId == goodsReceiptDTO.DeliveryId);

            GoodsReceipt goodsReceipt = new GoodsReceipt
            {
                DateDelivered = goodsReceiptDTO.DateDelivered,
                PurchaseOrder = purchaseOrder,
                Supplier = supplier,
                Site = site,
                Delivery = delivery
            };

            _context.GoodsReceipt.Add(goodsReceipt);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGoodsReceipt", new { id = goodsReceipt.ReceiptId }, goodsReceipt);
        }

        // DELETE: api/GoodsReceipts/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<GoodsReceipt>> DeleteGoodsReceipt(int id)
        {
            var goodsReceipt = await _context.GoodsReceipt.FindAsync(id);
            if (goodsReceipt == null)
            {
                return NotFound();
            }

            _context.GoodsReceipt.Remove(goodsReceipt);
            await _context.SaveChangesAsync();

            return goodsReceipt;
        }

        private bool GoodsReceiptExists(int id)
        {
            return _context.GoodsReceipt.Any(e => e.ReceiptId == id);
        }
    }
}
