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
    public class GoodsReceiptControllerTest : SeedDatabase
    {
        public GoodsReceiptControllerTest() : base(
            new DbContextOptionsBuilder<ProcurementDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options)
        { }

        [Fact]
        public async Task Can_get_all_GoodsReceipts_in_database()
        {
            using (var context = new ProcurementDbContext(ContextOptions))
            {
                GoodsReceiptsController receiptsController = new GoodsReceiptsController(context);

                var result = await receiptsController.GetGoodsReceipt();

                var viewResult = Assert.IsType<ActionResult<IEnumerable<GoodsReceipt>>>(result);
                var goodsReceipts = Assert.IsType<List<GoodsReceipt>>(viewResult.Value);
                Assert.Single(goodsReceipts);
            }
        }

        [Fact]
        public async Task Can_get_GoodsReceipt_details_by_ReceiptId()
        {
            const int RECEIPT_ID = 1;

            using (var context = new ProcurementDbContext(ContextOptions))
            {
                GoodsReceiptsController receiptsController = new GoodsReceiptsController(context);

                var result = await receiptsController.GetGoodsReceipt(RECEIPT_ID);

                var viewResult = Assert.IsType<ActionResult<GoodsReceipt>>(result);
                var goodsReceipt = Assert.IsType<GoodsReceipt>(viewResult.Value);
                Assert.NotNull(goodsReceipt);
                Assert.Equal(RECEIPT_ID, goodsReceipt.ReceiptId);
            }
        }

        [Fact]
        public async Task Should_not_return_GoodsReceipt_when_not_existing()
        {
            const int RECEIPT_ID = 11;

            using (var context = new ProcurementDbContext(ContextOptions))
            {
                GoodsReceiptsController receiptsController = new GoodsReceiptsController(context);

                var result = await receiptsController.GetGoodsReceipt(RECEIPT_ID);

                var viewResult = Assert.IsType<ActionResult<GoodsReceipt>>(result);
                Assert.IsNotType<GoodsReceipt>(viewResult.Value);
                var response = Assert.IsType<NotFoundResult>(viewResult.Result);
                Assert.Equal(404, response.StatusCode);
            }
        }

        [Fact]
        public async Task Can_add_GoodsReceipt()
        {
            const string DELIVERY_ID = "DL002";
            const string SITE_CODE = "SITE001";
            const int PURCHASE_ORDER = 1;

            using (var context = new ProcurementDbContext(ContextOptions))
            {
                GoodsReceiptsController receiptsController = new GoodsReceiptsController(context);

                var goodsReceipt = new GoodsReceiptDTO { DeliveryId = DELIVERY_ID, SiteCode = SITE_CODE, PurchaseOrder = PURCHASE_ORDER };

                var result = await receiptsController.PostGoodsReceipt(goodsReceipt);

                var viewResult = Assert.IsType<ActionResult<GoodsReceipt>>(result);
                var actionResult = Assert.IsType<CreatedAtActionResult>(viewResult.Result);
                var receipt = Assert.IsType<GoodsReceipt>(actionResult.Value);
                Assert.NotNull(receipt);
            }
        }

        [Fact]
        public async Task Can_delete_GoodsReceipt_by_ReceiptId()
        {
            const int RECEIPT_ID = 1;

            using (var context = new ProcurementDbContext(ContextOptions))
            {
                GoodsReceiptsController receiptsController = new GoodsReceiptsController(context);

                var result = await receiptsController.DeleteGoodsReceipt(RECEIPT_ID);

                var viewResult = Assert.IsType<ActionResult<GoodsReceipt>>(result);
                var goodsReceipt = Assert.IsType<GoodsReceipt>(viewResult.Value);

                Assert.Equal(RECEIPT_ID, goodsReceipt.ReceiptId);
            }
        }

        [Fact]
        public async Task Cannot_delete_GoodsReceipt_when_it_not_exist()
        {
            const int RECEIPT_ID = 11;

            using (var context = new ProcurementDbContext(ContextOptions))
            {
                GoodsReceiptsController receiptsController = new GoodsReceiptsController(context);

                var result = await receiptsController.DeleteGoodsReceipt(RECEIPT_ID);

                var viewResult = Assert.IsType<ActionResult<GoodsReceipt>>(result);
                Assert.IsNotType<GoodsReceipt>(viewResult.Value);
                var response = Assert.IsType<NotFoundResult>(viewResult.Result);
                Assert.Equal(404, response.StatusCode);
            }
        }

        [Fact]
        public async Task Returned_GoodsReceipt_should_include_relavant_PurchaseOrder()
        {
            const int RECEIPT_ID = 1;
            const int PURCHASE_ORDER_REFERENCE = 1;

            using (var context = new ProcurementDbContext(ContextOptions))
            {
                GoodsReceiptsController receiptsController = new GoodsReceiptsController(context);

                var result = await receiptsController.GetGoodsReceipt(RECEIPT_ID);

                var viewResult = Assert.IsType<ActionResult<GoodsReceipt>>(result);
                var goodsReceipt = Assert.IsType<GoodsReceipt>(viewResult.Value);
                Assert.NotNull(goodsReceipt);
                Assert.Equal(RECEIPT_ID, goodsReceipt.ReceiptId);

                var purchaseOrder = Assert.IsType<PurchaseOrder>(goodsReceipt.PurchaseOrder);
                Assert.NotNull(purchaseOrder);
                Assert.Equal(PURCHASE_ORDER_REFERENCE, purchaseOrder.OrderReference);
            }
        }
    }
}
