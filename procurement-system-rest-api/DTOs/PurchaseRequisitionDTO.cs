using System;

namespace procurement_system_rest_api.DTOs
{
    public class PurchaseRequisitionDTO
    {
        public int RequisitionNo { get; set; }
        public string SiteManagerId { get; set; } 
        public string SupplierCode { get; set; } 
        public string ShippingAddress { get; set; }
        public string SiteCode { get; set; } 
        public DateTime DeliverBefore { get; set; }
        public double DeliveryCost { get; set; }
        public double TotalCost { get; set; }
        public string Status { get; set; }
        public string Comments { get; set; }
        public string[] ItemIds { get; set; }
    }
}
