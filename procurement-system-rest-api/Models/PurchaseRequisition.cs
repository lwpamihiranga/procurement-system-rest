using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace procurement_system_rest_api.Models
{
    public class PurchaseRequisition
    {
        [Key]
        public int RequisitionNo { get; set; }
        public SiteManager SiteManager { get; set; } 
        public Supplier Supplier { get; set; }
        public string ShippingAddress { get; set; }
        public Site Site { get; set; }
        public DateTime DeliverBefore { get; set; }
        public double DeliveryCost { get; set; }
        public double TotalCost { get; set; }
        public string Status { get; set; }
        public string Comments { get; set; }
        public ICollection<PurchaseRequisitionItems> PurchaseRequisitionItems { get; set; }
    }
}
