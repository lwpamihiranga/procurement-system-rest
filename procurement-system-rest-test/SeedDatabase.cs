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

                var site1 = new Site { SiteCode = "SITE001", SiteName = "SLIIT Campus Site", SiteAddress = "Malabe", Description = "Malabe SLIIT Campus working site", SiteOfficeNo = "0115489657", SiteManager = manager1 };

                context.Sites.Add(site1);

                var supplier1 = new Supplier { SupplierCode = "SP1", SupplierName = "MAS Holdings", Address1 = "Colombo 3", CompanyNo = "011548795", MobileNo = "077485698", Email = "supplier@mas.com" };

                context.Supplier.Add(supplier1);
                context.Supplier.Add(new Supplier { SupplierCode = "SP2", SupplierName = "MAS Holdings", Address1 = "Colombo 3", CompanyNo = "011548795", MobileNo = "077485698", Email = "supplier@mas.com"  });
                context.Supplier.Add(new Supplier { SupplierCode = "SP3", SupplierName = "MAS Holdings", Address1 = "Colombo 3", CompanyNo = "011548795", MobileNo = "077485698", Email = "supplier@mas.com"  });

                var item1 = new Item { ItemId = "IT001", ItemName = "Roofing Sheets", ItemPrice = 200.20, Description = "Roof sheets" };
                var item2 = new Item { ItemId = "IT002", ItemName = "Roofing Sheets", ItemPrice = 200.20, Description = "Roof sheets" };
                var item3 = new Item { ItemId = "IT003", ItemName = "Roofing Sheets", ItemPrice = 200.20, Description = "Roof sheets" };
                context.Items.Add(item1);
                context.Items.Add(item2);
                context.Items.Add(item3);
                var itemSupplier1 = new ItemSuppliers { Item = item1, Supplier = supplier1 };
                var itemSupplier2 = new ItemSuppliers { Item = item2, Supplier = supplier1 };
                var itemSupplier3 = new ItemSuppliers { Item = item3, Supplier = supplier1 };
                context.Add(itemSupplier1);
                context.Add(itemSupplier2);
                context.Add(itemSupplier3);

                var requisition1 = new PurchaseRequisition 
                { 
                    RequisitionNo = 1, 
                    ShippingAddress = "Malabe", 
                    TotalCost = 2000.00, 
                    Status = "Pending", 
                    SiteManager = manager1,
                    Supplier = supplier1, 
                    Site = site1 
                };
                context.PurchaseRequisitions.Add(requisition1);
                var requisitionItem1 = new PurchaseRequisitionItems { Item = item1, PurchaseRequisition = requisition1, ItemCount = 3 };
                var requisitionItem2 = new PurchaseRequisitionItems { Item = item2, PurchaseRequisition = requisition1, ItemCount = 2 };
                var requisitionItem3 = new PurchaseRequisitionItems { Item = item3, PurchaseRequisition = requisition1, ItemCount = 1 };
                context.Add(requisitionItem1);
                context.Add(requisitionItem2);
                context.Add(requisitionItem3);

                var order1 = new PurchaseOrder
                {
                    OrderReference = 1,
                    ShippingAddress = "Malabe",
                    TotalCost = 2000.00,
                    OrderStatus = "IN PROCESS",
                    SiteManager = manager1,
                    Supplier = supplier1,
                    Site = site1
                };
                context.PurchaseOrders.Add(order1);
                var orderItems1 = new PurchaseOrderItems { Item = item1, PurchaseOrder = order1, ItemCount = 3 };
                var orderItems2 = new PurchaseOrderItems { Item = item2, PurchaseOrder = order1, ItemCount = 2 };
                var orderItems3 = new PurchaseOrderItems { Item = item3, PurchaseOrder = order1, ItemCount = 1 };
                context.Add(orderItems1);
                context.Add(orderItems2);
                context.Add(orderItems3);

                var enquiry1 = new Enquiry { EnquiryId = 1, Description = "Why order is late?", EnquiryStatus = "Pending", OrderReference = order1, SiteManager = manager1 };       
                var enquiry2 = new Enquiry { EnquiryId = 2, Description = "Why order is late?", EnquiryStatus = "Pending", OrderReference = order1, SiteManager = manager1 };       
                var enquiry3 = new Enquiry { EnquiryId = 3, Description = "Why order is late?", EnquiryStatus = "Pending", OrderReference = order1, SiteManager = manager1 };
                context.Add(enquiry1);
                context.Add(enquiry2);
                context.Add(enquiry3);

                var delivery1 = new Delivery { DeliveryId = "DL001", OnSiteDelivery = true, DeliveryStatus = "On Process", IsFullDelivery = true, Site = site1, PurchaseOrder = order1 };
                context.Deliveries.Add(delivery1);

                var goodsReceipt1 = new GoodsReceipt { ReceiptId = 1, PurchaseOrder = order1, Supplier = supplier1, Site = site1, Delivery = delivery1 };
                context.GoodsReceipt.Add(goodsReceipt1);

                context.SaveChanges();
            }
        }
    }
}
