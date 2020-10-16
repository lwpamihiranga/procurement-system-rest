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
    public class ItemsControllerTest : SeedDatabase
    {
        public ItemsControllerTest() : base(
            new DbContextOptionsBuilder<ProcurementDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options)
        { }

        [Fact]
        public async Task Can_get_all_Items_in_database()
        {
            using (var context = new ProcurementDbContext(ContextOptions))
            {
                ItemsController itemsController = new ItemsController(context);

                var result = await itemsController.GetItems();

                var viewResult = Assert.IsType<ActionResult<IEnumerable<Item>>>(result);
                var items = Assert.IsType<List<Item>>(viewResult.Value);
                Assert.Equal(3, items.Count);
            }
        }

        [Fact]
        public async Task Can_get_Item_details_by_ItemId()
        {
            const string ITEMID = "IT001";

            using (var context = new ProcurementDbContext(ContextOptions))
            {
                ItemsController itemsController = new ItemsController(context);

                var result = await itemsController.GetItem(ITEMID);

                var viewResult = Assert.IsType<ActionResult<Item>>(result);
                var item = Assert.IsType<Item>(viewResult.Value);
                Assert.NotNull(item);
                Assert.Equal(ITEMID, item.ItemId);
            }
        }

        [Fact]
        public async Task Should_not_return_Item_when_ItemId_not_existing()
        {
            const string ITEMID = "IT00001";

            using (var context = new ProcurementDbContext(ContextOptions))
            {
                ItemsController itemsController = new ItemsController(context);

                var result = await itemsController.GetItem(ITEMID);

                var viewResult = Assert.IsType<ActionResult<Item>>(result);
                Assert.IsNotType<Item>(viewResult.Value);
                var response = Assert.IsType<NotFoundResult>(viewResult.Result);
                Assert.Equal(404, response.StatusCode);
            }
        }

        [Fact]
        public async Task Can_add_Item_when_it_not_existing()
        {
            using (var context = new ProcurementDbContext(ContextOptions))
            {
                ItemsController itemsController = new ItemsController(context);

                Item item = new Item { ItemId = "IT004", ItemName = "Roofing Sheets", ItemPrice = 200.20, Description = "Roof sheets", SupplierCode = "SP1" };

                var result = await itemsController.PostItem(item);

                var viewResult = Assert.IsType<ActionResult<Item>>(result);
                var actionResult = Assert.IsType<CreatedAtActionResult>(viewResult.Result);
                var model = Assert.IsType<Item>(actionResult.Value);
                Assert.Equal("IT004", model.ItemId);
            }
        }

        [Fact]
        public async Task Cannot_add_Item_when_it_exist()
        {
            using (var context = new ProcurementDbContext(ContextOptions))
            {
                ItemsController itemsController = new ItemsController(context);

                Item item = new Item { ItemId = "IT001", ItemName = "Roofing Sheets", ItemPrice = 200.20, Description = "Roof sheets", SupplierCode = "SP1" };

                try
                {
                    await itemsController.PostItem(item);
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
        public async Task Can_delete_Item_by_ItemId()
        {
            const string ITEMID = "IT001";

            using (var context = new ProcurementDbContext(ContextOptions))
            {
                ItemsController itemsController = new ItemsController(context);

                var result = await itemsController.DeleteItem(ITEMID);

                var viewResult = Assert.IsType<ActionResult<Item>>(result);
                var model = Assert.IsType<Item>(viewResult.Value);

                Assert.Equal(ITEMID, model.ItemId);
            }
        }

        [Fact]
        public async Task Cannot_delete_Item_when_it_not_exist()
        {
            const string ITEMID = "IT00001";

            using (var context = new ProcurementDbContext(ContextOptions))
            {
                ItemsController itemsController = new ItemsController(context);

                var result = await itemsController.DeleteItem(ITEMID);

                var viewResult = Assert.IsType<ActionResult<Item>>(result);
                Assert.IsNotType<Item>(viewResult.Value);
                var response = Assert.IsType<NotFoundResult>(viewResult.Result);
                Assert.Equal(404, response.StatusCode);
            }
        }
    }
}
