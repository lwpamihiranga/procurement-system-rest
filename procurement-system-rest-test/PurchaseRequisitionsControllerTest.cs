using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using procurement_system_rest_api;
using procurement_system_rest_api.Controllers;
using procurement_system_rest_api.DTOs;
using procurement_system_rest_api.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace procurement_system_rest_test
{
    public class PurchaseRequisitionsControllerTest : SeedDatabase
    {
        public PurchaseRequisitionsControllerTest() : base(
            new DbContextOptionsBuilder<ProcurementDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options)
        { }

        [Fact]
        public async Task Can_get_all_Requisitions_in_database()
        {
            using (var context = new ProcurementDbContext(ContextOptions))
            {
                PurchaseRequisitionsController requisitionsController = new PurchaseRequisitionsController(context);

                var result = await requisitionsController.GetPurchaseRequisitions();

                var viewResult = Assert.IsType<ActionResult<IEnumerable<PurchaseRequisition>>>(result);
                var requisitions = Assert.IsType<List<PurchaseRequisition>>(viewResult.Value);
                Assert.Single(requisitions);
            }
        }

        [Fact]
        public async Task Can_get_Requisition_details_by_RequisitionNo()
        {
            const int REQUISITION = 1;

            using (var context = new ProcurementDbContext(ContextOptions))
            {
                PurchaseRequisitionsController requisitionsController = new PurchaseRequisitionsController(context);

                var result = await requisitionsController.GetPurchaseRequisition(REQUISITION);

                var viewResult = Assert.IsType<ActionResult<PurchaseRequisition>>(result);
                var requisition = Assert.IsType<PurchaseRequisition>(viewResult.Value);
                Assert.NotNull(requisition);
                Assert.Equal(REQUISITION, requisition.RequisitionNo);
            }
        }

        [Fact]
        public async Task Should_not_return_Requisition_when_Requisition_not_existing()
        {
            const int REQUISITION = 11;

            using (var context = new ProcurementDbContext(ContextOptions))
            {
                PurchaseRequisitionsController requisitionsController = new PurchaseRequisitionsController(context);

                var result = await requisitionsController.GetPurchaseRequisition(REQUISITION);

                var viewResult = Assert.IsType<ActionResult<PurchaseRequisition>>(result);
                Assert.IsNotType<PurchaseRequisition>(viewResult.Value);
                var response = Assert.IsType<NotFoundResult>(viewResult.Result);
                Assert.Equal(404, response.StatusCode);
            }
        }

        [Fact]
        public async Task Can_add_Requisition()
        {
            const int REQUISITION = 3;
            const string SITE_MANAGER_ID = "EMP1";
            const string SUPPLIER_CODE = "SP1";
            const string SITE_CODE = "SITE001";
            const string SHIPPING_ADDRESS = "MALABE";
            const double TOTAL_COST = 2000.00;
            const string STATUS = "PENDING";
            const string COMMENTS = "Immediate Request";
            string[] ITEMS = { "IT001", "IT001", "IT002", "IT002", "IT002", "IT003" };

            using (var context = new ProcurementDbContext(ContextOptions))
            {
                PurchaseRequisitionsController requisitionsController = new PurchaseRequisitionsController(context);

                var requisition = new PurchaseRequisitionDTO { SiteManagerId = SITE_MANAGER_ID, SupplierCode = SUPPLIER_CODE, SiteCode = SITE_CODE, ShippingAddress = SHIPPING_ADDRESS, TotalCost = TOTAL_COST, Status = STATUS, Comments = COMMENTS, ItemIds = ITEMS };

                var result = await requisitionsController.PostPurchaseRequisition(requisition);

                var viewResult = Assert.IsType<ActionResult<PurchaseRequisition>>(result);
                var actionResult = Assert.IsType<CreatedAtActionResult>(viewResult.Result);
                var model = Assert.IsType<PurchaseRequisition>(actionResult.Value);
                Assert.Equal(REQUISITION, model.RequisitionNo);
            }
        }

        [Fact]
        public async Task Can_delete_Requisition_by_RequisitionNo()
        {
            const int REQUISITION = 1;

            using (var context = new ProcurementDbContext(ContextOptions))
            {
                PurchaseRequisitionsController requisitionsController = new PurchaseRequisitionsController(context);

                var result = await requisitionsController.DeletePurchaseRequisition(REQUISITION);

                var viewResult = Assert.IsType<ActionResult<PurchaseRequisition>>(result);
                var model = Assert.IsType<PurchaseRequisition>(viewResult.Value);

                Assert.Equal(REQUISITION, model.RequisitionNo);
            }
        }

        [Fact]
        public async Task Cannot_delete_Requisition_when_it_not_exist()
        {
            const int REQUISITION = 11;

            using (var context = new ProcurementDbContext(ContextOptions))
            {
                PurchaseRequisitionsController requisitionsController = new PurchaseRequisitionsController(context);

                var result = await requisitionsController.DeletePurchaseRequisition(REQUISITION);

                var viewResult = Assert.IsType<ActionResult<PurchaseRequisition>>(result);
                Assert.IsNotType<PurchaseRequisition>(viewResult.Value);
                var response = Assert.IsType<NotFoundResult>(viewResult.Result);
                Assert.Equal(404, response.StatusCode);
            }
        }

        [Fact]
        public async Task Returned_Requisition_should_include_relavant_SiteManager()
        {
            const int REQUISITION = 1;
            const string SITE_MANAGER_ID = "EMP1";

            using (var context = new ProcurementDbContext(ContextOptions))
            {
                PurchaseRequisitionsController requisitionsController = new PurchaseRequisitionsController(context);

                var result = await requisitionsController.GetPurchaseRequisition(REQUISITION);

                var viewResult = Assert.IsType<ActionResult<PurchaseRequisition>>(result);
                var requisition = Assert.IsType<PurchaseRequisition>(viewResult.Value);
                Assert.NotNull(requisition);
                Assert.Equal(REQUISITION, requisition.RequisitionNo);

                var siteManager = Assert.IsType<SiteManager>(requisition.SiteManager);
                Assert.NotNull(siteManager);
                Assert.Equal(SITE_MANAGER_ID, siteManager.StaffId);
            }
        }
    }
}
