using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using procurement_system_rest_api;
using procurement_system_rest_api.Controllers;
using procurement_system_rest_api.DTOs;
using procurement_system_rest_api.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace procurement_system_rest_test
{
    public class SitesControllerTest : SeedDatabase
    {
        public SitesControllerTest() : base(
            new DbContextOptionsBuilder<ProcurementDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options)
        { }

        [Fact]
        public async Task Can_get_all_Sites_in_database()
        {
            using(var context = new ProcurementDbContext(ContextOptions))
            {
                SitesController sitesController = new SitesController(context);

                var result = await sitesController.GetSites();

                var viewResult = Assert.IsType<ActionResult<IEnumerable<Site>>>(result);
                var sites = Assert.IsType<List<Site>>(viewResult.Value);
                Assert.Single(sites);
            }
        }

        [Fact]
        public async Task Can_get_Site_details_by_SiteCode()
        {
            const string SITECODE = "SITE001";

            using (var context = new ProcurementDbContext(ContextOptions))
            {
                SitesController sitesController = new SitesController(context);

                var result = await sitesController.GetSite(SITECODE);

                var viewResult = Assert.IsType<ActionResult<Site>>(result);
                var site = Assert.IsType<Site>(viewResult.Value);
                Assert.NotNull(site);
                Assert.Equal(SITECODE, site.SiteCode);
            }
        }

        [Fact]
        public async Task Should_not_return_Site_when_SideCode_not_existing()
        {
            const string SITECODE = "SITE0001";

            using (var context = new ProcurementDbContext(ContextOptions))
            {
                SitesController sitesController = new SitesController(context);

                var result = await sitesController.GetSite(SITECODE);

                var viewResult = Assert.IsType<ActionResult<Site>>(result);
                Assert.IsNotType<Site>(viewResult.Value);
                var response = Assert.IsType<NotFoundResult>(viewResult.Result);
                Assert.Equal(404, response.StatusCode);
            }
        }

        [Fact]
        public async Task Can_add_Site_when_it_not_existing()
        {
            using (var context = new ProcurementDbContext(ContextOptions))
            {
                SitesController sitesController = new SitesController(context);

                SiteDTO siteDto = new SiteDTO { SiteCode = "SITE002", SiteName = "SLIIT Campus Site", SiteAddress = "Malabe", Description = "Malabe SLIIT Campus working site", SiteOfficeNo = "0115489657", SiteManagerId = "EMP1" };

                var result = await sitesController.PostSite(siteDto);

                var viewResult = Assert.IsType<ActionResult<Site>>(result);
                var actionResult = Assert.IsType<CreatedAtActionResult>(viewResult.Result);
                var model = Assert.IsType<Site>(actionResult.Value);
                Assert.Equal("SITE002", model.SiteCode);
            }
        }

        [Fact]
        public async Task Cannot_add_Site_when_it_exist()
        {
            using (var context = new ProcurementDbContext(ContextOptions))
            {
                SitesController sitesController = new SitesController(context);

                SiteDTO siteDto = new SiteDTO { SiteCode = "SITE001", SiteName = "SLIIT Campus Site", SiteAddress = "Malabe", Description = "Malabe SLIIT Campus working site", SiteOfficeNo = "0115489657", SiteManagerId = "EMP1" };

                try
                {
                    await sitesController.PostSite(siteDto);
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
        public async Task Can_delete_Site_by_SiteCode()
        {
            const string SITECODE = "SITE001";

            using (var context = new ProcurementDbContext(ContextOptions))
            {
                SitesController sitesController = new SitesController(context);


                var result = await sitesController.DeleteSite(SITECODE);

                var viewResult = Assert.IsType<ActionResult<Site>>(result);
                var model = Assert.IsType<Site>(viewResult.Value);

                Assert.Equal(SITECODE, model.SiteCode);
            }
        }

        [Fact]
        public async Task Cannot_delete_Site_when_it_not_exist()
        {
            const string SITECODE = "SITE0001";

            using (var context = new ProcurementDbContext(ContextOptions))
            {
                SitesController sitesController = new SitesController(context);

                var result = await sitesController.DeleteSite(SITECODE);

                var viewResult = Assert.IsType<ActionResult<Site>>(result);
                Assert.IsNotType<Site>(viewResult.Value);
                var response = Assert.IsType<NotFoundResult>(viewResult.Result);
                Assert.Equal(404, response.StatusCode);
            }

        }
    }
}
