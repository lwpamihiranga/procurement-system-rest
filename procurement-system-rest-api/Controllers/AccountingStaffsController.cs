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
    public class AccountingStaffsController : ControllerBase
    {
        private readonly ProcurementDbContext _context;

        public AccountingStaffsController(ProcurementDbContext context)
        {
            _context = context;
        }

        // GET: api/AccountingStaffs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccountingStaff>>> GetAccountingStaff()
        {
            return await _context.AccountingStaff.Include(e => e.InvoicesHandled).ToListAsync();
        }

        // GET: api/AccountingStaffs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AccountingStaff>> GetAccountingStaff(string id)
        {
            var accountingStaff = await _context.AccountingStaff.FindAsync(id);

            if (accountingStaff == null)
            {
                return NotFound();
            }

            return accountingStaff;
        }

        // PUT: api/AccountingStaffs/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccountingStaff(string id, AccountingStaff accountingStaff)
        {
            if (id != accountingStaff.StaffId)
            {
                return BadRequest();
            }

            _context.Entry(accountingStaff).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountingStaffExists(id))
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

        // POST: api/AccountingStaffs
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<AccountingStaff>> PostAccountingStaff(AccountingStaff accountingStaff)
        {
            _context.AccountingStaff.Add(accountingStaff);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AccountingStaffExists(accountingStaff.StaffId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetAccountingStaff", new { id = accountingStaff.StaffId }, accountingStaff);
        }

        // DELETE: api/AccountingStaffs/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<AccountingStaff>> DeleteAccountingStaff(string id)
        {
            var accountingStaff = await _context.AccountingStaff.FindAsync(id);
            if (accountingStaff == null)
            {
                return NotFound();
            }

            _context.AccountingStaff.Remove(accountingStaff);
            await _context.SaveChangesAsync();

            return accountingStaff;
        }

        private bool AccountingStaffExists(string id)
        {
            return _context.AccountingStaff.Any(e => e.StaffId == id);
        }
    }
}
