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
    public class ManagementStaffsController : ControllerBase
    {
        private readonly ProcurementDbContext _context;

        public ManagementStaffsController(ProcurementDbContext context)
        {
            _context = context;
        }

        // GET: api/ManagementStaffs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ManagementStaff>>> GetManagementStaff()
        {
            return await _context.ManagementStaff.ToListAsync();
        }

        // GET: api/ManagementStaffs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ManagementStaff>> GetManagementStaff(string id)
        {
            var managementStaff = await _context.ManagementStaff.FindAsync(id);

            if (managementStaff == null)
            {
                return NotFound();
            }

            return managementStaff;
        }

        // PUT: api/ManagementStaffs/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutManagementStaff(string id, ManagementStaff managementStaff)
        {
            if (id != managementStaff.StaffId)
            {
                return BadRequest();
            }

            _context.Entry(managementStaff).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ManagementStaffExists(id))
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

        // POST: api/ManagementStaffs
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<ManagementStaff>> PostManagementStaff(ManagementStaff managementStaff)
        {
            _context.ManagementStaff.Add(managementStaff);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ManagementStaffExists(managementStaff.StaffId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetManagementStaff", new { id = managementStaff.StaffId }, managementStaff);
        }

        // DELETE: api/ManagementStaffs/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ManagementStaff>> DeleteManagementStaff(string id)
        {
            var managementStaff = await _context.ManagementStaff.FindAsync(id);
            if (managementStaff == null)
            {
                return NotFound();
            }

            _context.ManagementStaff.Remove(managementStaff);
            await _context.SaveChangesAsync();

            return managementStaff;
        }

        private bool ManagementStaffExists(string id)
        {
            return _context.ManagementStaff.Any(e => e.StaffId == id);
        }
    }
}
