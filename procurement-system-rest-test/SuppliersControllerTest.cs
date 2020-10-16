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
    public class SuppliersControllerTest : SeedDatabase
    {
        public SuppliersControllerTest() : base(
            new DbContextOptionsBuilder<ProcurementDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options)
        { }

        [Fact]
        public async Task Can_get_all_Suppliers_in_database()
        {
            using (var context = new ProcurementDbContext(ContextOptions))
            {
                SuppliersController suppliersController = new SuppliersController(context);

                var result = await suppliersController.GetSuppliers();

                var viewResult = Assert.IsType<ActionResult<IEnumerable<Supplier>>>(result);
                var suppliers = Assert.IsType<List<Supplier>>(viewResult.Value);
                Assert.Equal(3, suppliers.Count);
            }
        }

        [Fact]
        public async Task Can_get_Supplier_details_by_SupplierCode()
        {
            const string SUPPLIERCODE = "SP1";

            using (var context = new ProcurementDbContext(ContextOptions))
            {
                SuppliersController suppliersController = new SuppliersController(context);

                var result = await suppliersController.GetSupplier(SUPPLIERCODE);

                var viewResult = Assert.IsType<ActionResult<Supplier>>(result);
                var supplier = Assert.IsType<Supplier>(viewResult.Value);
                Assert.NotNull(supplier);
                Assert.Equal(SUPPLIERCODE, supplier.SupplierCode);
            }
        }

        [Fact]
        public async Task Should_not_return_Supplier_when_SupplierCode_not_existing()
        {
            const string SUPPLIERCODE = "SP001";

            using (var context = new ProcurementDbContext(ContextOptions))
            {
                SuppliersController suppliersController = new SuppliersController(context);

                var result = await suppliersController.GetSupplier(SUPPLIERCODE);

                var viewResult = Assert.IsType<ActionResult<Supplier>>(result);
                Assert.IsNotType<Supplier>(viewResult.Value);
                var response = Assert.IsType<NotFoundResult>(viewResult.Result);
                Assert.Equal(404, response.StatusCode);
            }
        }

        [Fact]
        public async Task Can_add_Supplier_when_it_not_existing()
        {
            using (var context = new ProcurementDbContext(ContextOptions))
            {
                SuppliersController suppliersController = new SuppliersController(context);

                Supplier supplier = new Supplier { SupplierCode = "SP4", SupplierName = "MAS Holdings", Address1 = "Colombo 3", CompanyNo = "011548795", MobileNo = "077485698", Email = "supplier@mas.com" };

                var result = await suppliersController.PostSupplier(supplier);

                var viewResult = Assert.IsType<ActionResult<Supplier>>(result);
                var actionResult = Assert.IsType<CreatedAtActionResult>(viewResult.Result);
                var model = Assert.IsType<Supplier>(actionResult.Value);
                Assert.Equal("SP4", model.SupplierCode);
            }
        }

        [Fact]
        public async Task Cannot_add_Supplier_when_it_exist()
        {
            using (var context = new ProcurementDbContext(ContextOptions))
            {
                SuppliersController suppliersController = new SuppliersController(context);

                Supplier supplier = new Supplier { SupplierCode = "SP1", SupplierName = "MAS Holdings", Address1 = "Colombo 3", CompanyNo = "011548795", MobileNo = "077485698", Email = "supplier@mas.com" };

                try
                {
                    await suppliersController.PostSupplier(supplier);
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
        public async Task Can_delete_Supplier_by_SupplierCode()
        {
            const string SUPPLIERCODE = "SP1";

            using (var context = new ProcurementDbContext(ContextOptions))
            {
                SuppliersController suppliersController = new SuppliersController(context);

                var result = await suppliersController.DeleteSupplier(SUPPLIERCODE);

                var viewResult = Assert.IsType<ActionResult<Supplier>>(result);
                var model = Assert.IsType<Supplier>(viewResult.Value);

                Assert.Equal(SUPPLIERCODE, model.SupplierCode);
            }
        }

        [Fact]
        public async Task Cannot_delete_Supplier_when_it_not_exist()
        {
            const string SUPPLIERCODE = "SP001";

            using (var context = new ProcurementDbContext(ContextOptions))
            {
                SuppliersController suppliersController = new SuppliersController(context);

                var result = await suppliersController.DeleteSupplier(SUPPLIERCODE);

                var viewResult = Assert.IsType<ActionResult<Supplier>>(result);
                Assert.IsNotType<Supplier>(viewResult.Value);
                var response = Assert.IsType<NotFoundResult>(viewResult.Result);
                Assert.Equal(404, response.StatusCode);
            }
        }
    }
}
