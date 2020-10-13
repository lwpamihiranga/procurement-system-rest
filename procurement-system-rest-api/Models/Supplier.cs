using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace procurement_system_rest_api.Models
{
    public class Supplier
    {
        [Key]
        public string SupplierCode { get; set; }
        //public string CompanyName { get; set; } // having supplier name and company name two is no use. both are considered as same
        public string SupplierName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string CompanyNo { get; set; }
        public string MobileNo { get; set; }
        public string Fax { get; set; } //int -> string
        public string Email { get; set; }
        public string WebSite { get; set; }
        public ICollection<ItemSuppliers> ItemSuppliers { get; set; }
    }
}
