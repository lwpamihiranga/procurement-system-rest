using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace procurement_system_rest_api.Models
{
    public class PurchaseOrder
    {
        public int OrderReference { get; set; }
        public SiteManager SiteManager { get; set; }     // string SiteManagerName -> SiteManager SiteManager
        public DateTime CurrentDate { get; set; }   // Date  ->  DateTime
        public Supplier Supplier { get; set; }          //string SupplierCode  ->  Supplier Supplier
        //public string SupplierName { get; set; }      //include in Supplier
       // public string SupplierCompany { get; set; }   //include in supplier
        public string ShippingAddress { get; set; }
        public string ApprovedBy { get; set; }
        public Site Site { get; set; }    //string SiteName ->  Site Site 
        public DateTime DeliverBefor { get; set; } // Date -> DateTime
        public List<Item> Items { get; set; }
        public double DeliveryCost { get; set; }
        public double TotalCost { get; set; }
        public string OrderStatus { get; set; }

    }
}
