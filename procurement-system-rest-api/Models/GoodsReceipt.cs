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
        public Supplier Supplier { get; set; }    //string SupplierName -> Supplier Supplier
        //public string SupplierCompany { get; set; } //include in Supplier
        public Site Site { get; set; }      //string SiteName ->  Site Site 
        public List<Item> Items { get; set; }
        public Delivery Delivery { get; set; }   //string DeliveryAddress  ->   Delivery Delivery
        public DateTime DateDelivered { get; set; } //Data -> Datetime
    }
}
