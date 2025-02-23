﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using procurement_system_rest_api.DTOs;
using procurement_system_rest_api.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace procurement_system_rest_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SitesController : ControllerBase
    {
        private readonly ProcurementDbContext _context;

        public SitesController(ProcurementDbContext context)
        {
            _context = context;
        }

        // GET: api/Sites
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Site>>> GetSites()
        {
            return await _context.Sites.Include(e => e.SiteManager).ToListAsync();
        }

        // GET: api/Sites/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Site>> GetSite(string id)
        {
            var site = await _context.Sites.FindAsync(id);

            if (site == null)
            {
                return NotFound();
            }

            return site;
        }

        // PUT: api/Sites/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSite(string id, Site site)
        {
            if (id != site.SiteCode)
            {
                return BadRequest();
            }

            Site Site = _context.Sites.FirstOrDefault(e => e.SiteCode == id);
            SiteManager SiteManager = _context.SiteManagers.FirstOrDefault(e => e.StaffId == site.SiteManager.StaffId);
            Site.SiteManager = SiteManager;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SiteExists(id))
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

        // POST: api/Sites
        [HttpPost]
        public async Task<ActionResult<Site>> PostSite(SiteDTO siteDTO)
        {
            SiteManager siteManager = _context.SiteManagers.FirstOrDefault(e => e.StaffId == siteDTO.SiteManagerId);

            Site site = new Site
            {
                SiteCode = siteDTO.SiteCode,
                SiteName = siteDTO.SiteName,
                SiteAddress = siteDTO.SiteAddress,
                Description = siteDTO.Description,
                SiteOfficeNo = siteDTO.SiteOfficeNo,
                SiteManager = siteManager
            };

            _context.Sites.Add(site);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SiteExists(site.SiteCode))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetSite", new { id = site.SiteCode }, site);
        }

        // DELETE: api/Sites/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Site>> DeleteSite(string id)
        {
            var site = await _context.Sites.FindAsync(id);
            if (site == null)
            {
                return NotFound();
            }

            _context.Sites.Remove(site);
            await _context.SaveChangesAsync();

            return site;
        }

        private bool SiteExists(string id)
        {
            return _context.Sites.Any(e => e.SiteCode == id);
        }
    }
}
