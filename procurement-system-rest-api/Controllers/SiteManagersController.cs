using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using procurement_system_rest_api;
using procurement_system_rest_api.Models;

namespace procurement_system_rest_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SiteManagersController : ControllerBase
    {
        private readonly ProcurementDbContext _context;

        public SiteManagersController(ProcurementDbContext context)
        {
            _context = context;
        }

        // GET: api/SiteManagers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SiteManager>>> GetSiteManagers()
        {
            return await _context.SiteManagers.Include(e => e.SiteList).ToListAsync();
        }

        // GET: api/SiteManagers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SiteManager>> GetSiteManager(string id)
        {
            var siteManager = await _context.SiteManagers.FindAsync(id);

            if (siteManager == null)
            {
                return NotFound();
            }

            return siteManager;
        }

        // PUT: api/SiteManagers/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSiteManager(string id, SiteManager siteManager)
        {
            if (id != siteManager.StaffId)
            {
                return BadRequest();
            }

            _context.Entry(siteManager).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SiteManagerExists(id))
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

        // POST: api/SiteManagers
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<SiteManager>> PostSiteManager(SiteManager siteManager)
        {
            _context.SiteManagers.Add(siteManager);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SiteManagerExists(siteManager.StaffId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetSiteManager", new { id = siteManager.StaffId }, siteManager);
        }

        // DELETE: api/SiteManagers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<SiteManager>> DeleteSiteManager(string id)
        {
            var siteManager = await _context.SiteManagers.FindAsync(id);
            if (siteManager == null)
            {
                return NotFound();
            }

            _context.SiteManagers.Remove(siteManager);
            await _context.SaveChangesAsync();

            return siteManager;
        }

        private bool SiteManagerExists(string id)
        {
            return _context.SiteManagers.Any(e => e.StaffId == id);
        }
    }
}
