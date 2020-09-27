using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace procurement_system_rest_api.Models
{
    public class Enquiry
    {
        public int EnquiryId { get; set; }
        public PurchaseOrder OrderReference { get; set; } // string -> purchaseOrder
        public SiteManager SiteManager { get; set; }  // string SitrManagerId -> SiteManager SiteManager
        public string OrderType { get; set; }
        public string Description { get; set; }
        public string EnquiryStatus { get; set; }
    }
}
