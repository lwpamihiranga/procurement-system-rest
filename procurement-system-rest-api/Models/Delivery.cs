using System.ComponentModel.DataAnnotations;

namespace procurement_system_rest_api.Models
{
    public class Delivery
    {
        [Key]
        public string DeliveryId { get; set; }
        public string DeliveryMethod { get; set; }
        public bool OnSiteDelivery { get; set; } //Boolean -> bool
        public Site Site { get; set; }   //public string SiteCode { get; set; }
        //public string SiteName { get; set; }
        //public string DeliveryAddress { get; set; }
        //public string OrderId { get; set; }
        //public string SupplierCode { get; set; }
        //public string SupplierName { get; set; }
        //public string SupplierCompanyName { get; set; }
        public PurchaseOrder PurchaseOrder { get; set; }
        public string DeliveryStatus { get; set; }
        public double PayableAmount { get; set; }
        public bool IsFullDelivery { get; set; }
    }
}
