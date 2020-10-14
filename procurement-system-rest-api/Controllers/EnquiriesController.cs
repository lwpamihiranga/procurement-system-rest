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
    public class EnquiriesController : ControllerBase
    {
        private readonly ProcurementDbContext _context;

        public EnquiriesController(ProcurementDbContext context)
        {
            _context = context;
        }

        // GET: api/Enquiries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Enquiry>>> GetEnquiries()
        {
            return await _context.Enquiries.Include(e => e.OrderReference).Include(e => e.SiteManager).ToListAsync();
        }

        // GET: api/Enquiries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Enquiry>> GetEnquiry(int id)
        {
            var enquiry = await _context.Enquiries.FindAsync(id);

            if (enquiry == null)
            {
                return NotFound();
            }

            return enquiry;
        }

        // PUT: api/Enquiries/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEnquiry(int id, Enquiry enquiry)
        {
            if (id != enquiry.EnquiryId)
            {
                return BadRequest();
            }

            _context.Entry(enquiry).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EnquiryExists(id))
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

        // POST: api/Enquiries
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Enquiry>> PostEnquiry(EnquiryDTO enquiryDTO)
        {
            PurchaseOrder purchaseOrder = _context.PurchaseOrders.FirstOrDefault(e => e.OrderReference == enquiryDTO.OrderReference);
            SiteManager siteManager = _context.SiteManagers.FirstOrDefault(e => e.StaffId == enquiryDTO.SiteManagerId);

            Enquiry enquiry = new Enquiry
            {
                OrderType = enquiryDTO.OrderType,
                Description = enquiryDTO.Description,
                EnquiryStatus = enquiryDTO.EnquiryStatus,
                OrderReference = purchaseOrder,
                SiteManager = siteManager
            };

            _context.Enquiries.Add(enquiry);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEnquiry", new { id = enquiry.EnquiryId }, enquiry);
        }

        // DELETE: api/Enquiries/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Enquiry>> DeleteEnquiry(int id)
        {
            var enquiry = await _context.Enquiries.FindAsync(id);
            if (enquiry == null)
            {
                return NotFound();
            }

            _context.Enquiries.Remove(enquiry);
            await _context.SaveChangesAsync();

            return enquiry;
        }

        private bool EnquiryExists(int id)
        {
            return _context.Enquiries.Any(e => e.EnquiryId == id);
        }
    }
}
