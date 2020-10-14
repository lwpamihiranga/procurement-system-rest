using System.Collections.Generic;

namespace procurement_system_rest_api.Models
{
    public class SiteManager: CompanyStaff
    {
        public List<Site> SiteList { get; set; }
        public List<PurchaseRequisition> PurchaseRequisitionsMade { get; set; }
        public List<PurchaseOrder> PurchaseOrdersMade { get; set; }
        public List<Enquiry> Enquiries { get; set; }
    }
}
