using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace procurement_system_rest_api.Models
{
    public class Supplier
    {
        [Key]
        public string SupplierCode { get; set; }
        public string SupplierName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string CompanyNo { get; set; }
        public string MobileNo { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string WebSite { get; set; }
        public ICollection<ItemSuppliers> ItemSuppliers { get; set; }
    }
}
