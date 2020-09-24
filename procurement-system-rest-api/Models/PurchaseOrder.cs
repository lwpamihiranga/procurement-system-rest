using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace procurement_system_rest_api.Models
{
    public class PurchaseOrder
    {
        public int OrderReference { get; set; }
        public string SiteManagerName { get; set; }
        public DateTime CurrentDate { get; set; } // Date  ->  DateTime
        public string SupplierCode { get; set; }
        public string SupplierName { get; set; }
        public string SupplierCompany { get; set; }
        public string ShippingAddress { get; set; }
        public string ApprovedBy { get; set; }
        public string SiteName { get; set; }
        public DateTime DeliverBefor { get; set; } // Date -> DateTime
        public List<Item> Items { get; set; }
        public double DeliveryCost { get; set; }
        public double TotalCost { get; set; }
        public string OrderStatus { get; set; }

    }
}
