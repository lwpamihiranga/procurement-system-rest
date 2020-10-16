using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using procurement_system_rest_api;
using procurement_system_rest_api.Controllers;
using procurement_system_rest_api.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace procurement_system_rest_test
{
    public class AccountingStaffTest : SeedDatabase
    {
        public AccountingStaffTest() : base(
            new DbContextOptionsBuilder<ProcurementDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options)
        { }

        [Fact]
        public async Task Can_get_all_AccountingStaff_in_database()
        {
            using (var context = new ProcurementDbContext(ContextOptions))
            {
                AccountingStaffsController accountingStaffsController = new AccountingStaffsController(context);

                var result = await accountingStaffsController.GetAccountingStaff();

                var viewResult = Assert.IsType<ActionResult<IEnumerable<AccountingStaff>>>(result);
                var model = Assert.IsType<List<AccountingStaff>>(viewResult.Value);

                Assert.Equal(3, model.Count);
            }
        }
        [Fact]

        public async Task Can_get_AccountingStaff_By_Id()
        {
            const string ACCOUNTING_STAFF_ID = "EMP21";

            using (var context = new ProcurementDbContext(ContextOptions))
            {
                AccountingStaffsController accountingStaffsController = new AccountingStaffsController(context);

                var result = await accountingStaffsController.GetAccountingStaff(ACCOUNTING_STAFF_ID);

                var viewResult = Assert.IsType<ActionResult<AccountingStaff>>(result);
                var model = Assert.IsType<AccountingStaff>(viewResult.Value);

                Assert.Equal(ACCOUNTING_STAFF_ID, model.StaffId);
            }
        }

        [Fact]
        public async Task Should_not_return_AccountingStaff_when_unavailable()
        {
            const string ACCOUNTING_STAFF_ID = "EMP100";

            using (var context = new ProcurementDbContext(ContextOptions))
            {
                AccountingStaffsController accountingStaffsController = new AccountingStaffsController(context);

                var result = await accountingStaffsController.GetAccountingStaff(ACCOUNTING_STAFF_ID);

                var viewResult = Assert.IsType<ActionResult<AccountingStaff>>(result);
                Assert.IsNotType<AccountingStaff>(viewResult.Value);
                var response = Assert.IsType<NotFoundResult>(viewResult.Result);
                Assert.Equal(404, response.StatusCode);
            }
        }

        [Fact]
        public async Task Can_add_new_AccountingStaff_when_it_not_existing()
        {
            const string ACCOUNTING_STAFF_ID = "EMP24";
            const string FIRST_NAME = "First Name";
            const string LAST_NAME = "Last Name";
            const string MOBILE = "0718958874";

            using (var context = new ProcurementDbContext(ContextOptions))
            {
                AccountingStaffsController accountingStaffsController = new AccountingStaffsController(context);

                AccountingStaff accountingStaff = new AccountingStaff { StaffId = ACCOUNTING_STAFF_ID, FirstName = FIRST_NAME, LastName = LAST_NAME, MobileNo = MOBILE };

                var result = await accountingStaffsController.PostAccountingStaff(accountingStaff);

                var viewResult = Assert.IsType<ActionResult<AccountingStaff>>(result);
                var actionResult = Assert.IsType<CreatedAtActionResult>(viewResult.Result);
                var model = Assert.IsType<AccountingStaff>(actionResult.Value);
                Assert.Equal(ACCOUNTING_STAFF_ID, model.StaffId);
            }
        }

        [Fact]
        public async Task Cannot_add_AccountingStaff_when_it_already_exists()
        {
            const string ACCOUNTING_STAFF_ID = "EMP21";
            const string FIRST_NAME = "First Name";
            const string LAST_NAME = "Last Name";
            const string MOBILE = "0718958874";

            using (var context = new ProcurementDbContext(ContextOptions))
            {
                AccountingStaffsController accountingStaffsController = new AccountingStaffsController(context);

                AccountingStaff accountingStaff = new AccountingStaff { StaffId = ACCOUNTING_STAFF_ID, FirstName = FIRST_NAME, LastName = LAST_NAME, MobileNo = MOBILE };

                try
                {
                    await accountingStaffsController.PostAccountingStaff(accountingStaff);
                }
                catch (Exception exception)
                {
                    Assert.NotNull(exception);
                    return;
                }

                Assert.True(false);
            }
        }

        [Fact]
        public async Task Can_delete_AccountingStaff_by_Id()
        {
            const string ACCOUNTING_STAFF_ID = "EMP21";

            using (var context = new ProcurementDbContext(ContextOptions))
            {
                AccountingStaffsController accountingStaffsController = new AccountingStaffsController(context);

                var result = await accountingStaffsController.DeleteAccountingStaff(ACCOUNTING_STAFF_ID);

                var viewResult = Assert.IsType<ActionResult<AccountingStaff>>(result);
                var model = Assert.IsType<AccountingStaff>(viewResult.Value);

                Assert.Equal(ACCOUNTING_STAFF_ID, model.StaffId);
            }
        }

        [Fact]
        public async Task Cannot_delete_AccountingStaff_when_it_not_existing()
        {
            const string ACCOUNTING_STAFF_ID = "EMP100";

            using (var context = new ProcurementDbContext(ContextOptions))
            {
                AccountingStaffsController accountingStaffsController = new AccountingStaffsController(context);

                var result = await accountingStaffsController.DeleteAccountingStaff(ACCOUNTING_STAFF_ID);

                var viewResult = Assert.IsType<ActionResult<AccountingStaff>>(result);
                Assert.IsNotType<AccountingStaff>(viewResult.Value);
                var response = Assert.IsType<NotFoundResult>(viewResult.Result);
                Assert.Equal(404, response.StatusCode);
            }
        }
    }
}
