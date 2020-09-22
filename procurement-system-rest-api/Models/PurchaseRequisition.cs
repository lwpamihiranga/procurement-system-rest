using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace procurement_system_rest_api.Models
{
    public class PurchaseRequisition
    {
        public int RequisitionNo { get; set; }
        public string SiteManagerId { get; set; }
        public DateTime CurrentDate { get; set; }
        public string SupplierCode { get; set; }
        public string CompanyName { get; set; }
        public string ShippingAddress { get; set; }
        public string SiteName { get; set; }
        public DateTime DeliverBefore { get; set; }
        public double DeliveryCost { get; set; }
        public double TotalCost { get; set; }
        public string Status { get; set; }
        public string Comments { get; set; }
        public List<Site> Items { get; set; }
    }
}
