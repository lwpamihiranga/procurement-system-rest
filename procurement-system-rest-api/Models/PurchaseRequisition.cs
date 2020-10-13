using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace procurement_system_rest_api.Models
{
    public class PurchaseRequisition
    {
        [Key]
        public int RequisitionNo { get; set; }
        public SiteManager SiteManager { get; set; }   // string SiteManagerId -> SiteManager SiteManger
        //public DateTime CurrentDate { get; set; }  // no need having a current date field. it should get from system
        public Supplier Supplier { get; set; } //string SupplierCode - > Supplier Supplier  
       //public string CompanyName { get; set; }  //include in Supplier model
        public string ShippingAddress { get; set; }
        public Site Site { get; set; } //public string SiteName { get; set; }
        // no point of having site name. dont allow relationship. added siteCode with site enityty
        public DateTime DeliverBefore { get; set; }
        public double DeliveryCost { get; set; }
        public double TotalCost { get; set; }
        public string Status { get; set; }
        public string Comments { get; set; }
        //public List<Site> Items { get; set; }
        public ICollection<PurchaseRequisitionItems> PurchaseRequisitionItems { get; set; }
    }
}
