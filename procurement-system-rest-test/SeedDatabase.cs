using Microsoft.EntityFrameworkCore;
using procurement_system_rest_api;
using procurement_system_rest_api.DTOs;
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

                var manager1 = new SiteManager { StaffId = "EMP1", FirstName = "FirstName", LastName = "LastName", MobileNo = "0718956874" };

                context.SiteManagers.Add(manager1);
                context.SiteManagers.Add(new SiteManager { StaffId = "EMP2", FirstName = "FirstName", LastName = "LastName", MobileNo = "0718956874" });
                context.SiteManagers.Add(new SiteManager { StaffId = "EMP3", FirstName = "FirstName", LastName = "LastName", MobileNo = "0718956874" });

                context.ManagementStaff.Add(new ManagementStaff { StaffId = "EMP11", FirstName = "FirstName", LastName = "LastName", MobileNo = "0718956874" });
                context.ManagementStaff.Add(new ManagementStaff { StaffId = "EMP12", FirstName = "FirstName", LastName = "LastName", MobileNo = "0718956874" });
                context.ManagementStaff.Add(new ManagementStaff { StaffId = "EMP13", FirstName = "FirstName", LastName = "LastName", MobileNo = "0718956874" });

                context.AccountingStaff.Add(new AccountingStaff { StaffId = "EMP21", FirstName = "FirstName", LastName = "LastName", MobileNo = "0718956874" });
                context.AccountingStaff.Add(new AccountingStaff { StaffId = "EMP22", FirstName = "FirstName", LastName = "LastName", MobileNo = "0718956874" });
                context.AccountingStaff.Add(new AccountingStaff { StaffId = "EMP23", FirstName = "FirstName", LastName = "LastName", MobileNo = "0718956874" });

                context.Sites.Add(new Site { SiteCode = "SITE001", SiteName = "SLIIT Campus Site", SiteAddress = "Malabe", Description = "Malabe SLIIT Campus working site", SiteOfficeNo = "0115489657", SiteManager = manager1 });

                context.Supplier.Add(new Supplier { SupplierCode = "SP1", SupplierName = "MAS Holdings", Address1 = "Colombo 3", CompanyNo = "011548795", MobileNo = "077485698", Email = "supplier@mas.com"  });
                context.Supplier.Add(new Supplier { SupplierCode = "SP2", SupplierName = "MAS Holdings", Address1 = "Colombo 3", CompanyNo = "011548795", MobileNo = "077485698", Email = "supplier@mas.com"  });
                context.Supplier.Add(new Supplier { SupplierCode = "SP3", SupplierName = "MAS Holdings", Address1 = "Colombo 3", CompanyNo = "011548795", MobileNo = "077485698", Email = "supplier@mas.com"  });

                context.SaveChanges();
            }
        }
    }
}
