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
    public class PurchaseOrdersControllerTest : SeedDatabase
    {
        public PurchaseOrdersControllerTest() : base(
            new DbContextOptionsBuilder<ProcurementDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options)
        { }

        [Fact]
        public async Task Can_get_all_Orders_in_database()
        {
            using (var context = new ProcurementDbContext(ContextOptions))
            {
                PurchaseOrdersController ordersController = new PurchaseOrdersController(context);

                var result = await ordersController.GetPurchaseOrders();

                var viewResult = Assert.IsType<ActionResult<IEnumerable<PurchaseOrder>>>(result);
                var orders = Assert.IsType<List<PurchaseOrder>>(viewResult.Value);
                Assert.Single(orders);
            }
        }

        [Fact]
        public async Task Can_get_Order_details_by_OrderNo()
        {
            const int ORDER = 1;

            using (var context = new ProcurementDbContext(ContextOptions))
            {
                PurchaseOrdersController ordersController = new PurchaseOrdersController(context);

                var result = await ordersController.GetPurchaseOrder(ORDER);

                var viewResult = Assert.IsType<ActionResult<PurchaseOrder>>(result);
                var order = Assert.IsType<PurchaseOrder>(viewResult.Value);
                Assert.NotNull(order);
                Assert.Equal(ORDER, order.OrderReference);
            }
        }

        [Fact]
        public async Task Should_not_return_Order_when_Order_not_existing()
        {
            const int ORDER = 11;

            using (var context = new ProcurementDbContext(ContextOptions))
            {
                PurchaseOrdersController ordersController = new PurchaseOrdersController(context);

                var result = await ordersController.GetPurchaseOrder(ORDER);

                var viewResult = Assert.IsType<ActionResult<PurchaseOrder>>(result);
                Assert.IsNotType<PurchaseOrder>(viewResult.Value);
                var response = Assert.IsType<NotFoundResult>(viewResult.Result);
                Assert.Equal(404, response.StatusCode);
            }
        }

        [Fact]
        public async Task Can_add_Order_when_it_not_existing()
        {
            const int ORDER = 3;
            const string SITE_MANAGER_ID = "EMP1";
            const string SUPPLIER_CODE = "SP1";
            const string SITE_CODE = "SITE001";
            const string SHIPPING_ADDRESS = "MALABE";
            const double TOTAL_COST = 2000.00;
            const string STATUS = "PENDING";
            string[] ITEMS = { "IT001", "IT001", "IT002", "IT002", "IT002", "IT003" };

            using (var context = new ProcurementDbContext(ContextOptions))
            {
                PurchaseOrdersController ordersController = new PurchaseOrdersController(context);

                var order = new PurchaseOrderDTO { SiteManagerId = SITE_MANAGER_ID, SupplierCode = SUPPLIER_CODE, SiteCode = SITE_CODE, ShippingAddress = SHIPPING_ADDRESS, TotalCost = TOTAL_COST, OrderStatus = STATUS, ItemIds = ITEMS };

                var result = await ordersController.PostPurchaseOrder(order);

                var viewResult = Assert.IsType<ActionResult<PurchaseOrder>>(result);
                var actionResult = Assert.IsType<CreatedAtActionResult>(viewResult.Result);
                var model = Assert.IsType<PurchaseOrder>(actionResult.Value);
                Assert.Equal(ORDER, model.OrderReference);
            }
        }

        [Fact]
        public async Task Can_delete_Order_by_OderReference()
        {
            const int ORDER = 1;

            using (var context = new ProcurementDbContext(ContextOptions))
            {
                PurchaseOrdersController ordersController = new PurchaseOrdersController(context);

                var result = await ordersController.DeletePurchaseOrder(ORDER);

                var viewResult = Assert.IsType<ActionResult<PurchaseOrder>>(result);
                var model = Assert.IsType<PurchaseOrder>(viewResult.Value);

                Assert.Equal(ORDER, model.OrderReference);
            }
        }

        [Fact]
        public async Task Cannot_delete_Order_when_it_not_exist()
        {
            const int ORDER = 11;

            using (var context = new ProcurementDbContext(ContextOptions))
            {
                PurchaseOrdersController ordersController = new PurchaseOrdersController(context);

                var result = await ordersController.DeletePurchaseOrder(ORDER);

                var viewResult = Assert.IsType<ActionResult<PurchaseOrder>>(result);
                Assert.IsNotType<PurchaseOrder>(viewResult.Value);
                var response = Assert.IsType<NotFoundResult>(viewResult.Result);
                Assert.Equal(404, response.StatusCode);
            }
        }

        [Fact]
        public async Task Returned_Requisition_should_include_relavant_SiteManager()
        {
            const int ORDER = 1;
            const string SITE_MANAGER_ID = "EMP1";

            using (var context = new ProcurementDbContext(ContextOptions))
            {
                PurchaseOrdersController ordersController = new PurchaseOrdersController(context);

                var result = await ordersController.GetPurchaseOrder(ORDER);

                var viewResult = Assert.IsType<ActionResult<PurchaseOrder>>(result);
                var order = Assert.IsType<PurchaseOrder>(viewResult.Value);
                Assert.NotNull(order);
                Assert.Equal(ORDER, order.OrderReference);

                var siteManager = Assert.IsType<SiteManager>(order.SiteManager);
                Assert.NotNull(siteManager);
                Assert.Equal(SITE_MANAGER_ID, siteManager.StaffId);
            }
        }
    }
}
