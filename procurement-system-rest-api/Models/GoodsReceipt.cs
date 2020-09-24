using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace procurement_system_rest_api.Models
{
    public class GoodsReceipt
    {
        public int ReceiptId { get; set; }
        public string OrderReference { get; set; }
        public string SupplierName { get; set; }
        public string SupplierCompany { get; set; }
        public string SiteName { get; set; }
        public List<Item> Items { get; set; }
        public string DeliveryAddress { get; set; }
        public DateTime DateDelivered { get; set; } //Data -> Datetime
    }
}
