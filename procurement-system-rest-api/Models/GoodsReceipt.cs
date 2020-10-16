using System;
using System.ComponentModel.DataAnnotations;

namespace procurement_system_rest_api.Models
{
    public class GoodsReceipt
    {
        [Key]
        public int ReceiptId { get; set; }
        public PurchaseOrder PurchaseOrder { get; set; }
        public Supplier Supplier { get; set; }
        public Site Site { get; set; }
        public Delivery Delivery { get; set; }
        public DateTime DateDelivered { get; set; }
    }
}
