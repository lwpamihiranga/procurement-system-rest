using System;

namespace procurement_system_rest_api.DTOs
{
    public class PurchaseOrderDTO
    {
        public int OrderReference { get; set; }
        public string SiteManagerId { get; set; }    
        public string SupplierCode { get; set; }         
        public string ShippingAddress { get; set; }
        public string ManagementStaffId { get; set; } 
        public string SiteCode { get; set; }    
        public DateTime DeliverBefore { get; set; }
        public double DeliveryCost { get; set; }
        public double TotalCost { get; set; }
        public string OrderStatus { get; set; }
        public string[] ItemIds { get; set; }
    }
}
