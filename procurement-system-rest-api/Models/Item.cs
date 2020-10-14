using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace procurement_system_rest_api.Models
{
    public class Item
    {
        [Key]
        public string ItemId { get; set; }
        public string ItemName { get; set; }
        public double ItemPrice { get; set; }
        public string Description { get; set; }
        public string UnitOfMeasuring { get; set; }
        //public List<Supplier> Suppliers { get; set; }
        public ICollection<ItemSuppliers> ItemSuppliers { get; set; }
        public ICollection<PurchaseRequisitionItems> PurchaseRequisitionItems { get; set; }
        public ICollection<PurchaseOrderItems> PurchaseOrderItems { get; set; }
        [NotMapped]
        public string SupplierCode { get; set; }
    }
}
