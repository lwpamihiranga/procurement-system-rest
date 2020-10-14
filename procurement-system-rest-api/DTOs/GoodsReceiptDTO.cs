using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace procurement_system_rest_api.DTOs
{
    public class GoodsReceiptDTO
    {
        public int ReceiptId { get; set; }
        public int PurchaseOrder { get; set; }
        public string SupplierCode { get; set; } 
        public string SiteCode { get; set; }   
        public string DeliveryId { get; set; }   
        public DateTime DateDelivered { get; set; } 
    }
}
