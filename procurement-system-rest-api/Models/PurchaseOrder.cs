using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace procurement_system_rest_api.Models
{
    public class PurchaseOrder
    {
        [Key]
        public int OrderReference { get; set; }
        public SiteManager SiteManager { get; set; }
        public Supplier Supplier { get; set; }
        public string ShippingAddress { get; set; }
        public ManagementStaff ApprovedBy { get; set; }
        public Site Site { get; set; }
        public DateTime DeliverBefore { get; set; }
        public double DeliveryCost { get; set; }
        public double TotalCost { get; set; }
        public string OrderStatus { get; set; }
        public ICollection<PurchaseOrderItems> PurchaseOrderItems { get; set; }
    }
}
