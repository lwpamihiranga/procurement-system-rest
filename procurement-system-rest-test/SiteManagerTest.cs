using Microsoft.EntityFrameworkCore;
using procurement_system_rest_api;
using procurement_system_rest_api.Controllers;
using procurement_system_rest_api.Models;
using System;
using System.Collections.Generic;
using Xunit;

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
        public async void Test_OneAsync()
        {
          /*  using(var context = new ProcurementDbContext(ContextOptions))
            {
                SiteManagersController siteManagersController = new SiteManagersController(context);

                List<SiteManager> siteManagers = await siteManagersController.GetSiteManagers();

                Assert.Equal(3, siteManagers.Count);
                Assert.Equal("EMP1", siteManagers[0].StaffId);
            }*/
            
            /*using(var context = new ProcurementDbContext(ContextOptions))
            {
                SiteManagersController siteManagersController = new SiteManagersController(context);
                
                SiteManager siteManager = new SiteManager { StaffId = "EMP4", FirstName = "FirstName", LastName = "LastName", MobileNo = "0718956874" };

                await siteManagersController.PostSiteManager(siteManager);

                List<SiteManager> siteManagers = await siteManagersController.GetSiteManagers();

                Assert.Equal(4, siteManagers.Count);
            }*/
        }
    }
}
