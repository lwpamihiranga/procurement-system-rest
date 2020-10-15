using Microsoft.EntityFrameworkCore;
using procurement_system_rest_api;
using procurement_system_rest_api.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace procurement_system_rest_test
{
    public class SeedDatabase
    {
        protected SeedDatabase(DbContextOptions<ProcurementDbContext> contextOptions)
        {
            ContextOptions = contextOptions;
            Seed();
        }

        protected DbContextOptions<ProcurementDbContext> ContextOptions { get; }

        private void Seed()
        {
            using (var context = new ProcurementDbContext(ContextOptions))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                context.SiteManagers.Add(new SiteManager { StaffId = "EMP1", FirstName = "FirstName", LastName = "LastName", MobileNo = "0718956874" });
                context.SiteManagers.Add(new SiteManager { StaffId = "EMP2", FirstName = "FirstName", LastName = "LastName", MobileNo = "0718956874" });
                context.SiteManagers.Add(new SiteManager { StaffId = "EMP3", FirstName = "FirstName", LastName = "LastName", MobileNo = "0718956874" });

                context.SaveChanges();

                context.ManagementStaff.Add(new ManagementStaff { StaffId = "EMP11", FirstName = "FirstName", LastName = "LastName", MobileNo = "0718956874" });
                context.ManagementStaff.Add(new ManagementStaff { StaffId = "EMP12", FirstName = "FirstName", LastName = "LastName", MobileNo = "0718956874" });
                context.ManagementStaff.Add(new ManagementStaff { StaffId = "EMP13", FirstName = "FirstName", LastName = "LastName", MobileNo = "0718956874" });

                context.SaveChanges();
            }
        }
    }
}
