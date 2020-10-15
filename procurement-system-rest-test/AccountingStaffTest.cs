using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using procurement_system_rest_api;
using procurement_system_rest_api.Controllers;
using procurement_system_rest_api.Models;
using System;
using System.Collections.Generic;
using System.Text;
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
            using (var context = new ProcurementDbContext(ContextOptions))
            {
                AccountingStaffsController accountingStaffsController = new AccountingStaffsController(context);

                var result = await accountingStaffsController.GetAccountingStaff("EMP21");

                var viewResult = Assert.IsType<ActionResult<AccountingStaff>>(result);
                var model = Assert.IsType<AccountingStaff>(viewResult.Value);

                Assert.Equal("EMP21", model.StaffId);
            }
        }
        [Fact]
        public async Task Should_not_return_AccountingStaff_when_unavailable()
        {
            using (var context = new ProcurementDbContext(ContextOptions))
            {
                AccountingStaffsController accountingStaffsController = new AccountingStaffsController(context);

                var result = await accountingStaffsController.GetAccountingStaff("EMP100");

                var viewResult = Assert.IsType<ActionResult<AccountingStaff>>(result);
                Assert.IsNotType<AccountingStaff>(viewResult.Value);
                var response = Assert.IsType<NotFoundResult>(viewResult.Result);
                Assert.Equal(404, response.StatusCode);
            }
        }
        [Fact]
        public async Task Can_add_new_AccountingStaff_when_it_not_existing()
        {
            using (var context = new ProcurementDbContext(ContextOptions))
            {
                AccountingStaffsController accountingStaffsController = new AccountingStaffsController(context);

                AccountingStaff accountingStaff = new AccountingStaff { StaffId = "EMP24", FirstName = "FirstName", LastName = "LastName", MobileNo = "0718956874" };

                var result = await accountingStaffsController.PostAccountingStaff(accountingStaff);

                var viewResult = Assert.IsType<ActionResult<AccountingStaff>>(result);
                var actionResult = Assert.IsType<CreatedAtActionResult>(viewResult.Result);
                var model = Assert.IsType<AccountingStaff>(actionResult.Value);
                Assert.Equal("EMP24", model.StaffId);
            }
        }
        [Fact]
        public async Task Cannot_add_AccountingStaff_when_it_already_exists()
        {
            using (var context = new ProcurementDbContext(ContextOptions))
            {
                AccountingStaffsController accountingStaffsController = new AccountingStaffsController(context);

                AccountingStaff accountingStaff = new AccountingStaff { StaffId = "EMP21", FirstName = "FirstName", LastName = "LastName", MobileNo = "0718956874" };

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
            using (var context = new ProcurementDbContext(ContextOptions))
            {
                AccountingStaffsController accountingStaffsController = new AccountingStaffsController(context);

                var result = await accountingStaffsController.DeleteAccountingStaff("EMP21");

                var viewResult = Assert.IsType<ActionResult<AccountingStaff>>(result);
                var model = Assert.IsType<AccountingStaff>(viewResult.Value);

                Assert.Equal("EMP21", model.StaffId);
            }
        }
        [Fact]
        public async Task Cannot_delete_AccountingStaff_when_it_not_existing()
        {
            using (var context = new ProcurementDbContext(ContextOptions))
            {
                AccountingStaffsController accountingStaffsController = new AccountingStaffsController(context);

                var result = await accountingStaffsController.DeleteAccountingStaff("EMP100");

                var viewResult = Assert.IsType<ActionResult<AccountingStaff>>(result);
                Assert.IsNotType<AccountingStaff>(viewResult.Value);
                var response = Assert.IsType<NotFoundResult>(viewResult.Result);
                Assert.Equal(404, response.StatusCode);
            }
        }



    }
}
