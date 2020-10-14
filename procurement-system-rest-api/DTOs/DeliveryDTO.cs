using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace procurement_system_rest_api.DTOs
{
    public class DeliveryDTO
    {
        public string DeliveryId { get; set; }
        public string DeliveryMethod { get; set; }
        public bool OnSiteDelivery { get; set; } //Boolean -> bool
        public string SiteCode { get; set; }  
        public int PurchaseOrder { get; set; }
        public string DeliveryStatus { get; set; }
        public double PayableAmount { get; set; }
        public bool IsFullDelivery { get; set; }
    }
}
