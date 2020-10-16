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
            const string SITE_MANAGER_ID = "EMP1";

            using (var context = new ProcurementDbContext(ContextOptions))
            {
                SiteManagersController siteManagersController = new SiteManagersController(context);

                var result = await siteManagersController.GetSiteManager(SITE_MANAGER_ID);

                var viewResult = Assert.IsType<ActionResult<SiteManager>>(result);
                var model = Assert.IsType<SiteManager>(viewResult.Value);

                Assert.Equal(SITE_MANAGER_ID, model.StaffId);
            }
        }

        [Fact]
        public async Task Should_not_return_SiteManager_when_unavailable()
        {
            const string SITE_MANAGER_ID = "EMP100";

            using (var context = new ProcurementDbContext(ContextOptions))
            {
                SiteManagersController siteManagersController = new SiteManagersController(context);

                var result = await siteManagersController.GetSiteManager(SITE_MANAGER_ID);

                var viewResult = Assert.IsType<ActionResult<SiteManager>>(result);
                Assert.IsNotType<SiteManager>(viewResult.Value);
                var response = Assert.IsType<NotFoundResult>(viewResult.Result);
                Assert.Equal(404, response.StatusCode);
            }
        }

        [Fact]
        public async Task Can_add_new_SiteManager_when_it_not_existing()
        {
            const string SITE_MANAGER_ID = "EMP4";
            const string FIRST_NAME = "First Name";
            const string LAST_NAME = "Last Name";
            const string MOBILE = "0718958874";

            using (var context = new ProcurementDbContext(ContextOptions))
            {
                SiteManagersController siteManagersController = new SiteManagersController(context);

                SiteManager siteManager = new SiteManager { StaffId = SITE_MANAGER_ID, FirstName = FIRST_NAME, LastName = LAST_NAME, MobileNo = MOBILE };

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
            const string SITE_MANAGER_ID = "EMP1";
            const string FIRST_NAME = "First Name";
            const string LAST_NAME = "Last Name";
            const string MOBILE = "0718958874";

            using (var context = new ProcurementDbContext(ContextOptions))
            {
                SiteManagersController siteManagersController = new SiteManagersController(context);

                SiteManager siteManager = new SiteManager { StaffId = SITE_MANAGER_ID, FirstName = FIRST_NAME, LastName = LAST_NAME, MobileNo = MOBILE }; ;

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
            const string SITE_MANAGER_ID = "EMP1";

            using (var context = new ProcurementDbContext(ContextOptions))
            {
                SiteManagersController siteManagersController = new SiteManagersController(context);

                var result = await siteManagersController.DeleteSiteManager(SITE_MANAGER_ID);

                var viewResult = Assert.IsType<ActionResult<SiteManager>>(result);
                var model = Assert.IsType<SiteManager>(viewResult.Value);

                Assert.Equal(SITE_MANAGER_ID, model.StaffId);
            }
        }

        [Fact]
        public async Task Cannot_delete_SiteManager_when_it_not_existing()
        {
            const string SITE_MANAGER_ID = "EMP100";

            using (var context = new ProcurementDbContext(ContextOptions))
            {
                SiteManagersController siteManagersController = new SiteManagersController(context);

                var result = await siteManagersController.DeleteSiteManager(SITE_MANAGER_ID);

                var viewResult = Assert.IsType<ActionResult<SiteManager>>(result);
                Assert.IsNotType<SiteManager>(viewResult.Value);
                var response = Assert.IsType<NotFoundResult>(viewResult.Result);
                Assert.Equal(404, response.StatusCode);
            }
        }
    }
}
