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
    public class ManagementStaffTest: SeedDatabase
    {
        public ManagementStaffTest() : base(
            new DbContextOptionsBuilder<ProcurementDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options)
        { }

        [Fact]
        public async Task Can_get_all_ManagementStaff_in_database()
        {
            using (var context = new ProcurementDbContext(ContextOptions))
            {
                ManagementStaffsController managementStaffsController = new ManagementStaffsController(context);

                var result = await managementStaffsController.GetManagementStaff();

                var viewResult = Assert.IsType<ActionResult<IEnumerable<ManagementStaff>>>(result);
                var model = Assert.IsType<List<ManagementStaff>>(viewResult.Value);

                Assert.Equal(3, model.Count);
            }
        }

        [Fact]
        public async Task Can_get_ManagementStaff_By_Id()
        {
            const string MANAGEMENT_STAFF_ID = "EMP11";

            using (var context = new ProcurementDbContext(ContextOptions))
            {
                ManagementStaffsController managementStaffsController = new ManagementStaffsController(context);

                var result = await managementStaffsController.GetManagementStaff(MANAGEMENT_STAFF_ID);

                var viewResult = Assert.IsType<ActionResult<ManagementStaff>>(result);
                var model = Assert.IsType<ManagementStaff>(viewResult.Value);

                Assert.Equal(MANAGEMENT_STAFF_ID, model.StaffId);
            }
        }

        [Fact]
        public async Task Should_not_return_ManagementStaff_when_unavailable()
        {
            const string MANAGEMENT_STAFF_ID = "EMP100";

            using (var context = new ProcurementDbContext(ContextOptions))
            {
                ManagementStaffsController managementStaffsController = new ManagementStaffsController(context);

                var result = await managementStaffsController.GetManagementStaff(MANAGEMENT_STAFF_ID);

                var viewResult = Assert.IsType<ActionResult<ManagementStaff>>(result);
                Assert.IsNotType<ManagementStaff>(viewResult.Value);
                var response = Assert.IsType<NotFoundResult>(viewResult.Result);
                Assert.Equal(404, response.StatusCode);
            }
        }

        [Fact]
        public async Task Can_add_new_ManagementStaff_when_it_not_existing()
        {
            const string MANAGEMENT_STAFF_ID = "EMP14";
            const string FIRST_NAME = "First Name";
            const string LAST_NAME = "Last Name";
            const string MOBILE = "0718958874";

            using (var context = new ProcurementDbContext(ContextOptions))
            {
                ManagementStaffsController managementStaffsController = new ManagementStaffsController(context);

                ManagementStaff managementStaff = new ManagementStaff { StaffId = MANAGEMENT_STAFF_ID, FirstName = FIRST_NAME, LastName = LAST_NAME, MobileNo = MOBILE };

                var result = await managementStaffsController.PostManagementStaff(managementStaff);

                var viewResult = Assert.IsType<ActionResult<ManagementStaff>>(result);
                var actionResult = Assert.IsType<CreatedAtActionResult>(viewResult.Result);
                var model = Assert.IsType<ManagementStaff>(actionResult.Value);
                Assert.Equal(MANAGEMENT_STAFF_ID, model.StaffId);
            }
        }

        [Fact]
        public async Task Cannot_add_ManagementStaff_when_it_already_exists()
        {
            const string MANAGEMENT_STAFF_ID = "EMP11";
            const string FIRST_NAME = "First Name";
            const string LAST_NAME = "Last Name";
            const string MOBILE = "0718958874";

            using (var context = new ProcurementDbContext(ContextOptions))
            {
                ManagementStaffsController managementStaffsController = new ManagementStaffsController(context);

                ManagementStaff managementStaff = new ManagementStaff { StaffId = MANAGEMENT_STAFF_ID, FirstName = FIRST_NAME, LastName = LAST_NAME, MobileNo = MOBILE };

                try
                {
                    await managementStaffsController.PostManagementStaff(managementStaff);
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
        public async Task Can_delete_ManagementStaff_by_Id()
        {
            const string MANAGEMENT_STAFF_ID = "EMP11";

            using (var context = new ProcurementDbContext(ContextOptions))
            {
                ManagementStaffsController managementStaffsController = new ManagementStaffsController(context);

                var result = await managementStaffsController.DeleteManagementStaff(MANAGEMENT_STAFF_ID);

                var viewResult = Assert.IsType<ActionResult<ManagementStaff>>(result);
                var model = Assert.IsType<ManagementStaff>(viewResult.Value);

                Assert.Equal(MANAGEMENT_STAFF_ID, model.StaffId);
            }
        }

        [Fact]
        public async Task Cannot_delete_ManagementStaff_when_it_not_existing()
        {
            const string MANAGEMENT_STAFF_ID = "EMP100";

            using (var context = new ProcurementDbContext(ContextOptions))
            {
                ManagementStaffsController managementStaffsController = new ManagementStaffsController(context);

                var result = await managementStaffsController.DeleteManagementStaff(MANAGEMENT_STAFF_ID);

                var viewResult = Assert.IsType<ActionResult<ManagementStaff>>(result);
                Assert.IsNotType<ManagementStaff>(viewResult.Value);
                var response = Assert.IsType<NotFoundResult>(viewResult.Result);
                Assert.Equal(404, response.StatusCode);
            }
        }
    }
}
