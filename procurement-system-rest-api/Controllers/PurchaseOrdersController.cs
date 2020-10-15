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
    public class PurchaseOrdersController : ControllerBase
    {
        private readonly ProcurementDbContext _context;

        public PurchaseOrdersController(ProcurementDbContext context)
        {
            _context = context;
        }

        // GET: api/PurchaseOrders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PurchaseOrder>>> GetPurchaseOrders()
        {
            return await _context.PurchaseOrders
                        .Include(e => e.PurchaseOrderItems)
                        .Include(e => e.SiteManager)
                        .Include(e => e.Supplier)
                        .Include(e => e.ApprovedBy)
                        .ToListAsync();
        }

        // GET: api/PurchaseOrders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PurchaseOrder>> GetPurchaseOrder(int id)
        {
            var purchaseOrder = await _context.PurchaseOrders
                                    .Include(e => e.PurchaseOrderItems)
                                    .Include(e => e.SiteManager)
                                    .Include(e => e.Supplier)
                                    .Include(e => e.ApprovedBy)
                                    .FirstOrDefaultAsync(e => e.OrderReference == id);

            if (purchaseOrder == null)
            {
                return NotFound();
            }

            return purchaseOrder;
        }

        // PUT: api/PurchaseOrders/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPurchaseOrder(int id, PurchaseOrder purchaseOrder)
        {
            if (id != purchaseOrder.OrderReference)
            {
                return BadRequest();
            }

            //_context.Entry(purchaseOrder).State = EntityState.Modified;
            var existingOrder = _context.PurchaseOrders.Find(id);

            existingOrder.OrderStatus = purchaseOrder.OrderStatus;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PurchaseOrderExists(id))
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

        // POST: api/PurchaseOrders
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<PurchaseOrder>> PostPurchaseOrder(PurchaseOrderDTO purchaseOrderDTO)
        {
            SiteManager siteManager = _context.SiteManagers.FirstOrDefault(e => e.StaffId == purchaseOrderDTO.SiteManagerId);
            Supplier supplier = _context.Supplier.FirstOrDefault(e => e.SupplierCode == purchaseOrderDTO.SupplierCode);
            Site site = _context.Sites.FirstOrDefault(e => e.SiteCode == purchaseOrderDTO.SiteCode);

            PurchaseOrder purchaseOrder = new PurchaseOrder
            {
                ShippingAddress = purchaseOrderDTO.ShippingAddress,
                DeliverBefore = purchaseOrderDTO.DeliverBefore,
                DeliveryCost = purchaseOrderDTO.DeliveryCost,
                TotalCost = purchaseOrderDTO.TotalCost,
                OrderStatus = purchaseOrderDTO.OrderStatus,
                SiteManager = siteManager,
                Supplier = supplier,
                Site = site
            };

            if(purchaseOrderDTO.ManagementStaffId != null)
            {
                ManagementStaff managementStaff = _context.ManagementStaff.FirstOrDefault(e => e.StaffId == purchaseOrderDTO.ManagementStaffId);

                purchaseOrder.ApprovedBy = managementStaff;
            }

            var itemMap = new Dictionary<string, int>();

            foreach (string itemId in purchaseOrderDTO.ItemIds)
            {
                if (itemMap.ContainsKey(itemId))
                {
                    itemMap[itemId] = itemMap[itemId] + 1;
                }
                else
                {
                    itemMap.Add(itemId, 1);
                }
            }

            for (int i = 0; i < itemMap.Count; i++)
            {
                var item = new PurchaseOrderItems { ItemId = itemMap.ElementAt(i).Key, PurchaseOrder = purchaseOrder, ItemCount = itemMap.ElementAt(i).Value };

                _context.Set<PurchaseOrderItems>().Add(item);
            }

            _context.PurchaseOrders.Add(purchaseOrder);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPurchaseOrder", new { id = purchaseOrder.OrderReference }, purchaseOrder);
        }

        // DELETE: api/PurchaseOrders/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PurchaseOrder>> DeletePurchaseOrder(int id)
        {
            var purchaseOrder = await _context.PurchaseOrders.FindAsync(id);
            if (purchaseOrder == null)
            {
                return NotFound();
            }

            _context.PurchaseOrders.Remove(purchaseOrder);
            await _context.SaveChangesAsync();

            return purchaseOrder;
        }

        private bool PurchaseOrderExists(int id)
        {
            return _context.PurchaseOrders.Any(e => e.OrderReference == id);
        }
    }
}
