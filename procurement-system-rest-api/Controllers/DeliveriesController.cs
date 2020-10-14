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
    public class DeliveriesController : ControllerBase
    {
        private readonly ProcurementDbContext _context;

        public DeliveriesController(ProcurementDbContext context)
        {
            _context = context;
        }

        // GET: api/Deliveries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Delivery>>> GetDeliveries()
        {
            return await _context.Deliveries.ToListAsync();
        }

        // GET: api/Deliveries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Delivery>> GetDelivery(string id)
        {
            var delivery = await _context.Deliveries.FindAsync(id);

            if (delivery == null)
            {
                return NotFound();
            }

            return delivery;
        }

        // PUT: api/Deliveries/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDelivery(string id, Delivery delivery)
        {
            if (id != delivery.DeliveryId)
            {
                return BadRequest();
            }

            _context.Entry(delivery).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeliveryExists(id))
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

        // POST: api/Deliveries
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Delivery>> PostDelivery(DeliveryDTO deliveryDTO)
        {
            PurchaseOrder purchaseOrder = _context.PurchaseOrders.Include(e => e.Supplier).FirstOrDefault(e => e.OrderReference == deliveryDTO.PurchaseOrder);
            Site site = _context.Sites.FirstOrDefault(e => e.SiteCode == deliveryDTO.SiteCode);

            Delivery delivery = new Delivery
            {
                DeliveryId = deliveryDTO.DeliveryId,
                DeliveryMethod = deliveryDTO.DeliveryMethod,
                OnSiteDelivery = true,
                DeliveryStatus = deliveryDTO.DeliveryStatus,
                PayableAmount = purchaseOrder.TotalCost,
                IsFullDelivery = true,
                PurchaseOrder = purchaseOrder,
                Site = site
            };

            GoodsReceipt goodsReceipt = new GoodsReceipt
            {
                PurchaseOrder = purchaseOrder,
                Supplier = purchaseOrder.Supplier,
                Site = purchaseOrder.Site,
                Delivery = delivery,
                DateDelivered = DateTime.UtcNow 
            };

            _context.Deliveries.Add(delivery);
            _context.GoodsReceipt.Add(goodsReceipt);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (DeliveryExists(delivery.DeliveryId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetDelivery", new { id = delivery.DeliveryId }, delivery);
        }

        // DELETE: api/Deliveries/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Delivery>> DeleteDelivery(string id)
        {
            var delivery = await _context.Deliveries.FindAsync(id);
            if (delivery == null)
            {
                return NotFound();
            }

            _context.Deliveries.Remove(delivery);
            await _context.SaveChangesAsync();

            return delivery;
        }

        private bool DeliveryExists(string id)
        {
            return _context.Deliveries.Any(e => e.DeliveryId == id);
        }
    }
}
