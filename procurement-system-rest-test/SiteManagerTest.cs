using Microsoft.EntityFrameworkCore;
using procurement_system_rest_api;
using procurement_system_rest_api.Controllers;
using procurement_system_rest_api.Models;
using System;
using System.Collections.Generic;
using Xunit;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace procurement_system_rest_test
{
    public class SiteManagerTest: SeedDatabase
    {
        public SiteManagerTest() : base(
            new DbContextOptionsBuilder<ProcurementDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options)
        { }

        [Fact]
        public async Task Can_get_all_SiteManager_in_database()
        {
            using (var context = new ProcurementDbContext(ContextOptions))
            {
                SiteManagersController siteManagersController = new SiteManagersController(context);

                var result = await siteManagersController.GetSiteManagers();

                var viewResult = Assert.IsType<ActionResult<IEnumerable<SiteManager>>>(result);
                var model = Assert.IsType<List<SiteManager>>(viewResult.Value);

                Assert.Equal(3, model.Count);
            }
        }
        [Fact]
        public async Task Can_get_SiteManager_By_Id()
        {
            using (var context = new ProcurementDbContext(ContextOptions))
            {
                SiteManagersController siteManagersController = new SiteManagersController(context);

                var result = await siteManagersController.GetSiteManager("EMP1");

                var viewResult = Assert.IsType<ActionResult<SiteManager>>(result);
                var model = Assert.IsType<SiteManager>(viewResult.Value);

                Assert.Equal("EMP1", model.StaffId);
            }
        }
        [Fact]
        public async Task Should_not_return_SiteManager_when_unavailable()
        {
            using (var context = new ProcurementDbContext(ContextOptions))
            {
                SiteManagersController siteManagersController = new SiteManagersController(context);

                var result = await siteManagersController.GetSiteManager("EMP100");

                var viewResult = Assert.IsType<ActionResult<SiteManager>>(result);
                Assert.IsNotType<SiteManager>(viewResult.Value);
                var response = Assert.IsType<NotFoundResult>(viewResult.Result);
                Assert.Equal(404, response.StatusCode);
            }
        }
        [Fact]
        public async Task Can_add_new_SiteManager_when_it_not_existing()
        {
            using (var context = new ProcurementDbContext(ContextOptions))
            {
                SiteManagersController siteManagersController = new SiteManagersController(context);

                SiteManager siteManager = new SiteManager { StaffId = "EMP4", FirstName = "FirstName", LastName = "LastName", MobileNo = "0718958874" };

                var result = await siteManagersController.PostSiteManager(siteManager);

                var viewResult = Assert.IsType<ActionResult<SiteManager>>(result);
                var actionResult = Assert.IsType<CreatedAtActionResult>(viewResult.Result);
                var model = Assert.IsType<SiteManager>(actionResult.Value);
                Assert.Equal("EMP4", model.StaffId);
            }
        }

        [Fact]
        public async Task Cannot_add_SiteManager_when_it_already_exists()
        {
            using (var context = new ProcurementDbContext(ContextOptions))
            {
                SiteManagersController siteManagersController = new SiteManagersController(context);

                SiteManager siteManager = new SiteManager { StaffId = "EMP1", FirstName = "FirstName", LastName = "LastName", MobileNo = "0718956874" };

                try
                {
                    await siteManagersController.PostSiteManager(siteManager);
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
        public async Task Can_delete_SiteManager_by_Id()
        {
            using (var context = new ProcurementDbContext(ContextOptions))
            {
                SiteManagersController siteManagersController = new SiteManagersController(context);

                var result = await siteManagersController.DeleteSiteManager("EMP1");

                var viewResult = Assert.IsType<ActionResult<SiteManager>>(result);
                var model = Assert.IsType<SiteManager>(viewResult.Value);

                Assert.Equal("EMP1", model.StaffId);
            }
        }
        [Fact]
        public async Task Cannot_delete_SiteManager_when_it_not_existing()
        {
            using (var context = new ProcurementDbContext(ContextOptions))
            {
                SiteManagersController siteManagersController = new SiteManagersController(context);

                var result = await siteManagersController.DeleteSiteManager("EMP100");

                var viewResult = Assert.IsType<ActionResult<SiteManager>>(result);
                Assert.IsNotType<SiteManager>(viewResult.Value);
                var response = Assert.IsType<NotFoundResult>(viewResult.Result);
                Assert.Equal(404, response.StatusCode);
            }
        }



    }
}
