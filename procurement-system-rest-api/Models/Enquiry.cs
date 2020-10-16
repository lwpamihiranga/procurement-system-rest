using System.ComponentModel.DataAnnotations;

namespace procurement_system_rest_api.Models
{
    public class Enquiry
    {
        [Key]
        public int EnquiryId { get; set; }
        public PurchaseOrder OrderReference { get; set; }
        public SiteManager SiteManager { get; set; }  
        public string OrderType { get; set; }
        public string Description { get; set; }
        public string EnquiryStatus { get; set; }
    }
}
