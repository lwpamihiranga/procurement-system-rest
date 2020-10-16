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
    public class InvoicesControllerTest : SeedDatabase
    {
        public InvoicesControllerTest() : base(
            new DbContextOptionsBuilder<ProcurementDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options)
        { }

        [Fact]
        public async Task Can_get_all_Invoices_in_database()
        {
            using (var context = new ProcurementDbContext(ContextOptions))
            {
                InvoicesController invoicesController = new InvoicesController(context);

                var result = await invoicesController.GetInvoice();

                var viewResult = Assert.IsType<ActionResult<IEnumerable<Invoice>>>(result);
                var invoices = Assert.IsType<List<Invoice>>(viewResult.Value);

                Assert.Single(invoices);
            }
        }


        [Fact]
        public async Task Can_get_Invoice_By_Id()
        {
            const string INVOICE_ID = "INV001";

            using (var context = new ProcurementDbContext(ContextOptions))
            {
                InvoicesController invoicesController = new InvoicesController(context);

                var result = await invoicesController.GetInvoice(INVOICE_ID);

                var viewResult = Assert.IsType<ActionResult<Invoice>>(result);
                var invoice = Assert.IsType<Invoice>(viewResult.Value);

                Assert.Equal(INVOICE_ID, invoice.InvoiceId);
            }
        }

        [Fact]
        public async Task Should_not_return_Invoice_when_unavailable()
        {
            const string INVOICE_ID = "INV0001";

            using (var context = new ProcurementDbContext(ContextOptions))
            {
                InvoicesController invoicesController = new InvoicesController(context);

                var result = await invoicesController.GetInvoice(INVOICE_ID);

                var viewResult = Assert.IsType<ActionResult<Invoice>>(result);
                Assert.IsNotType<Invoice>(viewResult.Value);
                var response = Assert.IsType<NotFoundResult>(viewResult.Result);
                Assert.Equal(404, response.StatusCode);
            }
        }

        [Fact]
        public async Task Can_add_new_Invoice_when_it_not_existing()
        {
            const string INVOICE_ID = "INV002";
            const double NET_AMOUNT = 2000.00;
            const string DESCRIPTION = "Order 1 invoice";
            const int GOODS_RECEIPT = 1;
            const string SUPPLIER = "SP1";
            const string ACCOUNTING_STAFF_ID = "EMP21";

            using (var context = new ProcurementDbContext(ContextOptions))
            {
                InvoicesController invoicesController = new InvoicesController(context);

                var invoice = new InvoiceDTO { InvoiceId = INVOICE_ID, NetAmount = NET_AMOUNT, Description = DESCRIPTION, GoodsReceipt = GOODS_RECEIPT, Supplier = SUPPLIER, AccountingStaffId = ACCOUNTING_STAFF_ID };

                var result = await invoicesController.PostInvoice(invoice);

                var viewResult = Assert.IsType<ActionResult<Invoice>>(result);
                var actionResult = Assert.IsType<CreatedAtActionResult>(viewResult.Result);
                var invoiceResult = Assert.IsType<Invoice>(actionResult.Value);
                Assert.Equal(INVOICE_ID, invoiceResult.InvoiceId);
            }
        }

        [Fact]
        public async Task Cannot_add_Invoice_when_it_already_exists()
        {
            const string INVOICE_ID = "INV001";
            const double NET_AMOUNT = 2000.00;
            const string DESCRIPTION = "Order 1 invoice";
            const int GOODS_RECEIPT = 1;
            const string SUPPLIER = "SP1";
            const string ACCOUNTING_STAFF_ID = "EMP21";

            using (var context = new ProcurementDbContext(ContextOptions))
            {
                InvoicesController invoicesController = new InvoicesController(context);

                var invoice = new InvoiceDTO { InvoiceId = INVOICE_ID, NetAmount = NET_AMOUNT, Description = DESCRIPTION, GoodsReceipt = GOODS_RECEIPT, Supplier = SUPPLIER, AccountingStaffId = ACCOUNTING_STAFF_ID };

                try
                {
                    await invoicesController.PostInvoice(invoice);
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
        public async Task Can_delete_Invoice_by_Id()
        {
            const string INVOICE_ID = "INV001";

            using (var context = new ProcurementDbContext(ContextOptions))
            {
                InvoicesController invoicesController = new InvoicesController(context);

                var result = await invoicesController.DeleteInvoice(INVOICE_ID);

                var viewResult = Assert.IsType<ActionResult<Invoice>>(result);
                var invoice = Assert.IsType<Invoice>(viewResult.Value);

                Assert.Equal(INVOICE_ID, invoice.InvoiceId);
            }
        }

        [Fact]
        public async Task Cannot_delete_Invoice_when_it_not_existing()
        {
            const string INVOICE_ID = "INV0001";

            using (var context = new ProcurementDbContext(ContextOptions))
            {
                InvoicesController invoicesController = new InvoicesController(context);

                var result = await invoicesController.DeleteInvoice(INVOICE_ID);

                var viewResult = Assert.IsType<ActionResult<Invoice>>(result);
                Assert.IsNotType<Invoice>(viewResult.Value);
                var response = Assert.IsType<NotFoundResult>(viewResult.Result);
                Assert.Equal(404, response.StatusCode);
            }
        }
    }
}
