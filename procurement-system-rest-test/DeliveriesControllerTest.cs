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
    public class DeliveriesControllerTest : SeedDatabase
    {
        public DeliveriesControllerTest() : base(
            new DbContextOptionsBuilder<ProcurementDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options)
        { }

        [Fact]
        public async Task Can_get_all_Delivery_details_in_database()
        {
            using (var context = new ProcurementDbContext(ContextOptions))
            {
                DeliveriesController deliveriesController = new DeliveriesController(context);

                var result = await deliveriesController.GetDeliveries();

                var viewResult = Assert.IsType<ActionResult<IEnumerable<Delivery>>>(result);
                var delivery = Assert.IsType<List<Delivery>>(viewResult.Value);

                Assert.Single(delivery);
            }
        }

        [Fact]
        public async Task Can_get_Delivery_By_Id()
        {
            const string DELIVERY_ID = "DL001";

            using (var context = new ProcurementDbContext(ContextOptions))
            {
                DeliveriesController deliveriesController = new DeliveriesController(context);

                var result = await deliveriesController.GetDelivery(DELIVERY_ID);

                var viewResult = Assert.IsType<ActionResult<Delivery>>(result);
                var delivery = Assert.IsType<Delivery>(viewResult.Value);

                Assert.Equal(DELIVERY_ID, delivery.DeliveryId);
            }
        }

        [Fact]
        public async Task Should_not_return_Delivery_when_unavailable()
        {
            const string DELIVERY_ID = "DL0001";

            using (var context = new ProcurementDbContext(ContextOptions))
            {
                DeliveriesController deliveriesController = new DeliveriesController(context);

                var result = await deliveriesController.GetDelivery(DELIVERY_ID);

                var viewResult = Assert.IsType<ActionResult<Delivery>>(result);
                Assert.IsNotType<Delivery>(viewResult.Value);
                var response = Assert.IsType<NotFoundResult>(viewResult.Result);
                Assert.Equal(404, response.StatusCode);
            }
        }

        [Fact]
        public async Task Can_add_new_Delivery_when_it_not_existing()
        {
            const string DELIVERY_ID = "DL002";
            const string SITE_CODE = "SITE001";
            const int ORDER_REFERENCE = 1;
            const string STATUS = "ON PROCESS";
        
            using (var context = new ProcurementDbContext(ContextOptions))
            {
                DeliveriesController deliveriesController = new DeliveriesController(context);

                DeliveryDTO deliveryDto = new DeliveryDTO { DeliveryId = DELIVERY_ID, SiteCode = SITE_CODE, PurchaseOrder = ORDER_REFERENCE, DeliveryStatus = STATUS };

                var result = await deliveriesController.PostDelivery(deliveryDto);

                var viewResult = Assert.IsType<ActionResult<Delivery>>(result);
                var actionResult = Assert.IsType<CreatedAtActionResult>(viewResult.Result);
                var delivery = Assert.IsType<Delivery>(actionResult.Value);
                Assert.Equal(DELIVERY_ID, delivery.DeliveryId);
            }
        }

        [Fact]
        public async Task Cannot_add_Delivery_when_it_already_exists()
        {
            const string DELIVERY_ID = "DL001";
            const string SITE_CODE = "SITE001";
            const int ORDER_REFERENCE = 1;
            const string STATUS = "ON PROCESS";

            using (var context = new ProcurementDbContext(ContextOptions))
            {
                DeliveriesController deliveriesController = new DeliveriesController(context);

                DeliveryDTO deliveryDto = new DeliveryDTO { DeliveryId = DELIVERY_ID, SiteCode = SITE_CODE, PurchaseOrder = ORDER_REFERENCE, DeliveryStatus = STATUS };

                try
                {
                    await deliveriesController.PostDelivery(deliveryDto);
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
        public async Task Can_delete_Delivery_by_Id()
        {
            const string DELIVERY_ID = "DL001";

            using (var context = new ProcurementDbContext(ContextOptions))
            {
                DeliveriesController deliveriesController = new DeliveriesController(context);

                var result = await deliveriesController.DeleteDelivery(DELIVERY_ID);

                var viewResult = Assert.IsType<ActionResult<Delivery>>(result);
                var delivery = Assert.IsType<Delivery>(viewResult.Value);

                Assert.Equal(DELIVERY_ID, delivery.DeliveryId);
            }
        }

        [Fact]
        public async Task Cannot_delete_Delivery_when_it_not_existing()
        {
            const string DELIVERY_ID = "DL0001";

            using (var context = new ProcurementDbContext(ContextOptions))
            {
                DeliveriesController deliveriesController = new DeliveriesController(context);

                var result = await deliveriesController.DeleteDelivery(DELIVERY_ID);

                var viewResult = Assert.IsType<ActionResult<Delivery>>(result);
                Assert.IsNotType<Delivery>(viewResult.Value);
                var response = Assert.IsType<NotFoundResult>(viewResult.Result);
                Assert.Equal(404, response.StatusCode);
            }
        }
    }
}
