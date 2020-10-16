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
    public class EnquiriesControllerTest : SeedDatabase
    {
        public EnquiriesControllerTest() : base(
            new DbContextOptionsBuilder<ProcurementDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options)
        { }
        
        [Fact]
        public async Task Can_get_all_Enquiries_in_database()
        {
            using (var context = new ProcurementDbContext(ContextOptions))
            {
                EnquiriesController enquiriesController = new EnquiriesController(context);

                var result = await enquiriesController.GetEnquiries();

                var viewResult = Assert.IsType<ActionResult<IEnumerable<Enquiry>>>(result);
                var enquiries = Assert.IsType<List<Enquiry>>(viewResult.Value);
                Assert.Equal(3, enquiries.Count);
            }
        }

        [Fact]
        public async Task Can_get_Enquiry_details_by_EnquiryId()
        {
            const int ENQUIRY_ID = 1;

            using (var context = new ProcurementDbContext(ContextOptions))
            {
                EnquiriesController enquiriesController = new EnquiriesController(context);

                var result = await enquiriesController.GetEnquiry(ENQUIRY_ID);

                var viewResult = Assert.IsType<ActionResult<Enquiry>>(result);
                var enquiry = Assert.IsType<Enquiry>(viewResult.Value);
                Assert.NotNull(enquiry);
                Assert.Equal(ENQUIRY_ID, enquiry.EnquiryId);
            }
        }

        [Fact]
        public async Task Should_not_return_Enquiry_when_Enquiry_not_existing()
        {
            const int ENQUIRY_ID = 11;

            using (var context = new ProcurementDbContext(ContextOptions))
            {
                EnquiriesController enquiriesController = new EnquiriesController(context);

                var result = await enquiriesController.GetEnquiry(ENQUIRY_ID);

                var viewResult = Assert.IsType<ActionResult<Enquiry>>(result);
                Assert.IsNotType<Enquiry>(viewResult.Value);
                var response = Assert.IsType<NotFoundResult>(viewResult.Result);
                Assert.Equal(404, response.StatusCode);
            }
        }


        [Fact]
        public async Task Can_add_Enquiry()
        {
            const int ORDER_REFERENCE = 1;
            const string SITE_MANAGER_ID = "EMP1";
            const string DESCRIPTION = "This is immediate";
            const string STATUS = "New";
            using (var context = new ProcurementDbContext(ContextOptions))
            {
                EnquiriesController enquiriesController = new EnquiriesController(context);

                EnquiryDTO enquiryDto = new EnquiryDTO { OrderReference = ORDER_REFERENCE, SiteManagerId = SITE_MANAGER_ID, Description = DESCRIPTION, EnquiryStatus = STATUS };

                var result = await enquiriesController.PostEnquiry(enquiryDto);

                var viewResult = Assert.IsType<ActionResult<Enquiry>>(result);
                var actionResult = Assert.IsType<CreatedAtActionResult>(viewResult.Result);
                var model = Assert.IsType<Enquiry>(actionResult.Value);
                Assert.Equal(DESCRIPTION, model.Description);
            }
        }

        [Fact]
        public async Task Can_delete_Enquiry_by_EnquiryId()
        {
            const int ENQUIRY_ID = 1;

            using (var context = new ProcurementDbContext(ContextOptions))
            {
                EnquiriesController enquiriesController = new EnquiriesController(context);

                var result = await enquiriesController.DeleteEnquiry(ENQUIRY_ID);

                var viewResult = Assert.IsType<ActionResult<Enquiry>>(result);
                var model = Assert.IsType<Enquiry>(viewResult.Value);

                Assert.Equal(ENQUIRY_ID, model.EnquiryId);
            }
        }

        [Fact]
        public async Task Cannot_delete_Enquiry_when_it_not_exist()
        {
            const int ENQUIRY_ID = 11;

            using (var context = new ProcurementDbContext(ContextOptions))
            {
                EnquiriesController enquiriesController = new EnquiriesController(context);

                var result = await enquiriesController.DeleteEnquiry(ENQUIRY_ID);

                var viewResult = Assert.IsType<ActionResult<Enquiry>>(result);
                Assert.IsNotType<Enquiry>(viewResult.Value);
                var response = Assert.IsType<NotFoundResult>(viewResult.Result);
                Assert.Equal(404, response.StatusCode);
            }
        }

        [Fact]
        public async Task Returned_Enquiry_should_include_relavant_OrderReference()
        {
            const int ENQUIRY_ID = 2;
            const int ORDER_REFERENCE_NO = 1;

            using (var context = new ProcurementDbContext(ContextOptions))
            {
                EnquiriesController enquiriesController = new EnquiriesController(context);

                var result = await enquiriesController.GetEnquiry(ENQUIRY_ID);

                var viewResult = Assert.IsType<ActionResult<Enquiry>>(result);
                var enquiry = Assert.IsType<Enquiry>(viewResult.Value);
                Assert.NotNull(enquiry);
                Assert.Equal(ENQUIRY_ID, enquiry.EnquiryId);

                var purchaseOrder = Assert.IsType<PurchaseOrder>(enquiry.OrderReference);
                Assert.NotNull(purchaseOrder);
                Assert.Equal(ORDER_REFERENCE_NO, purchaseOrder.OrderReference);
            }
        }
    }
}
