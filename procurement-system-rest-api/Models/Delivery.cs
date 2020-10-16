using System.ComponentModel.DataAnnotations;

namespace procurement_system_rest_api.Models
{
    public class Delivery
    {
        [Key]
        public string DeliveryId { get; set; }
        public string DeliveryMethod { get; set; }
        public bool OnSiteDelivery { get; set; } 
        public PurchaseOrder PurchaseOrder { get; set; }
        public string DeliveryStatus { get; set; }
        public double PayableAmount { get; set; }
        public bool IsFullDelivery { get; set; }
    }
}
